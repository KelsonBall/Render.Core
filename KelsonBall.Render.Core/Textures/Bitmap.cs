using KelsonBall.Render.Core.Extensions;
using System.Runtime.InteropServices;

namespace Render.Core.Textures
{

    public class Bitmap
    {
        public FileHeader FileData;
        public ImageHeader ImageData;
        public RGBA[,] Pixels;

        [StructLayout(LayoutKind.Explicit)]
        public struct FileHeader
        {
            [FieldOffset(0)]
            public ushort Signature;
            [FieldOffset(2)]
            public uint FileSize;
            [FieldOffset(6)]
            public uint Reserved;
            [FieldOffset(10)]
            public uint FileOffsetToPixelArray;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct ImageHeader
        {
            [FieldOffset(0)]
            public uint HeaderSize;
            [FieldOffset(4)]
            public uint Width;
            [FieldOffset(8)]
            public uint Height;
            [FieldOffset(12)]
            public ushort ImagePlanes;
            [FieldOffset(14)]
            public ushort BitsPerPixel;
            [FieldOffset(16)]
            public uint Compression;
            [FieldOffset(24)]
            public uint SizeImage;
            [FieldOffset(28)]
            public uint XPelsPerMeter;
            [FieldOffset(32)]
            public uint YPelsPerMeter;
        }





        public Bitmap(byte[] fileData)
        {
            FileData = fileData.MarshalToStruct<FileHeader>(length: 14);
            ImageData = fileData.MarshalToStruct<ImageHeader>(start: 14, length: 44);
            int width = (int)ImageData.Width;
            int height = (int)ImageData.Height;
            Pixels = new RGBA[width, height];
            int offset = (int)FileData.FileOffsetToPixelArray;
            for (int i = 0; i < width * height * 3; i += 3)
            {
                byte r = fileData[i + offset + 2];
                byte g = fileData[i + offset + 0];
                byte b = fileData[i + offset + 1];
                Pixels[(i / 3) % width, height - 1 -(i / 3) / width] = new RGBA(r, g, b, 0xFF);
            }
        }
    }
}
