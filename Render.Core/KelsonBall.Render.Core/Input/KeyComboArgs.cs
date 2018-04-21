namespace Render.Core.Input
{
    public class KeyComboArgs
    {
        public readonly Key[] Keys;

        public KeyComboArgs(params Key[] keys)
        {
            Keys = keys;
        }
    }
}