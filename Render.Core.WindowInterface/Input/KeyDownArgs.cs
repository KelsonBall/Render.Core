namespace Render.Core.Input
{
    public class KeyDownArgs : KeyboardEventArgs
    {
        public readonly Key Key;

        public KeyDownArgs(IKeyboardState state, Key key) : base(state)
        {
            Key = key;
        }
    }
}