namespace Render.Core.Extensions
{
    public static class IntegerExtensions
    {
        public static byte[] ToBytes(this int source) =>
            new byte[] {
                source.GetByte(0), source.GetByte(1),
                source.GetByte(2), source.GetByte(3)
            };

        public static byte GetByte(this int source, int index)
        {
            var shift = index * 8;
            int mask = 0xFF << shift;
            return (byte)((source & mask) >> shift);
        }

        public static int ToInteger(this byte[] bytes)
        {
            int shift = 0;
            int result = 0;
            foreach (var b in bytes)
            {
                result |= b << shift;
                shift += 8;
                if (shift == 32)
                    break;
            }
            return result;
        }

    }
}
