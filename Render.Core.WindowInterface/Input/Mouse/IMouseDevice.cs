using System;

namespace Render.Core.Input.Mouse
{
    public interface IMouseDevice
    {
        IMouseState Previous { get; }
        IMouseState State { get; }

        event Action<MouseMoveArgs> MouseMoved;
        event Action<MouseDownArgs> MouseButtonDown;
        event Action<MouseUpArgs> MouseButtonUp;
        event Action<MouseDragStartArgs> MouseDragStarted;
        event Action<MouseDragArgs> MouseDraging;
        event Action<MouseDragEndArgs> MouseDragEnding;
        event Action<MouseScrollArgs> MouseScrollEvent;

        void Update(IMouseState newState, ulong tick);
    }

    internal class MouseDevice : IMouseDevice
    {
        private readonly ulong[] buttons;

        public MouseDevice()
        {
            buttons = new ulong[13];
        }

        public IMouseState Previous { get; }
        public IMouseState State { get; }

        public event Action<MouseMoveArgs> MouseMoved;
        public event Action<MouseDownArgs> MouseButtonDown;
        public event Action<MouseUpArgs> MouseButtonUp;
        public event Action<MouseDragStartArgs> MouseDragStarted;
        public event Action<MouseDragArgs> MouseDraging;
        public event Action<MouseDragEndArgs> MouseDragEnding;
        public event Action<MouseScrollArgs> MouseScrollEvent;

        public void Update(IMouseState newState, ulong tick)
        {

        }
    }
}
