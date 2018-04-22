using Render.Core.Transforms;
using System;
using Render.Core;
using Render.Core.Vectors;
using MathNet.Numerics.LinearAlgebra;

namespace RenderCore.Game
{
    public class ScreenObject : GameObject
    {
        protected readonly OrthoFrame Transform = OrthoFrame.Unit;

        public virtual Rektor Position { get => Transform.Translation; set => Transform.Translation = value; }
        public virtual double Rotation { get => Transform.Rotation; set => Transform.Rotation = value; }
        public virtual Rektor Scale { get => Transform.Scale; set => Transform.Scale = value; }
        public virtual Rektor Forward => Transform.Forward;
        public virtual Rektor Left => Transform.Left;

        public Action<ICanvas> OnDraw;

        public ScreenObject(Action<ScreenObject> configure) : base(o => configure((ScreenObject)o))
        {
        }

        private Matrix<double> inverse;
        public virtual void PushMatrix(ICanvas canvas)
        {
            inverse = Transform.Inverse;
            canvas.MultMatrix(Transform.Matrix);
        }

        public virtual void PopMatrix(ICanvas canvas)
        {
            canvas.MultMatrix(inverse);
            inverse = null;
        }

        public override void Render(ICanvas canvas)
        {
            PushMatrix(canvas);
            OnDraw?.Invoke(canvas);
            base.Render(canvas);
            PopMatrix(canvas);
        }

        public virtual bool IsMouseOver(Rektor mouse)
        {
            return false;
        }
    }
}
