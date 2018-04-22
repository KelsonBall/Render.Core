using Render.Core;
using Render.Core.BadPuns;
using Render.Core.Textures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RenderCore.Game
{
    public class Scene : GameObject
    {
        public Camera Camera;
        public Action<ICanvas> OnDraw;

        public readonly Dictionary<string, Texture> Textures = new Dictionary<string, Texture>();
        public readonly Dictionary<string, Font> Fonts = new Dictionary<string, Font>();

        public Scene(Action<Scene> configuration) : base(o => configuration((Scene)o))
        {
            _root = this;
            if (Camera == null)
                throw a.fit("Camera must be initialized in scene configuration");
            this.Add(Camera);
        }

        public override void Add(GameObject child)
        {
            base.Add(child);
        }

        public virtual void LoadAssets()
        {

        }

        public static bool CanUseCooldown(bool condition, Stopwatch timer, double milliseconds)
            => condition && (!timer.IsRunning || timer.ElapsedMilliseconds >= milliseconds);

        public static void UseCooldown(Stopwatch timer)
            => timer.Restart();

        public override void Update(TimeSpan time)
        {
            Camera.Update(time);
            foreach (var child in Children.Where(c => c != Camera))
                child.Update(time);
            foreach (var behavior in Behaviors)
                behavior.Update?.Invoke(time);
        }

        public override void Render(ICanvas canvas)
        {
            OnDraw?.Invoke(canvas);
            Camera.Render(canvas);
            foreach (var child in Children.Where(c => c != Camera))
                child.Render(canvas);
        }
    }
}
