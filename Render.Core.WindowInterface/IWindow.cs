using System;
using System.ComponentModel;
using OpenTK;
using OpenTK.Input;

namespace Render.Core.WindowInterface
{
    public interface IWindow
    {
        Rectangle ClientRectangle { get; }
        MouseDevice Mouse { get; }
        KeyboardDevice Keyboard { get; }
        VSyncMode VSync { get; set; }
        int Width { get; }
        int Height { get; }

        event Action<bool> DisposeEvent;
        event Action ExitEvent;
        event Action<EventArgs> OnClosedEvent;
        event Action<CancelEventArgs> OnClosingEvent;
        event Action<EventArgs> OnDisposedEvent;
        event Action<EventArgs> OnFocusedChangedEvent;
        event Action<EventArgs> OnIconChangedEvent;
        event Action<KeyboardKeyEventArgs> OnKeyDownEvent;
        event Action<KeyPressEventArgs> OnKeyPressEvent;
        event Action<KeyboardKeyEventArgs> OnKeyUpEvent;
        event Action<EventArgs> OnLoadEvent;
        event Action<MouseButtonEventArgs> OnMouseDownEvent;
        event Action<EventArgs> OnMouseEnterEvent;
        event Action<EventArgs> OnMouseLeaveEvent;
        event Action<MouseMoveEventArgs> OnMouseMoveEvent;
        event Action<MouseButtonEventArgs> OnMouseUpEvent;
        event Action<MouseWheelEventArgs> OnMouseWheelEvent;
        event Action<EventArgs> OnMoveEvent;
        event Action<FrameEventArgs> OnRenderFrameEvent;
        event Action<EventArgs> OnResizeEvent;
        event Action<EventArgs> OnTitleChangedEvent;
        event Action<EventArgs> OnUnloadEvent;
        event Action<FrameEventArgs> OnUpdateFrameEvent;
        event Action<EventArgs> OnVisibleChangedEvent;
        event Action<EventArgs> OnWindowBorderChangedEvent;
        event Action<EventArgs> OnWindowInfoChangedEvent;
        event Action<EventArgs> OnWindowStateChangedEvent;

        void Exit();
        void Dispose();
        void SwapBuffers();
        void Run(int v);
    }
}