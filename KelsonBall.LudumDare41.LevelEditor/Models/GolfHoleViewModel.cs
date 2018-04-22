
using System;
using System.Text;
using System.Linq;
using KelsonBall.LudumDare41.Items.Models;

namespace KelsonBall.LudumDare41.LevelEditor
{
    public class GolfHoleViewModel : MapItemViewModel<GolfHole>
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

        public System.Double Radius 
        {
            get => data.Radius;
            set => Set(() => data.Radius = value);
        }

        public System.Double Gravity 
        {
            get => data.Gravity;
            set => Set(() => data.Gravity = value);
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
