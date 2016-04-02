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
}
