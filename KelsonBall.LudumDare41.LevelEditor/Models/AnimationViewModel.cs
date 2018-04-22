
using System;
using System.Text;
using System.Linq;
using KelsonBall.LudumDare41.Items.Models;

namespace KelsonBall.LudumDare41.LevelEditor
{
    public class AnimationViewModel : MapItemViewModel<Animation>
    {
        
        public System.Int32 Width 
        {
            get => data.Width;
            set => Set(() => data.Width = value);
        }

        public System.Int32 Height 
        {
            get => data.Height;
            set => Set(() => data.Height = value);
        }

        public System.Int32 Padding 
        {
            get => data.Padding;
            set => Set(() => data.Padding = value);
        }

        public System.Int32 FrameDuration 
        {
            get => data.FrameDuration;
            set => Set(() => data.FrameDuration = value);
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
