using OpenTK.Input;

namespace Render.Core.Input
{
    public abstract class MouseEventArgs
    {
        public readonly IMouseState State;

        protected MouseEventArgs(IMouseState state) => State = state;
    }
}
