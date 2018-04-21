using System;

namespace Render.Core.Input.Keyboard
{
    public interface IKeyboardDevice
    {
        uint KeyPressedTicks(Key key);
        double KeyPressedMilliseconds(Key key);
        bool KeyIsPressed(Key key);

        event Action<KeyDownArgs> KeyDown;
        event Action<KeyReleaseArgs> KeyReleased;

        void AddKeyCombo(params Key[] keys);
        event Action<KeyComboArgs> KeyCombo;
    }

    internal class KeyboardDevice : IKeyboardDevice
    {
        public event Action<KeyDownArgs> KeyDown;
        internal void InvokeKeyDown(KeyDownArgs e) => KeyDown?.Invoke(e);

        public event Action<KeyReleaseArgs> KeyReleased;
        internal void InvokeKeyReleased(KeyReleaseArgs e) => KeyReleased?.Invoke(e);

        public event Action<KeyComboArgs> KeyCombo;
        internal void InvokeKeyCombo(KeyComboArgs e) => KeyCombo?.Invoke(e);


        public void AddKeyCombo(params Key[] keys)
        {
            throw new NotImplementedException();
        }

        public bool KeyIsPressed(Key key)
        {
            throw new NotImplementedException();
        }

        public double KeyPressedMilliseconds(Key key)
        {
            throw new NotImplementedException();
        }

        public uint KeyPressedTicks(Key key)
        {
            throw new NotImplementedException();
        }
    }
}
