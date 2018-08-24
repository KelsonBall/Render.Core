using Kelson.Common.Vectors;
using System;
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

        internal readonly Dictionary<int, ShaderProgram> ProgramHandles = new Dictionary<int, ShaderProgram>();
        public ShaderProgram CreateProgram(string vertexShader = null, string fragmentShader = null)
        {
            var program = new ShaderProgram(this, vertexShader, fragmentShader);
            ProgramHandles.Add(program.Handle, program);
            return program;
        }

        internal readonly Dictionary<int, VertShader> VertexShaderHandles = new Dictionary<int, VertShader>();
        public VertShader CreateVertexShader(string source = null)
        {
            var shader = new VertShader(this, program: source);
            VertexShaderHandles.Add(shader.Handle, shader);
            return shader;
        }

        internal readonly Dictionary<int, FragShader> FragmentShaderHandles = new Dictionary<int, FragShader>();
        public FragShader CreateFragmentShader(string source = null)
        {
            var shader = new FragShader(this, program: source);
            FragmentShaderHandles.Add(shader.Handle, shader);
            return shader;
        }

        internal readonly Dictionary<int, VertexBufferObject> VertexBufferHandles = new Dictionary<int, VertexBufferObject>();
        public VertexBufferObject CreateVertexBuffer(IEnumerable<Vector3fd> vectors)
        {
            var buffer = new VertexBufferObject(this, vectors);

            return buffer;
        }

        public VertexBufferObject CreateVertexBuffer(IEnumerable<Vector2fd> vectors)
        {
            var buffer = new VertexBufferObject(this, vectors.Select(v => new Vector3fd(v, 0f)));
            assets.Add(buffer);
            return buffer;
        }

        internal readonly Dictionary<int, VertexArrayObject> VertexArrayHandles = new Dictionary<int, VertexArrayObject>();
        public VertexArrayObject CreateVertexArray()
        {
            var array = new VertexArrayObject(this);
            assets.Add(array);
            return array;
        }

        public void Dispose()
        {
            //foreach (var kvp in VertexShaderHandles.ToList())
            //    kvp.Value.Dispose();
            //foreach (var kvp in FragmentShaderHandles.ToList())
            //    kvp.Value.Dispose();
            //foreach (var kvp in ProgramHandles.ToList())
            //    kvp.Value.Dispose();
            foreach (var kvp in VertexBufferHandles.ToList())
                kvp.Value.Dispose();
            foreach (var kvp in VertexArrayHandles.ToList())
                kvp.Value.Dispose();
        }
    }
}
