
using System;
using System.Text;
using System.Linq;
using KelsonBall.LudumDare41.Items.Models;

namespace KelsonBall.LudumDare41.LevelEditor
{
    public class GaurdViewModel : MapItemViewModel<Gaurd>
    {
        
        public System.Double X 
        {
            get => data.X;
            set => Set(() => data.X = value);
        }
        public System.Double Y 
        {
            get => data.Y;
            set => Set(() => data.Y = value);
        }
        public System.Double VisionLength 
        {
            get => data.VisionLength;
            set => Set(() => data.VisionLength = value);
        }
        public System.Double VisionArc 
        {
            get => data.VisionArc;
            set => Set(() => data.VisionArc = value);
        }
        public System.Double ReactionTime 
        {
            get => data.ReactionTime;
            set => Set(() => data.ReactionTime = value);
        }
        public KelsonBall.LudumDare41.Items.Models.MobPath Path 
        {
            get => data.Path;
            set => Set(() => data.Path = value);
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
