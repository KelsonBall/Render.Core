using MathNet.Numerics.LinearAlgebra;
using Render.Core;
using Render.Core.Transforms;
using System;

namespace RenderCore.Game
{
    public class Camera : ScreenObject
    {
        public Camera(Action<Camera> configure) : base(o => configure((Camera)o))
        {
        }

        public override void PushMatrix(ICanvas canvas)
        {
            //base.PushMatrix(canvas);
        }

        public override void PopMatrix(ICanvas canvas)
        {
            //base.PopMatrix(canvas);
        }

        public override void Render(ICanvas canvas)
        {
            canvas.MultMatrix(Transform.Inverse);
            base.Render(canvas);
            canvas.MultMatrix(Transform.Matrix);
        }
    }
}
