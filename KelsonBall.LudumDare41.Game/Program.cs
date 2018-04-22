using KelsonBall.LudumDare41.Assets;
using KelsonBall.LudumDare41.Scenes.Levels;
using Render.Core;
using Render.Core.Textures;
using RenderCore.Game;
using System;

namespace KelsonBall.LudumDare41.Game
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ResourceManager.RegisterResourceAssembly("Assets", "KelsonBall.LudumDare41.Assets", Art.Assembly);
            using (var canvas = Window.New(800, 600))
            {
                var scenes = new SceneManager(canvas);
                scenes.AddSceneFactory("Ball", () => BallLevel.GetLevel1(canvas));
                scenes.OpenScene("Ball");

                canvas.Show();
            }
            Texture.DeleteAll();
        }
    }
}
