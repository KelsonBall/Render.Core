using Kelson.Common.Transforms;
using Kelson.Common.Vectors;
using System;

namespace Render.Core.GraphicsInterface
{
    public class ShaderProgram : IManagedAssetHandle
    {

        internal ShaderProgram(ManagedGraphicsService graphics, string vertexSource, string fragmentSource)
        {
            using (var vert = graphics.CreateVertexShader(vertexSource))
            using (var frag = graphics.CreateFragmentShader(fragmentSource))
            {
                handle = graphics.gl.CreateProgram();
                graphics.gl.AttachShader(handle, vert.Handle);
                graphics.gl.AttachShader(handle, frag.Handle);
                graphics.gl.LinkProgram(handle);
            }
        }

        private readonly ManagedGraphicsService graphics;
        public ManagedGraphicsService GraphicsService => throw new NotImplementedException();

        private readonly int handle;
        public int Handle => handle;

        public AssetBinding Binding()
        {
            graphics.gl.UseProgram(handle);
            return new AssetBinding(() => graphics.gl.UseProgram(0));
        }

        public void SetUniformFloat(string name, float value)
        {
            int loc = graphics.gl.GetUniformLocation(handle, name);
            graphics.gl.Uniform1(loc, value);
        }

        public void SetUniformInteger(string name, int value)
        {
            int loc = graphics.gl.GetUniformLocation(handle, name);
            graphics.gl.Uniform1(loc, value);
        }

        public void SetUniformTransform(string name, Transform transform)
        {
            int loc = graphics.gl.GetUniformLocation(handle, name);
            graphics.gl.UniformMatrix4(loc, 1, false, transform.AsSpan().ToArray());
        }

        public void SetUniformVector(string name, Vector4fd vector)
        {
            int loc = graphics.gl.GetUniformLocation(handle, name);
            graphics.gl.Uniform4(loc, 4, vector.AsSpan().ToArray());
        }

        //public void SetUniformTexture(string name, Texture texture)
        //{
        //    int loc = graphics.gl.GetUniformLocation(handle, name);
        //    graphics.gl.ActiveTexture(TextureUnit.Texture0);
        //    using (texture.Binding())
        //    {
        //        graphics.gl.
        //        graphics.gl.BindSampler(0, )
        //    }
        //}

        public void Dispose() => graphics.gl.DeleteShader(handle);
    }
}
