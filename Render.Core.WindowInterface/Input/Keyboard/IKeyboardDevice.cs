using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        private readonly Dictionary<Key, KeyStateRecord> keyStates = new Dictionary<Key, KeyStateRecord>();

        public event Action<KeyDownArgs> KeyDown;
        internal void InvokeKeyDown(KeyDownArgs e)
        {
            keyStates[e.Key].IsPressed = true;
            keyStates[e.Key].StateDuration.Restart();
            KeyDown?.Invoke(e);
        }

        public event Action<KeyReleaseArgs> KeyReleased;
        internal void InvokeKeyReleased(KeyReleaseArgs e)
        {
            keyStates[e.Key].IsPressed = false;
            keyStates[e.Key].StateDuration.Restart();
            KeyReleased?.Invoke(e);
        }

        public event Action<KeyComboArgs> KeyCombo;
        internal void InvokeKeyCombo(KeyComboArgs e)
        {
            KeyCombo?.Invoke(e);
        }


        public void AddKeyCombo(params Key[] keys)
        {
            throw new NotImplementedException();
        }

        public bool KeyIsPressed(Key key) => keyStates[key].IsPressed;

        public double KeyPressedMilliseconds(Key key)
        {
            throw new NotImplementedException();
        }

        public uint KeyPressedTicks(Key key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set initial keyboard state
        /// </summary>
        /// <param name="state"></param>
        internal void SetKeyboardState(KeyboardState state)
        {
            foreach (var key in typeof(Render.Core.Input.Key).GetEnumValues())
            {
                keyStates[(Render.Core.Input.Key)key] = new KeyStateRecord
                {
                    Key = (Render.Core.Input.Key)key,
                    IsPressed = state[(OpenTK.Input.Key)key],
                };
                keyStates[(Render.Core.Input.Key)key].StateDuration.Start();
            }
        }
    }

    internal class KeyStateRecord
    {
        internal Render.Core.Input.Key Key { get; set; }
        internal Stopwatch StateDuration { get; set; } = new Stopwatch();
        internal bool IsPressed { get; set; } = false;

    }
}
