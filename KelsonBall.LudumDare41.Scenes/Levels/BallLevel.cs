using KelsonBall.Render.Core.Math;
using Render.Core.Textures;
using Render.Core;
using Render.Core.Input;
using Render.Core.Vectors;
using RenderCore.Game;
using System;
using System.Diagnostics;
using KelsonBall.LudumDare41.Items;

namespace KelsonBall.LudumDare41.Scenes.Levels
{
    public partial class BallLevel : Scene
    {
        public readonly Texture hazard = new Texture(new Bitmap(ResourceManager.Get("Assets.Packed.Hazard.bmp")).Pixels);
        public readonly Texture test = new Texture(new Bitmap(ResourceManager.Get("Assets.Packed.Data.bmp")).Pixels);
        public readonly Texture map1 = new Texture(new Bitmap(ResourceManager.Get("Assets.Packed.Map1.bmp")).Pixels);
        public readonly Texture BobsCharacter = new Texture(new TgaImage(ResourceManager.Get("Assets.Packed.CharacterIdle.tga")).Pixels);
        public readonly Texture BobSwing = new Texture(new TgaImage(ResourceManager.Get("Assets.Packed.CharacterSwing.tga")).Pixels);
        public readonly Font arialLarge = new Font(Font.Options.Arial, 20);
        public readonly Stopwatch Timer_GolfSwing = new Stopwatch();
        public readonly double Cooldown_GolfSwing = 500;

        public Ball GolfBall = null;
        public Physics2DObject Bob = null;

        public BallLevel(Action<BallLevel> configuration) : base(o => configuration((BallLevel)o))
        {
        }

        //

        private void CastGolfSwing(Key key, Rektor mouse)
        {
            if (CanUseCooldown(key == Key.Space, Timer_GolfSwing, Cooldown_GolfSwing) && BallProximity() <= 40)
            {
                UseCooldown(Timer_GolfSwing);
                if (Camera.MousePosition.X > Bob.Position.X)
                    Bob.Scale = (1, 1);
                else
                    Bob.Scale = (-1, 1);
                GolfBall.Launch(GolfBall.Aim(mouse).Unit(), GolfBall.Power(mouse));
            }
        }

        private double BallProximity() => (this.Bob.Position - this.GolfBall.Position).Magnitude();

    }
}
