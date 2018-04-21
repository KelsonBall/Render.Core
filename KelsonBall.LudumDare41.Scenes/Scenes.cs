using Render.Core;
using RenderCore.Game;
using Render.Core.Input;
using System;

namespace KelsonBall.LudumDare41.Scenes
{
    public static class Scenes
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
                    MainCamera.AddBehavior(new CameraBehavior(canvas));
                })
                {
                    new ScreenObject(s =>
                    {
                        s.Draw
                    })
                };
            });

        public class CameraBehavior : IBehavior
        {
            public string Name { get; set; } = "Camera Behavior";
            public bool Active { get; set; }

            private readonly ICanvas canvas;
            private Camera camera;

            public CameraBehavior(ICanvas canvas)
            {
                this.canvas = canvas;
            }



            public void InvokeLoad(GameObject self, TimeSpan time)
            {
                camera = (Camera)self;
            }

            public void InvokeUpdate(GameObject self, TimeSpan time)
            {
                //throw new NotImplementedException();
                if (canvas.Keyboard.KeyIsPressed(Key.W))
                    camera.Transform.TranslateBy((0, 1));
                if (canvas.Keyboard.KeyIsPressed(Key.S))
                    camera.Transform.TranslateBy((0, -1));
                if (canvas.Keyboard.KeyIsPressed(Key.A))
                    camera.Transform.TranslateBy((1, 0));
                if (canvas.Keyboard.KeyIsPressed(Key.D))
                    camera.Transform.TranslateBy((-1, 0));
            }
        }
    }
}
