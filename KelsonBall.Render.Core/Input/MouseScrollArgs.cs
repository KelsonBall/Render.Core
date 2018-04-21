namespace Render.Core.Input
{
    public class MouseScrollArgs : MouseEventArgs
    {
        public readonly double Delta;
        public readonly double Value;

        public MouseScrollArgs(double delta, double value)
        {
            Delta = delta;
            Value = value;
        }
    }
}