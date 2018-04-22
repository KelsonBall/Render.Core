using System;

namespace RenderCore.Game
{
    public class Behavior
    {
        public string Name { get; set; }
        public bool Active { get; set; } = true;

        public Action Load { get; set; }
        public Action<TimeSpan> Update { get; set; }
    }
}
