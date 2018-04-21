using System;
using Render.Core;
using VelcroPhysics.Dynamics;

namespace RenderCore.Game
{
    public class Physics2DObject : ScreenObject
    {
        public Body Body;
        public Func<World, Body> BodyFactory;
        public Action<ICanvas> Draw;

        public Physics2DObject(Action<Physics2DObject> configure) : base(o => configure((Physics2DObject)o))
        {
        }

        public override void Render(ICanvas canvas)
        {
            canvas.MultMatrix(Transform.Matrix);
            Draw?.Invoke(canvas);
            foreach (var child in Children)
                child.Render(canvas);
            canvas.MultMatrix(Transform.Inverse);
        }
    }
}
