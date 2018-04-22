using KelsonBall.LudumDare41.Items;
using KelsonBall.LudumDare41.Scenes.Behaviors;
using KelsonBall.Render.Core.Math;
using Render.Core;
using Render.Core.Input;
using Render.Core.Vectors;
using RenderCore.Game;
using System;
using VelcroPhysics.Collision.Filtering;
using VelcroPhysics.Dynamics;
using VelcroPhysics.Factories;

namespace KelsonBall.LudumDare41.Scenes.Levels
{
    public partial class BallLevel
    {


        public static Scene GetLevel1(ICanvas canvas)
        {
            var level = new BallLevel(_ =>
            {
                canvas.Keyboard.KeyDown += k =>
                {
                    _.CastGolfSwing(k.Key, _.Camera.MousePosition);
                };

                _.OnDraw += __ =>
                canvas.WithStyle(() =>
                    {
                        canvas.Image(_.map1, -_.Camera.Position);
                        //canvas.Fill = Color.Constants.White;
                        //canvas.Ellipse(canvas.MousePosition, (5, 5));
                    });

                _.Camera = new Camera(MainCamera =>
                {
                    MainCamera.AddBehavior(new Behavior
                    {
                        Name = "Camera Behavior",
                        Load = () => { },
                        Update = (TimeSpan time) =>
                        {
                            if (canvas.Keyboard.KeyIsPressed(Key.Q))
                                MainCamera.Rotation += -.05;
                            if (canvas.Keyboard.KeyIsPressed(Key.E))
                                MainCamera.Rotation += .05;

                        },
                    });
                });
            });

            level.Add(level.Camera);

            var host = new Physics2DHost(h => { });
            level.Camera.Add(host);
            host.Add(new Physics2DObject(r =>
                        {
                            r.PhysicsBodyFactory = w =>
                            {
                                var body = BodyFactory.CreateRectangle(w, 100, 10, 1, position: new Rektor(-50, 30));
                                body.CollisionCategories = Category.All & ~Category.Cat1;
                                body.Restitution = 1;
                                return body;
                            };
                            r.Position += (-50, 30);
                            r.OnDraw += _ => canvas.Image(level.hazard, (0, 0));
                        }));
            host.Add(new Physics2DObject(bob =>
                    {
                        Rektor position = (-130, -50);
                        level.Bob = bob;
                        bob.PhysicsBodyFactory = w =>
                        {
                            var body = BodyFactory.CreateRectangle(w, 30, 9, 0.01f, position: position, bodyType: BodyType.Dynamic);
                            body.CollisionCategories = Category.All & ~Category.Cat10 & ~Category.Cat1;
                            body.CollidesWith = Category.All & ~Category.Cat10;
                            body.Restitution = 1;
                            return body;
                        };
                        bob.Position = position;
                        double speed = 0;
                        bob.OnDraw += _ =>
                            canvas.WithStyle(() =>
                            {
                                if (CanUseCooldown(true, level.Timer_GolfSwing, level.Cooldown_GolfSwing))
                                    canvas.Image(level.BobsCharacter, (0, -20));
                                else
                                    canvas.Image(level.BobSwing, (0, -20));
                            });

                        bob.AddBehavior(BobBehavior.Get(bob, canvas, 50, 0.1f, s => speed = s));
                    }));

            host.Add(
                    new ScreenObject(powerBar =>
                    {
                        powerBar.OnDraw = _ =>
                            canvas.WithStyle(() =>
                            {
                                if (!CanUseCooldown(true, level.Timer_GolfSwing, level.Cooldown_GolfSwing) || level.BallProximity() > 40)
                                    return;
                                powerBar.Position = level.GolfBall.Position;
                                powerBar.Rotation = (powerBar.Position - level.Camera.MousePosition).Angle();
                                var power = level.GolfBall.Power(level.Camera.MousePosition);
                                var percentPower = power.ScaledAlong(end: level.GolfBall.MaxPower);
                                var length = Math.Max(percentPower * level.GolfBall.PowerRadius, 10);
                                var r = (byte)(percentPower * 0xFF);
                                var b = (byte)((1 - percentPower) * 0xFF);
                                var g = (byte)((1 - percentPower) * 0x80);
                                canvas.Fill = new Color(r, g, b, 0xFF);
                                canvas.Rectangle((-10 - length / 2, 0), (length, 5));
                                canvas.Fill = Color.Constants.Black;
                                canvas.Font = level.arialLarge;
                                //canvas.Text($"p: {Math.Round(power)}", (-30, -24));
                            });
                    }));

            host.Add(new Ball(ball =>
                    {
                        ball.Position = (-110, -57);
                        level.GolfBall = ball;
                    }));
            return level;
        }
    }
}

