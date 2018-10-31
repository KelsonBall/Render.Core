
namespace Render.Core.Input
{
    public abstract class KeyboardEventArgs
    {
        public readonly IKeyboardState State;

        protected KeyboardEventArgs(IKeyboardState state) => State = state;
    }
}
