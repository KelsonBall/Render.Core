using Render.Core.Transforms;
using System;
using Render.Core;
using Render.Core.Vectors;

namespace RenderCore.Game
{
    public class ScreenObject : GameObject
    {
        public readonly OrthoFrame Transform = OrthoFrame.Unit;
        public Action<ICanvas> OnDraw;

        public ScreenObject(Action<ScreenObject> configure) : base(o => configure((ScreenObject)o))
        {
        }

        public virtual void PushMatrix(ICanvas canvas)
        {
            canvas.PushFrame(Transform);
        }

        public virtual void PopMatrix(ICanvas canvas)
        {
            canvas.PopFrame(Transform);
        }

        public override void Render(ICanvas canvas)
        {
            PushMatrix(canvas);
            OnDraw?.Invoke(canvas);
            base.Render(canvas);
            PopMatrix(canvas);
        }

        public virtual bool IsMouseOver(rVector mouse)
        {
            return false;
        }
    }
}
