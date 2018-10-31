namespace Render.Core.Input
{
    public class KeyReleaseArgs : KeyboardEventArgs
    {
        public readonly Key Key;
        public readonly uint Ticks;
        public readonly double Milliseconds;

        public KeyReleaseArgs(IKeyboardState state, Key key, uint ticks, double milliseconds) : base(state)
        {
            Key = key;            
            Ticks = ticks;
            Milliseconds = milliseconds;
        }
    }
}