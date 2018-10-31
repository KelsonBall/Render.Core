using Kelson.Common.Vectors;
using Render.Core.GraphicsInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Render.Core.UI.Render
{
    public class Border : Quad
    {
        public Border(ManagedGraphicsService graphics) : base(graphics)
        {
        }

        [Uniform]
        public Vector4fd BorderThickness { get; set; } = (1, 1, 1, 1);

        [Uniform]
        public Vector3fd BorderColor { get; set; } = (0, 0, 0);

        [Uniform]
        public Vector3fd BackgroundColor { get; set; } = (1, 1, 1);
    }
}
