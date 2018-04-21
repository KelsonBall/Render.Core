namespace Render.Core.Input
{
    public class MouseDownArgs : MouseEventArgs
    {
        public readonly MouseButton Button;

        public MouseDownArgs(MouseButton button)
        {
            Button = button;
        }
    }
}