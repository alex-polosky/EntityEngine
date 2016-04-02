using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
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

namespace EntityEngine.Assets
{
    class Model
    {
    }
}

namespace EntityEngine.Components
{
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

        [OnDeserialized]
        private void _onDeserialized(StreamingContext context)
        {
            // TODO: get path from filename through Map Directory Service?
            Mesh3D m = null;
            //for (var x = 0; x < 5; x++)
            //{
            //    string path = Directory.GetCurrentDirectory();
            //    for (var y = 0; y < x; y++)
            //    {
            //        path = Path.Combine(path, "..");
            //    }
            //    path = Path.Combine(path, this.filePath).PathNormalize();
            //    try
            //    {
            //        m = FileManager.LoadMeshFromFile(GlobalEnvironment.MainWindowDevice, path);
            //        if (m != null)
            //            break;
            //    }
            //    catch { if (x == (5 - 1)) throw; }
            //}
            m = FileManager.LoadMeshFromFile(GlobalEnvironment.MainWindowDevice, FileManagerNS.FileManager.GetAssetFromPath(filePath).AssetPath);
            this.vertexBuffer = m.vertexBuffer;
            this.indexBuffer = m.indexBuffer;
            this.numberOfVertices = m.numberOfVertices;
            this.numberOfIndices = m.numberOfIndices;
        }

        private Mesh3D(D3D10.Device device, dynamic vertices, short[] indices, string filePath, bool throwaway = false)
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
