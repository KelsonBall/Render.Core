using System;
using System.Runtime.InteropServices;

namespace KelsonBall.Render.Core.Textures
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



        T MarshalToStruct<T>(byte[] data, int start = 0, int? length = null) where T : struct
        {
            length = length ?? data.Length;
            IntPtr dataPt = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, start, dataPt, length.Value);
            var castStruct = Marshal.PtrToStructure<T>(dataPt);
            Marshal.FreeHGlobal(dataPt);
            return castStruct;
        }

        public Bitmap(byte[] fileData)
        {
            FileData = MarshalToStruct<FileHeader>(fileData, length: 14);
            ImageData = MarshalToStruct<ImageHeader>(fileData, start: 14, length: 44);
            Pixels = new RGBA[ImageData.Width, ImageData.Height];
            int offset = (int)FileData.FileOffsetToPixelArray;
            int iHeight = 0;
            for (int i = 0; i < ImageData.Width * ImageData.Height * 3; i += 3)
            {
                byte b = fileData[i + offset + 2];
                byte r = fileData[i + offset + 0];
                byte g = fileData[i + offset + 1];
                if ((i / 3) % ImageData.Width == 0 && i > 0)
                    iHeight++;
                Pixels[(i / 3) % ImageData.Width, iHeight] = new RGBA(r, g, b, 0xFF);
            }
        }
    }
}
