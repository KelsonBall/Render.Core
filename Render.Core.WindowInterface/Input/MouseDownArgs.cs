namespace Render.Core.Input
{
    public class MouseDownArgs : MouseEventArgs
    {
        public readonly MouseButton Button;

        public MouseDownArgs(IMouseState state, MouseButton button) : base(state)
        {
            Button = button;
        }
    }
}