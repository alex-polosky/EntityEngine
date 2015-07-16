using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D10;
using SharpDX.DXGI;
using SharpDX.Windows;
using D3D10 = SharpDX.Direct3D10;

using EntityFramework;
using EntityFramework.Components;

namespace EntityEngine.Components
{
    [Flags]
    public enum RenderTypeFlag
    {
        None = 0x0,
        WireFrame = 0x1,
    }

    public static class VertexStructures
    {
        public enum Types
        {
            None, Pos, Textured, Normal, Color, TexturedNormal, ColorNormal
        }

        public struct Pos
        {
            public SharpDX.Vector3 pos;
            public static int sizeInBytes
            { get { return Vector3.SizeInBytes; } }
            public Pos(SharpDX.Vector3 pos)
            { this.pos = pos; }
        }

        public struct Textured
        {
            public SharpDX.Vector3 pos;
            public SharpDX.Vector2 tex;
            public static int sizeInBytes
            { get { return Vector3.SizeInBytes + Vector2.SizeInBytes; } }
            public Textured(SharpDX.Vector3 pos, SharpDX.Vector2 tex)
            { this.pos = pos; this.tex = tex; }
        }

        public struct Normal
        {
            public SharpDX.Vector3 pos;
            public SharpDX.Vector3 uv;
            public static int sizeInBytes
            { get { return Vector3.SizeInBytes + Vector3.SizeInBytes; } }
            public Normal(SharpDX.Vector3 pos, SharpDX.Vector3 uv)
            { this.pos = pos; this.uv = uv; }
        }

        public struct Color
        {
            public SharpDX.Vector3 pos;
            public SharpDX.Vector4 col;
            public static int sizeInBytes
            { get { return Vector3.SizeInBytes + Vector4.SizeInBytes; } }
            public Color(SharpDX.Vector3 pos, SharpDX.Vector4 col)
            { this.pos = pos; this.col = col; }
        }

        public struct TexturedNormal
        {
            public SharpDX.Vector3 pos;
            public SharpDX.Vector2 tex;
            public SharpDX.Vector3 uv;
            public static int sizeInBytes
            { get { return Vector3.SizeInBytes + Vector2.SizeInBytes + Vector3.SizeInBytes; } }
            public TexturedNormal(SharpDX.Vector3 pos, SharpDX.Vector2 tex, SharpDX.Vector3 uv)
            { this.pos = pos; this.tex = tex; this.uv = uv; }
        }

        public struct ColorNormal
        {
            public SharpDX.Vector3 pos;
            public SharpDX.Vector4 col;
            public SharpDX.Vector3 uv;
            public static int sizeInBytes
            { get { return Vector3.SizeInBytes + Vector4.SizeInBytes + Vector3.SizeInBytes; } }
            public ColorNormal(SharpDX.Vector3 pos, SharpDX.Vector4 col, SharpDX.Vector3 uv)
            { this.pos = pos; this.col = col; this.uv = uv; }
        }

        public static Dictionary<Types, Type> TypeMap = new Dictionary<Types, Type>()
        {
            {Types.Pos, typeof(Pos)},
            {Types.Textured, typeof(Textured)},
            {Types.Normal, typeof(Normal)},
            {Types.Color, typeof(Color)},
            {Types.TexturedNormal, typeof(TexturedNormal)},
            {Types.ColorNormal, typeof(ColorNormal)}
        };
    }

    [DataContract]
    public class Shader
    {
        private Guid id;
        [DataMember]
        public Guid guid { get { return this.id; } private set { this.id = value; } }
        [DataMember]
        public string filePath { get; private set; }
        [DataMember]
        public InputElement[] shaderVars { get; private set; }
        [DataMember]
        public string shaderLevel { get; private set; }
        public Effect effect { get; private set; }
        public List<List<InputLayout>> inputLayouts { get; private set; }
        public List<KeyValuePair<object, Type>> vars { get; private set; }

        public Shader()
        {
        }

