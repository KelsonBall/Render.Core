using Render.Core.Vectors;
using System;
using VelcroPhysics.Dynamics;

namespace RenderCore.Game
{
    public class Physics2DObject : ScreenObject
    {
        public Body Body;
        public Func<World, Body> PhysicsBodyFactory { private get; set; }
        public virtual Body GetPhysicsBody(World world) => (Body = PhysicsBodyFactory(world));

        public override Rektor Position
        {
            get => Transform.Translation;
            set
            {
                Transform.Translation = value;
                if (Body != null)
                    Body.Position = value;
            }
        }

        public override double Rotation
        {
            get => Transform.Rotation;
            set
            {
                Transform.Rotation = value;
                if (Body != null)
                    Body.Rotation = (float)value;
            }
        }

        public Physics2DObject(Action<Physics2DObject> configure) : base(o => configure((Physics2DObject)o))
        {
        }
    }
}
