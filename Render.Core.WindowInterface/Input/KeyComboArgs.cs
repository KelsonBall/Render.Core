namespace Render.Core.Input
{
    public class KeyComboArgs : KeyboardEventArgs
    {
        public readonly Key[] Keys;

        public KeyComboArgs(IKeyboardState state, params Key[] keys) : base(state)
        {
            Keys = keys;
        }
    }
}