using Render.Core.Vectors;

namespace KelsonBall.LudumDare41.LevelEditor.Events
{
    public class MapClickedAtEvent
    {
        public Rektor Point;
        public MapClickedAtEvent(Rektor p) => Point = p;
    }
}