        public Shader(D3D10.Device device, string path, InputElement[] shaderVars, string shaderLevel = "fx_4_0")
        {
            this.id = Guid.NewGuid();

            this.filePath = path;
            this.effect = null;
            this.shaderVars = shaderVars;
            this.shaderLevel = shaderLevel;

            ShaderBytecode shaderCode = ShaderBytecode.CompileFromFile(
                path,
                shaderLevel
            );
            this.effect = new Effect(device, shaderCode.Data);
            this.inputLayouts = new List<List<InputLayout>>();
            this.vars = new List<KeyValuePair<object, Type>>();

            // Using this, it will allow you to contain multiple passes and techniques in one file
            // Since we can't just get a total amount of techniques and passes, we have to 
            //     check if it is valid or not
            // As of right now, we're using just one set of input vars
            // In the future, I will add a function to allow us to read directly from multiple ones
            int techniqueCount = 0; int passCount = 0;
            while (true)
            {
                var technique = this.effect.GetTechniqueByIndex(techniqueCount);
                if (!technique.IsValid)
                    break;
                this.inputLayouts.Add(new List<InputLayout>());
                while (true)
                {
                    var pass = technique.GetPassByIndex(passCount);
                    if (!pass.IsValid)
                        break;
                    var passSignature = pass.Description.Signature;
                    this.inputLayouts[techniqueCount].Add(
                        new InputLayout(device, passSignature, shaderVars)
                    );
                    passCount += 1;
                }
                techniqueCount += 1;
            }
        }
    }

    [DataContract]
    public class Mesh3D
    {
        private Guid id;
        [DataMember]
        public Guid guid { get { return this.id; } private set { this.id = value; } }
        [DataMember]
        public string filePath { get; private set; }
        public D3D10.VertexBufferBinding vertexBuffer;
        public D3D10.Buffer indexBuffer;
        public int numberOfVertices;
        public short numberOfIndices;

        public static D3D10.Buffer CreateBuffer<T>(D3D10.Device device, BindFlags bindFlags, params T[] items)
            where T : struct
        {
            var len = Utilities.SizeOf(items);
            var stream = new DataStream(len, true, true);
            foreach (var item in items)
                stream.Write(item);
            stream.Position = 0;
            var buffer = new D3D10.Buffer(device, stream, len, ResourceUsage.Default,
                bindFlags, CpuAccessFlags.None, ResourceOptionFlags.None);
            return buffer;
        }

        public Mesh3D()
        {
        }

        private Mesh3D(D3D10.Device device, dynamic vertices, short[] indices, string filePath, bool throwaway=false)
        {
            this.id = Guid.NewGuid();
            this.filePath = filePath;
            this.numberOfVertices = vertices.Length;
            if (indices != null)
            {
                this.numberOfIndices = (short)indices.Length;
                this.indexBuffer = CreateBuffer<short>(device, BindFlags.IndexBuffer, indices);
            }
        }

        public Mesh3D(D3D10.Device device, VertexStructures.Pos[] vertices, short[] indices = null, string filePath = "")
            : this(device, vertices, indices, filePath, false)
        {
            this.vertexBuffer = new VertexBufferBinding(
                CreateBuffer<VertexStructures.Pos>(device, BindFlags.VertexBuffer, vertices),
                VertexStructures.Pos.sizeInBytes, 0);
        }

        public Mesh3D(D3D10.Device device, VertexStructures.Color[] vertices, short[] indices = null, string filePath = "")
            : this(device, vertices, indices, filePath, false)
        {
            this.vertexBuffer = new VertexBufferBinding(
                CreateBuffer<VertexStructures.Color>(device, BindFlags.VertexBuffer, vertices),
                VertexStructures.Color.sizeInBytes, 0);
        }

        public Mesh3D(D3D10.Device device, VertexStructures.Normal[] vertices, short[] indices = null, string filePath = "")
            : this(device, vertices, indices, filePath, false)
        {
            this.vertexBuffer = new VertexBufferBinding(
                CreateBuffer<VertexStructures.Normal>(device, BindFlags.VertexBuffer, vertices),
                VertexStructures.Normal.sizeInBytes, 0);
        }

