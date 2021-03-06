
using System;
using System.Text;
using System.Linq;
using KelsonBall.LudumDare41.Items.Models;

namespace KelsonBall.LudumDare41.LevelEditor
{
    public class RectangleTerrainViewModel : MapItemViewModel<RectangleTerrain>
    {
        
        public System.Double X 
        {
            get => data.X;
            set => Set(() => 
            {
                XAdjusted = value + XDisplayOffset;
                data.X = value;
            });
        }

        public System.Double Y 
        {
            get => data.Y;
            set => Set(() => 
            {
                YAdjusted = value + YDisplayOffset;
                data.Y = value;
            });
        }

        public System.Double Width 
        {
            get => data.Width;
            set => Set(() => data.Width = value);
        }

        public System.Double Height 
        {
            get => data.Height;
            set => Set(() => data.Height = value);
        }

        public System.Double Rotation 
        {
            get => data.Rotation;
            set => Set(() => data.Rotation = value);
        }

        public System.Double Bounce 
        {
            get => data.Bounce;
            set => Set(() => data.Bounce = value);
        }

        public System.Double SoundLevel 
        {
            get => data.SoundLevel;
            set => Set(() => data.SoundLevel = value);
        }

        
        public string Tags
        {
            get => string.Join(", ", data.Tags);
            set => Set(() => data.Tags = value.Split(',').Select(t => t.Trim()).ToArray());
        }

        public string CollisionGroups
        {
            get => string.Join(", ", data.CollisionGroups);
            set => Set(() => data.CollisionGroups = value.Split(',').Select(t => t.Trim()).ToArray());
        }

    }
}
