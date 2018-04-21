using Render.Core;
using RenderCore.Game;
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
                    MainCamera.AddBehavior(new CameraBehavior());
                })
                {
                    new Button
                }
            });

        public class CameraBehavior : IBehavior
        {
            public string Name { get; set; } = "Camera Behavior";
            public bool Active { get; set; }

            public void InvokeLoad(GameObject self, TimeSpan time)
            {
                throw new NotImplementedException();
            }

            public void InvokeUpdate(GameObject self, TimeSpan time)
            {
                throw new NotImplementedException();
            }
        }
    }
}
