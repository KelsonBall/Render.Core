using KelsonBall.Render.Core.Math;
using Render.Core;
using Render.Core.Vectors;
using RenderCore.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;

namespace KelsonBall.LudumDare41.Items
{
    public class Ball : Physics2DObject
    {
        public readonly Stopwatch TimeSinceHit = new Stopwatch();
        public double AirDuration = 0;
        public Ball(Action<Ball> configure) : base(o => configure((Ball)o))
        {
            PhysicsBodyFactory = w =>
            {
                var body = BodyFactory.CreateCircle(w, 4, 0.1f, position: Position, bodyType: BodyType.Dynamic);
                body.CollisionCategories = Category.Cat10;
                body.LinearDamping = 0.1f;
                body.Restitution = 1;
                return body;
            };

            OnDraw += DrawBall;
        }

        public Rektor Aim(Rektor mouse) => (mouse - Position);

        public double MaxPower = 1000;
        public double MinPower = 50;
        public double PowerRadius = 80;

        public double Power(Rektor mouse) => Math.Max(Math.Min(Aim(mouse).Magnitude().ScaledAlong(end: PowerRadius) * MaxPower, MaxPower), MinPower);

        Queue<Rektor> punchQueue = new Queue<Rektor>();
        public void Launch(Rektor direction, double power)
        {
            TimeSinceHit.Restart();
            AirDuration = power.ScaledAlong(end: MaxPower) * 1000;
            for (int i = 0; i < 10; i++)
                punchQueue.Enqueue(direction * power);
        }

        private void DrawBall(ICanvas canvas)
        {
            canvas.WithStyle(() =>
            {
                canvas.Fill = Color.Constants.White;
                canvas.Ellipse((0, 0), (4, 4));
            });
        }

        public override void Update(TimeSpan time)
        {
            if (punchQueue.Count > 0)
                Body.ApplyLinearImpulse(punchQueue.Dequeue());
            if (TimeSinceHit.ElapsedMilliseconds < AirDuration)
            {
                var s = Math.Sin(((double)(TimeSinceHit.ElapsedMilliseconds)).ScaledAlong(end: AirDuration) * Math.PI);
                if (s > 0.3)
                    Body.CollidesWith = Category.Cat1; // tall bois only
                else
                    Body.CollidesWith = Category.All;
                Scale = (1 + s, 1 + s);
                Body.LinearDamping = 0.0f;
            }
            else
            {
                Scale = (1, 1);
                Body.LinearDamping = 0.2f;
            }
            base.Update(time);
        }
    }
}
