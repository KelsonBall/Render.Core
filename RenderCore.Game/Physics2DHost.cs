using System;
using VelcroPhysics.Dynamics;
using Render.Core.Vectors;
using System.Collections.Generic;
using Render.Core;
using Render.Core.BadPuns;

namespace RenderCore.Game
{
    public class Physics2DHost : GameObject
    {
        public World Host;

        private readonly List<(Body body, Physics2DObject gameObject)> physicsMap = new List<(Body, Physics2DObject)>();

        public Physics2DHost(Action<Physics2DHost> configure) : base(o =>
        {
            ((Physics2DHost)o).Host = new World(new Rektor(0, 0));
            configure((Physics2DHost)o);
        })
        {
        }

        public override void Add(GameObject child)
        {
            if (child is Physics2DObject item)
                physicsMap.Add((item.GetPhysicsBody(Host), item));
            else
                throw a.fit();
            base.Add(child);
        }

        public override void Update(TimeSpan time)
        {
            Host.Step((float)time.TotalSeconds);
            foreach (var item in physicsMap)
            {
                item.gameObject.Position = item.body.Position;
                item.gameObject.Rotation = item.body.Rotation;
            }
            base.Update(time);
        }

        public override void Render(ICanvas canvas)
        {

            base.Render(canvas);
        }
    }
}
