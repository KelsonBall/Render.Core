using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Linq.Enumerable;

namespace Render.Core.Input.Mouse
{
    public interface IMouseDevice
    {
        MouseState State { get; }

        double X { get; }
        double Y { get; }

        double PreviousX { get; }
        double PreviousY { get; }

        bool IsDragging { get; }
        double DragStartX { get; }
        double DragStartY { get; }

        uint MouseButtonPressedTicks(MouseButton button);
        double MouseButtonPressedMilliseconds(MouseButton button);
        bool MouseButtonIsPressed(MouseButton button);

        event Action<MouseMoveArgs> MouseMoved;
        event Action<MouseDownArgs> MouseButtonDown;
        event Action<MouseUpArgs> MouseButtonUp;
        event Action<MouseDragStartArgs> MouseDragStarted;
        event Action<MouseDragArgs> MouseDraging;
        event Action<MouseDragEndArgs> MouseDragEnding;
        event Action<MouseScrollArgs> MouseScrollEvent;
    }

    internal class MouseDevice : IMouseDevice
    {
        public MouseDevice()
        {
            foreach (var button in Range(0, 13).Select(i => (MouseButton)i))
            {
                buttonPressed[button] = false;
                buttonTicks[button] = 0;
                buttonMs[button] = 0;
            }
        }

        public MouseState State { get; internal set; }

        public double X { get; internal set; }

        public double Y { get; internal set; }

        public double PreviousX { get; internal set; }

        public double PreviousY { get; internal set; }

        public bool IsDragging { get; set; }

        public double DragStartX { get; internal set; }

        public double DragStartY { get; internal set; }

        public event Action<MouseMoveArgs> MouseMoved;
        internal void InvokeMouseMoved(double x, double y, double dx, double dy, MouseState state)
        {
            State = state;
            PreviousX = X;
            PreviousY = Y;
            X = x;
            Y = y;
            MouseMoved?.Invoke(new MouseMoveArgs(x, y, dx, dy) { Mouse = state });
        }

        public event Action<MouseDownArgs> MouseButtonDown;
        internal void InvokeMouseButtonDown(MouseButton button, MouseState state)
        {
            State = state;
            buttonPressed[button] = true;
            buttonTicks[button] = 0;
            buttonMs[button] = 0;
            MouseButtonDown?.Invoke(new MouseDownArgs(button) { Mouse = state });
        }

        public event Action<MouseUpArgs> MouseButtonUp;
        internal void InvokeMouseButtonUp(MouseButton button, MouseState state)
        {
            State = state;
            MouseButtonUp?.Invoke(new MouseUpArgs(button, buttonTicks[button], buttonMs[button]) { Mouse = state });
        }

        public event Action<MouseDragStartArgs> MouseDragStarted;
        internal void Invoke(double x, double y, MouseState state)
        {
            DragStartX = x;
            DragStartY = y;
            IsDragging = true;
            MouseDragStarted?.Invoke(new MouseDragStartArgs(x, y) { Mouse = state });
        }

        public event Action<MouseDragArgs> MouseDraging;
        internal void InvokeMouseDraging(double x, double y, double dx, double dy, MouseState state)
        {
            throw new NotImplementedException();
            MouseDraging?.Invoke(new MouseDragArgs());
        }

        public event Action<MouseDragEndArgs> MouseDragEnding;
        internal void InvokeMouseDragEnding(MouseDragEndArgs e) => MouseDragEnding?.Invoke(e);

        public event Action<MouseScrollArgs> MouseScrollEvent;
        internal void InvokeMouseScrollEvent(MouseScrollArgs e) => MouseScrollEvent?.Invoke(e);

        private readonly Dictionary<MouseButton, uint> buttonTicks = new Dictionary<MouseButton, uint>();
        private readonly Dictionary<MouseButton, double> buttonMs = new Dictionary<MouseButton, double>();
        private readonly Dictionary<MouseButton, bool> buttonPressed = new Dictionary<MouseButton, bool>();

        internal void UpdatePressedButtons(TimeSpan time)
        {
            foreach (var button in buttonPressed.Where(kvp => kvp.Value).Select(kvp => kvp.Key))
            {
                buttonTicks[button]++;
                buttonMs[button] += time.Milliseconds;
            }
        }

        public bool MouseButtonIsPressed(MouseButton button) => buttonPressed[button];

        public double MouseButtonPressedMilliseconds(MouseButton button) => buttonMs[button];

        public uint MouseButtonPressedTicks(MouseButton button) => buttonTicks[button];
    }
}
