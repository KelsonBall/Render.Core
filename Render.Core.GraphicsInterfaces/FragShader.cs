using OpenTK.Graphics.OpenGL;
using System;

namespace Render.Core.GraphicsInterface
{
    public class FragShader : IManagedAssetHandle
    {
        const string DEFAULT_FRAGMENT_SHADER_SOURCE = @"
uniform vec4 uColor;
void main (void)
{
   gl_FragColor = uColor;
}
";
        internal FragShader(ManagedGraphicsService graphics, string program = DEFAULT_FRAGMENT_SHADER_SOURCE)
        {
            this.graphics = graphics;
            handle = graphics.gl.CreateShader(ShaderType.FragmentShader);
            graphics.gl.ShaderSource(handle, program);
            graphics.gl.CompileShader(handle);
            var log = graphics.gl.GetShaderInfoLog(handle);
            if (!string.IsNullOrEmpty(log))
                throw new InvalidOperationException(log);
        }

        private readonly ManagedGraphicsService graphics;
        public ManagedGraphicsService GraphicsService => throw new NotImplementedException();

        private readonly int handle;
        public int Handle => handle;

        public AssetBinding Binding() => new AssetBinding(() => { });

        public void Dispose() => graphics.gl.DeleteShader(handle);
    }
}
