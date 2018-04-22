using Render.Core;
using Render.Core.Input;
using Render.Core.Vectors;
using RenderCore.Game;
using System;

namespace KelsonBall.LudumDare41.Scenes.Behaviors
{
    public static class BobBehavior
    {
        public static Behavior Get(Physics2DObject bob, ICanvas canvas, float maxVel, float rotationSpeed, Action<double> setSpeed)
        {
            bool hasMoved = false;
            return new Behavior
            {
                Name = "Bob Behavior",
                Load = () => bob.Body.AngularDamping = 1f,
                Update = (TimeSpan time) =>
                {
                    if (bob.Scene != null)
                        bob.Scene.Camera.Position = bob.Position;
                    bob.Rotation = 0;
                    Rektor velocity = bob.Body.LinearVelocity;
                    var direction = new Rektor(0, 0);
                    if (canvas.Keyboard.KeyIsPressed(Key.W))
                        direction += (0, -1);
                    if (canvas.Keyboard.KeyIsPressed(Key.S))
                        direction += (0, 1);
                    if (canvas.Keyboard.KeyIsPressed(Key.A))
                    {
                        direction += (-1, 0);
                        bob.Scale = (-1, 1);
                    }
                    if (canvas.Keyboard.KeyIsPressed(Key.D))
                    {
                        direction += (1, 0);
                        bob.Scale = (1, 1);
                    }
                    if (direction.MagnitudeSquared() == 0)
                        bob.Body.ApplyLinearImpulse(-bob.Body.LinearVelocity);
                    else
                        bob.Body.ApplyLinearImpulse(direction * maxVel);

                },
            };
        }
    }
}
