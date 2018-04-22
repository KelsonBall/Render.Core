using Render.Core;
using Render.Core.Vectors;
using System;

namespace RenderCore.Game
{
    public class Camera : ScreenObject
    {
        public Rektor MousePosition { get; private set; }

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
            MousePosition = canvas.MousePosition + Position;
            var matrix = Transform.Matrix;
            canvas.MultMatrix(Transform.Inverse);
            base.Render(canvas);
            canvas.MultMatrix(matrix);
        }
    }
}
