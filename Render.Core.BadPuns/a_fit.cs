using System;
using System.Runtime.Serialization;

namespace Render.Core.BadPuns
{
    public class a : Exception
    {
        public a()
        {
        }

        public static a fit() => new a();

        public a(string message) : base(message)
        {
        }

        public static a fit(string message) => new a(message);

        public a(string message, Exception innerException) : base(message, innerException)
        {
        }

        public static a fit(string message, Exception innerException) => new a(message, innerException);

        protected a(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static a fit(SerializationInfo info, StreamingContext context) => new a(info, context);
    }
}
