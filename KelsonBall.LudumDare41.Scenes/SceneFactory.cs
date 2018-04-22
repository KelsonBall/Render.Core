using Render.Core;
using RenderCore.Game;
using Render.Core.Input;
using System;

namespace KelsonBall.LudumDare41.Scenes
{
    public static class SceneFactory
    {
        public static Scene PlanetRunner(ICanvas canvas)
        {
            throw new NotImplementedException();
        }

        public static Scene Demo(ICanvas canvas)
            => new Scene(MainScene =>
            {
                MainScene.Camera = new Camera(MainCamera =>
                {
                    MainCamera.AddBehavior(new Behavior
                    {
                        Name = "Camera Behavior",
                        Load = () => { },
                        Update = (TimeSpan time) =>
                        {
                            if (canvas.Keyboard.KeyIsPressed(Key.Q))
                                MainCamera.Transform.RotateBy(-.05);
                            if (canvas.Keyboard.KeyIsPressed(Key.E))
                                MainCamera.Transform.RotateBy(.05);

                        },
                    });
                })
                {
                    new ScreenObject(s =>
                    {
                        s.Transform.TranslateBy(100, 100);
                        s.OnDraw += _ =>
                            canvas.WithStyle(() =>
                            {
                                canvas.Fill = Color.Constants.Red;
                                canvas.Ellipse((0, 0), (10, 10));
                            });
                    }),
                    new ScreenObject(s =>
                    {
                        s.Transform.TranslateBy(0, 100);
                        s.OnDraw += _ =>
                            canvas.WithStyle(() =>
                            {
                                canvas.Fill = Color.Constants.Blue;
                                canvas.Ellipse((0, 0), (10, 10));
                            });
                    }),
                    new ScreenObject(s =>
                    {
                        s.Transform.TranslateBy(100, 0);
                        s.OnDraw += _ =>
                            canvas.WithStyle(() =>
                            {
                                canvas.Fill = Color.Constants.Green;
                                canvas.Ellipse((0, 0), (10, 10));
                            });
                    }),
                    new ScreenObject(s =>
                    {
                        s.Transform.TranslateBy(100, 0);
                        s.OnDraw += _ =>
                            canvas.WithStyle(() =>
                            {
                                canvas.Fill = Color.Constants.White;
                                canvas.Line((0, 0), (-100, -100));
                                canvas.Ellipse((-100, -100), (10, 10));
                            });
                    }),
                    new ScreenObject(bob =>
                    {
                        bob.OnDraw += _ =>
                            canvas.WithStyle(() =>
                            {
                                canvas.Fill = Color.Constants.Crimson;
                                canvas.Triangle((-5, 5), (-5, -5), (7, 0));
                            });

                        bob.AddBehavior(new Behavior
                        {
                            Name = "Bob Behavior",
                            Update = (TimeSpan time) =>
                            {
                                if (canvas.Keyboard.KeyIsPressed(Key.W))
                                    bob.Transform.TranslateBy(bob.Transform.Forward);
                                if (canvas.Keyboard.KeyIsPressed(Key.S))
                                    bob.Transform.TranslateBy(-bob.Transform.Forward);
                                if (canvas.Keyboard.KeyIsPressed(Key.A))
                                    bob.Transform.RotateBy(-.1);
                                if (canvas.Keyboard.KeyIsPressed(Key.D))
                                    bob.Transform.RotateBy(0.1);
                            },

                        });
                    })
                };
            });
    }
}