        public Mesh3D(D3D10.Device device, VertexStructures.Textured[] vertices, short[] indices = null, string filePath = "")
            : this(device, vertices, indices, filePath, false)
        {
            this.vertexBuffer = new VertexBufferBinding(
                CreateBuffer<VertexStructures.Textured>(device, BindFlags.VertexBuffer, vertices),
                VertexStructures.Textured.sizeInBytes, 0);
        }

        public Mesh3D(D3D10.Device device, VertexStructures.ColorNormal[] vertices, short[] indices = null, string filePath = "")
            : this(device, vertices, indices, filePath, false)
        {
            this.vertexBuffer = new VertexBufferBinding(
                CreateBuffer<VertexStructures.ColorNormal>(device, BindFlags.VertexBuffer, vertices),
                VertexStructures.ColorNormal.sizeInBytes, 0);
        }

        public Mesh3D(D3D10.Device device, VertexStructures.TexturedNormal[] vertices, short[] indices = null, string filePath = "")
            : this(device, vertices, indices, filePath, false)
        {
            this.vertexBuffer = new VertexBufferBinding(
                CreateBuffer<VertexStructures.TexturedNormal>(device, BindFlags.VertexBuffer, vertices),
                VertexStructures.TexturedNormal.sizeInBytes, 0);
        }

        [Obsolete]
        public Mesh3D(D3D10.Device device, Type vertFormat, float[] vertices, int[] indices)
        {
            this.numberOfVertices = vertices.Length;
            this.numberOfIndices = (short)indices.Length;
            int sizeOfVert = sizeof(float);
            int sizeOfInd = sizeof(int);
            int sizeOfVertexFormat = 0;
            switch (vertFormat.Name)
            {
                case ("Pos"):
                    sizeOfVertexFormat = VertexStructures.Pos.sizeInBytes;
                    break;
                case ("Textured"):
                    sizeOfVertexFormat = VertexStructures.Textured.sizeInBytes;
                    break;
                case ("Normal"):
                    sizeOfVertexFormat = VertexStructures.Normal.sizeInBytes;
                    break;
                case ("Color"):
                    sizeOfVertexFormat = VertexStructures.Color.sizeInBytes;
                    break;
                case ("TexturedNormal"):
                    sizeOfVertexFormat = VertexStructures.TexturedNormal.sizeInBytes;
                    break;
                case ("ColorNormal"):
                    sizeOfVertexFormat = VertexStructures.ColorNormal.sizeInBytes;
                    break;
                default:
                    break;
            }
            //sizeOfVertexFormat = 28;

            // Create stream
            var vStream = new DataStream(sizeInBytes: vertices.Length * sizeOfVert, canRead: false, canWrite: true);
            var iStream = new DataStream(sizeInBytes: indices.Length * sizeOfInd, canRead: false, canWrite: true);
            vStream.WriteRange(vertices);
            iStream.WriteRange(indices);

            // Reset stream position
            vStream.Position = 0;
            iStream.Position = 0;

            // Create buffer
            var vBuffer = new D3D10.Buffer(
                device, vStream, new BufferDescription(
                    vertices.Length * sizeOfVert,
                    ResourceUsage.Immutable,
                    BindFlags.VertexBuffer,
                    CpuAccessFlags.None,
                    ResourceOptionFlags.None
            )) { DebugName = "Vertex Buffer" };
            this.vertexBuffer = new VertexBufferBinding(vBuffer, sizeOfVertexFormat, 0);

            this.indexBuffer = new D3D10.Buffer(
                device, iStream, new BufferDescription(
                    indices.Length * sizeOfInd,
                    ResourceUsage.Immutable,
                    BindFlags.IndexBuffer,
                    CpuAccessFlags.None,
                    ResourceOptionFlags.None
            )) { DebugName = "Index Buffer" };
        }
    }
}
