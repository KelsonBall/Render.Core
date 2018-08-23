using Kelson.Common.Vectors;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Render.Core.GraphicsInterface
{
    public class ManagedGraphicsService : IDisposable
    {

        public readonly IGraphicsInterface gl;
        private readonly List<IManagedAssetHandle> assets = new List<IManagedAssetHandle>();

        public ManagedGraphicsService(IGraphicsInterface graphics)
        {
            gl = graphics;
        }

        public ShaderProgram CreateProgram(string vertexShader = null, string fragmentShader = null)
        {
            var program = new ShaderProgram(this, vertexShader, fragmentShader);
            assets.Add(program);
            return program;
        }

        public VertShader CreateVertexShader(string source = null)
        {
            var shader = new VertShader(this, program: source);
            assets.Add(shader);
            return shader;
        }

        public FragShader CreateFragmentShader(string source = null)
        {
            var shader = new FragShader(this, program: source);
            assets.Add(shader);
            return shader;
        }

        public VertexBufferObject CreateVertexBuffer(IEnumerable<Vector3fd> vectors)
        {
            var buffer = new VertexBufferObject(this, vectors);
            assets.Add(buffer);
            return buffer;
        }

        public VertexBufferObject CreateVertexBuffer(IEnumerable<Vector2fd> vectors)
        {
            var buffer = new VertexBufferObject(this, vectors.Select(v => new Vector3fd(v, 0f)));
            assets.Add(buffer);
            return buffer;
        }

        public void Dispose()
        {
            //while (assets.Any())
            //{
            //    var asset = assets[assets.Count - 1];
            //    assets.RemoveAt(assets.Count - 1);
            //    asset.Dispose();
            //}
        }
    }

    public class VertexBufferObject : IManagedAssetHandle
    {
        internal VertexBufferObject(ManagedGraphicsService graphics, IEnumerable<Vector3fd> vectors)
        {
            this.graphics = graphics;
            handle = graphics.gl.GenBuffer();
            graphics.gl.BindBuffer(BufferTarget.ArrayBuffer, handle);
            float[] data = vectors.SelectMany(v => new float[] { (float)v.X, (float)v.Y, (float)v.Z }).ToArray();
            graphics.gl.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
            graphics.gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            graphics.gl.EnableVertexAttribArray(0);
        }

        private readonly ManagedGraphicsService graphics;
        public ManagedGraphicsService GraphicsService => throw new NotImplementedException();

        private readonly int handle;
        public int Handle => handle;

        public AssetBinding Binding()
        {
            graphics.gl.BindBuffer(BufferTarget.ArrayBuffer, handle);
            return new AssetBinding(() => graphics.gl.BindBuffer(BufferTarget.ArrayBuffer, 0));
        }

        public void Dispose() => graphics.gl.DeleteShader(handle);
    }

    public class VertexArrayObject : IManagedAssetHandle
    {
        internal VertexArrayObject(ManagedGraphicsService graphics, IEnumerable<Vector3fd> vectors)
        {
            this.graphics = graphics;
            handle = graphics.gl.GenVertexArray();
            graphics.gl.BindBuffer(BufferTarget.ArrayBuffer, handle);
            float[] data = vectors.SelectMany(v => new float[] { (float)v.X, (float)v.Y, (float)v.Z }).ToArray();
            graphics.gl.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
        }

        private readonly ManagedGraphicsService graphics;
        public ManagedGraphicsService GraphicsService => throw new NotImplementedException();

        private readonly int handle;
        public int Handle => handle;

        public AssetBinding Binding()
        {
            graphics.gl.BindBuffer(BufferTarget.ArrayBuffer, handle);
            return new AssetBinding(() => graphics.gl.BindBuffer(BufferTarget.ArrayBuffer, 0));
        }

        public void Dispose() => graphics.gl.DeleteShader(handle);
    }
}
