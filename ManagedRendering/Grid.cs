using Kelson.Common.Vectors;
using Render.Core.GraphicsInterface;
using Render.Core.GraphicsInterfaces;
using System;

namespace ManagedRendering
{
    public class Grid : Quad
    {        
        [Uniform]
        public Vector4fd GridColor { get; set; } = (1f, 0f, 1f, 1f);                
        [Uniform]
        public float Width { get; set; } = 20;
        [Uniform]
        public float Thickness { get; set; } = 2;

        public Grid(ManagedGraphicsService graphics) : base(graphics)
        {
        }

        public override void Update(TimeSpan time)
        {
        }
    }
}
