using KelsonBall.Render.Core.Extensions;
using System;
using System.Runtime.InteropServices;
using static Render.Core.Textures.TgaImage.ImageHeader;

namespace Render.Core.Textures
{
    /// <summary>
    /// I was not expecting to write this kind of code during a game jam
    /// #JustDotNetCoreThings
    /// </summary>
    public class TgaImage
    {
        public ImageHeader Header;
        public RGBA[,] Pixels;

        [StructLayout(LayoutKind.Explicit)]
        public struct ImageHeader
        {
            [Flags]
            public enum Desciptor : byte
            {

                Attr_Zero         = 0,
                Attr_One          = 1,
                Attr_Two          = 2,
                Attr_Three        = 3,
                Attr_Four         = 4,
                // . . .

                ScreenOriginLower = 0b0000_0000,
                ScreenOriginUpper = 0b0001_0000,

                NonInterleaved    = 0b0000_0000,
                TwoWayInterleave  = 0b0010_0000,
                FourWayInterleave = 0b0100_0000,
                Reserved          = 0b0110_0000,

            }

            int AttributeBits(Desciptor descriptor) => ((byte)descriptor) & 0b1111;

            [FieldOffset(0)]
            public byte IdLength;
            [FieldOffset(1)]
            public byte ColorMapType;
            [FieldOffset(2)]
            public byte ImageType;
            [FieldOffset(3)]
            public ushort ColorMapStart;
            [FieldOffset(5)]
            public ushort ColorMapLength;
            [FieldOffset(7)]
            public byte ColorMapDepth;
            [FieldOffset(8)]
            public ushort XOrigin;
            [FieldOffset(10)]
            public ushort YOrigin;
            [FieldOffset(12)]
            public ushort Width;
            [FieldOffset(14)]
            public ushort Height;
            [FieldOffset(16)]
            public byte PixelDepth;
            [FieldOffset(17)]
            public Desciptor ImageDescriptor;
        }

        public TgaImage(byte[] data)
        {
            Header = data.MarshalToStruct<ImageHeader>(length: 18);
            int width = (int)Header.Width;
            int height = (int)Header.Height;
            Pixels = new RGBA[width, height];
            int offset = 18 + Header.IdLength + Header.ColorMapLength;
            int pixelStride = Header.PixelDepth / 8;
            for (int i = 0; i < width * height * pixelStride; i += pixelStride)
            {
                byte r = data[i + offset + 2];
                byte g = data[i + offset + 0];
                byte b = data[i + offset + 1];
                byte a = pixelStride == 4 ? data[i + offset + 3] : (byte)0xFF;
                if ((Header.ImageDescriptor & Desciptor.ScreenOriginUpper) == Desciptor.ScreenOriginUpper)
                    Pixels[(i / pixelStride) % width, (i / pixelStride) / width] = new RGBA(r, g, b, a);
                else
                    Pixels[(i / pixelStride) % width, height - 1 - (i / pixelStride) / width] = new RGBA(r, g, b, a);
            }
        }
    }
}
