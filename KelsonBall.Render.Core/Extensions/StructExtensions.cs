using System;
using System.Runtime.InteropServices;

namespace KelsonBall.Render.Core.Extensions
{
    public static class StructExtensions
    {
        public static T MarshalToStruct<T>(this byte[] data, int start = 0, int? length = null) where T : struct
        {
            length = length ?? data.Length;
            IntPtr dataPt = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, start, dataPt, length.Value);
            var castStruct = Marshal.PtrToStructure<T>(dataPt);
            Marshal.FreeHGlobal(dataPt);
            return castStruct;
        }
    }
}
