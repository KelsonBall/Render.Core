using OpenTK.Input;

namespace Render.Core.Input
{
    public abstract class KeyboardEventArgs
    {
        public KeyboardState Keyboard { get; internal set; }
    }
}
