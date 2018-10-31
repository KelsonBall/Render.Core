namespace Render.Core.Input
{
    public class MouseScrollArgs : MouseEventArgs
    {
        public readonly double Delta;
        public readonly double Value;

        public MouseScrollArgs(IMouseState state, double delta, double value) : base(state) =>
            (Delta, Value) = (delta, value);
    }
}