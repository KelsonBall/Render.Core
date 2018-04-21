namespace Render.Core.Input
{
    public class KeyReleaseArgs : KeyboardEventArgs
    {
        public readonly Key Key;
        public readonly uint Ticks;
        public readonly double Milliseconds;
    }
}