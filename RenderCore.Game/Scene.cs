using Render.Core;
using Render.Core.BadPuns;
using System;
using System.Linq;

namespace RenderCore.Game
{
    public class Scene : GameObject
    {
        public Camera Camera;

        public Scene(Action<Scene> configuration) : base(o => configuration((Scene)o))
        {
            if (Camera == null)
                throw a.fit("Camera must be initialized in scene configuration");
            this.Add(Camera);
        }

        public override void Add(GameObject child)
        {
            base.Add(child);
        }

        public override void Update(TimeSpan time)
        {
            foreach (var child in Children.Where(c => c != Camera))
                child.Update(time);
            foreach (var behavior in Behaviors)
                behavior.InvokeUpdate(this, time);
        }

        public override void Render(ICanvas canvas)
        {
            Camera.Render(canvas);
            foreach (var child in Children.Where(c => c != Camera))
                child.Render(canvas);
        }
    }
}
