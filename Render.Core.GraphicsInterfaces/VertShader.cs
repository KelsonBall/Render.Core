using OpenTK.Graphics.OpenGL;
using System;

namespace Render.Core.GraphicsInterface
{
    public class VertShader : IManagedAssetHandle
    {
        const string DEFAULT_VERTEX_SHANDER_SOURCE = @"
void main()
{
    gl_Position = gl_Vertex;
}
";
        internal VertShader(ManagedGraphicsService graphics, string program = DEFAULT_VERTEX_SHANDER_SOURCE)
        {
            this.graphics = graphics;
            handle = graphics.gl.CreateShader(ShaderType.VertexShader);
            graphics.gl.ShaderSource(handle, program);
            graphics.gl.CompileShader(handle);
        }

        private readonly ManagedGraphicsService graphics;
        public ManagedGraphicsService GraphicsService => throw new NotImplementedException();

        private readonly int handle;
        public int Handle => handle;

        public AssetBinding Binding() => new AssetBinding(() => { });

        public void Dispose() => graphics.gl.DeleteShader(handle);
    }
}
