
using System;
using System.Text;
using System.Linq;
using KelsonBall.LudumDare41.Items.Models;

namespace KelsonBall.LudumDare41.LevelEditor
{
    public class LaserHazardViewModel : MapItemViewModel<LaserHazard>
    {
        
        public System.Double XA 
        {
            get => data.XA;
            set => Set(() => data.XA = value);
        }
        public System.Double YA 
        {
            get => data.YA;
            set => Set(() => data.YA = value);
        }
        public System.Double XB 
        {
            get => data.XB;
            set => Set(() => data.XB = value);
        }
        public System.Double YB 
        {
            get => data.YB;
            set => Set(() => data.YB = value);
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
