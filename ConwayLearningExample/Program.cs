// http://angusforbes.com/blog/openglglsl-render-to-texture/
using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ConwayLearningExample
{
    class Program
    {

        const string SHADER_SOURCE = @"
varying vec2 v_texCoord;

";

        static int CreateTexture(int width, int height)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out int texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)TextureWrapMode.ClampToEdge);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            return texture;
        }

        static byte[,] Noise(int width, int height)
        {
            var random = new Random();
            var data = new byte[width * 4, height * 4];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    if ()

        }

        static void Main(string[] args)
        {
            var textureA = CreateTexture(256, 256);
            var textureB = CreateTexture(new byte[])


            Console.WriteLine("Hello World!");
        }
    }
}
