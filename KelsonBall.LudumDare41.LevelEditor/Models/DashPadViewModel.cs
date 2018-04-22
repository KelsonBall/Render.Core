
using System;
using System.Text;
using System.Linq;
using KelsonBall.LudumDare41.Items.Models;

namespace KelsonBall.LudumDare41.LevelEditor
{
    public class DashPadViewModel : MapItemViewModel<DashPad>
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

        public System.Double Rotation 
        {
            get => data.Rotation;
            set => Set(() => data.Rotation = value);
        }

        public System.Double Strength 
        {
            get => data.Strength;
            set => Set(() => data.Strength = value);
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