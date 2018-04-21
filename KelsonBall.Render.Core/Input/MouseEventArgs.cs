using OpenTK.Input;

namespace Render.Core.Input
{
    public abstract class MouseEventArgs
    {
        public MouseState Mouse { get; internal set; }
    }
}
