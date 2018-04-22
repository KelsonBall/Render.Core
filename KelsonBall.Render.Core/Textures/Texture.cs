using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using Render.Core.Vectors;
using System.Collections.Generic;

namespace Render.Core.Textures
{
    public class Texture
    {
        protected static readonly List<uint> _handles = new List<uint>();

        public static void DeleteAll()
        {
            foreach (var handle in _handles)
                GL.DeleteTexture(handle);
        }

        private readonly RGBA[,] _source;
        public readonly uint Handle;
        public readonly int Width;
        public readonly int Height;
        public Rektor Size => (Width, Height);
        public readonly int[] Data;

        public Texture(RGBA[,] source)
        {
            _source = source;
            Width = source.GetLength(0);
            Height = source.GetLength(1);
            Data = GetData(source);

            GL.Color4(Color4.White);
            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out Handle);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Nearest);
            float maxAniso;
            GL.GetFloat((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt, out maxAniso);
            GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, maxAniso);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            WithDataPtr(dataptr =>
            {
                GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, Width, Height, PixelFormat.Rgba, PixelType.UnsignedByte, dataptr);
            });

            GL.BindTexture(TextureTarget.Texture2D, 0);
            _handles.Add(Handle);
        }

        public static implicit operator uint (Texture image) => image.Handle;

        private static int[] GetData(RGBA[,] image)
        {
            var imHeight = image.GetLength(0);
            var imWidth = image.GetLength(1);
            int[] data = new int[imWidth * imHeight];
            int outIndex = 0;
            for (int column = 0; column < imWidth; column++)
            {
                for (int row = 0; row < imHeight; row++)
                {

                    data[outIndex] = image[row,column].A << 24
                                   | image[row,column].G << 16
                                   | image[row,column].B << 8
                                   | image[row,column].R;
                    outIndex++;
                }
            }

            return data;
        }

        public void WithDataPtr(Action<IntPtr> action)
        {
            unsafe
            {
                fixed (int* dataptr = &(Data[0]))
                {
                    action((IntPtr)dataptr);
                }
            }
        }
    }
}
