using System;
using System.IO;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using KelsonBall.Render.Core.Textures;

namespace Render.Core.Textures
{
    public class rImage
    {
        private readonly RGBA[,] _source;
        public readonly uint Handle;
        public readonly int Width;
        public readonly int Height;
        public readonly int[] Data;

        public rImage(RGBA[,] source)
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

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            WithDataPtr(dataptr =>
            {
                GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, Width, Height, PixelFormat.Rgba, PixelType.UnsignedByte, dataptr);
            });

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public static implicit operator uint (rImage image) => image.Handle;

        public static rImage FromSteam(Func<Stream> streamSource)
        {
            throw new NotImplementedException();
            //using (var stream = streamSource())
            //    return FromImage(ImageIO);
        }

        //public static PImage FromFile(string file) => FromImage(ImageIO.LoadColor(file));

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
                                   | image[row,column].B << 16
                                   | image[row,column].G << 8
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
