using System;

namespace RenderCore.Game
{
    public interface IBehavior
    {
        string Name { get; set; }
        bool Active { get; set; }
        void InvokeLoad(GameObject self, TimeSpan time);
        void InvokeUpdate(GameObject self, TimeSpan time);
    }
}
