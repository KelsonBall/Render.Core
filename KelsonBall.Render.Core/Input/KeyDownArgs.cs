namespace Render.Core.Input
{
    public class KeyDownArgs : KeyboardEventArgs
    {
        public readonly Key Key;

        public KeyDownArgs(Key key)
        {
            Key = key;
        }
    }
}