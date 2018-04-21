using KelsonBall.LudumDare41.Scenes;
using Render.Core;
using RenderCore.Game;

namespace KelsonBall.LudumDare41.Game
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var canvas = Window.New(800, 680))
            {
                var scenes = new SceneManager(canvas);
                scenes.AddSceneFactory("Main", () => Scenes.Demo(canvas));
                scenes.OpenScene("Main");

                canvas.Show();
            }
        }
    }
}
