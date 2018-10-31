using Render.Core.GraphicsInterface;
using Render.Core.GraphicsInterfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ManagedRendering
{
    public class Gradient : Quad
    {
        private string _source;
        public override async Task<string> Source()
        {            
            if (string.IsNullOrEmpty(_source))
                using (var reader = new StreamReader(typeof(Gradient).Assembly.GetManifestResourceStream(typeof(Gradient).FullName + ".glsl")))
                    return (_source = await reader.ReadToEndAsync());
            else
                return _source;            
        }

        public Gradient(ManagedGraphicsService graphics) : base(graphics)
        {

        }

        public override void Update(TimeSpan time)
        {
            throw new NotImplementedException();
        }
    }
}
