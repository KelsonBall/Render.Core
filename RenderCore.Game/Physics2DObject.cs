using System;
using VelcroPhysics.Dynamics;

namespace RenderCore.Game
{
    public class Physics2DObject : ScreenObject
    {
        public Body Body;
        public Func<World, Body> PhysicsBodyFactory { private get; set; }
        public virtual Body GetPhysicsBody(World world) => (Body = PhysicsBodyFactory(world));

        public Physics2DObject(Action<Physics2DObject> configure) : base(o => configure((Physics2DObject)o))
        {
        }
    }
}
