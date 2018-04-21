using OpenTK;
using System;
using System.ComponentModel;
using OpenTK.Input;
using OpenTK.Graphics;

namespace Render.Core.WindowInterface
{
    public class ApiWindow : GameWindow, IWindow
    {
        public new Rectangle ClientRectangle { get => base.ClientRectangle; }
        public new MouseDevice Mouse { get => base.Mouse; }
        public new KeyboardDevice Keyboard { get => base.Keyboard; }
        public new VSyncMode VSync { get => base.VSync; set => base.VSync = value; }
        public new int Width { get => base.Width; }
        public new int Height { get => base.Height; }

        public ApiWindow(int width, int height, GraphicsMode mode, string title) : base(width, height, mode, title)
        {
        }

        public event Action<EventArgs> OnClosedEvent;
        protected override void OnClosed(EventArgs e)
        {
            OnClosedEvent?.Invoke(e);
            base.OnClosed(e);
        }

        public event Action<CancelEventArgs> OnClosingEvent;
        protected override void OnClosing(CancelEventArgs e)
        {
            OnClosingEvent?.Invoke(e);
            base.OnClosing(e);
        }

        public event Action<EventArgs> OnFocusedChangedEvent;
        protected override void OnFocusedChanged(EventArgs e)
        {
            OnFocusedChangedEvent?.Invoke(e);
            base.OnFocusedChanged(e);
        }

        public event Action<EventArgs> OnIconChangedEvent;
        protected override void OnIconChanged(EventArgs e)
        {
            OnIconChangedEvent?.Invoke(e);
            base.OnIconChanged(e);
        }

        public event Action<KeyboardKeyEventArgs> OnKeyDownEvent;
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            OnKeyDownEvent?.Invoke(e);
            base.OnKeyDown(e);
        }

        public event Action<bool> DisposeEvent;
        protected override void Dispose(bool manual)
        {
            DisposeEvent?.Invoke(manual);
            base.Dispose(manual);
        }

        public event Action<EventArgs> OnDisposedEvent;
        protected override void OnDisposed(EventArgs e)
        {
            OnDisposedEvent?.Invoke(e);
            base.OnDisposed(e);
        }

        public event Action<KeyPressEventArgs> OnKeyPressEvent;
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            OnKeyPressEvent?.Invoke(e);
            base.OnKeyPress(e);
        }

        public event Action<KeyboardKeyEventArgs> OnKeyUpEvent;
        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            OnKeyUpEvent?.Invoke(e);
            base.OnKeyUp(e);
        }

        public event Action<EventArgs> OnLoadEvent;
        protected override void OnLoad(EventArgs e)
        {
            OnLoadEvent?.Invoke(e);
            base.OnLoad(e);
        }

        public event Action<MouseButtonEventArgs> OnMouseDownEvent;
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            OnMouseDownEvent?.Invoke(e);
            base.OnMouseDown(e);
        }

        public event Action<EventArgs> OnMouseEnterEvent;
        protected override void OnMouseEnter(EventArgs e)
        {
            OnMouseEnterEvent?.Invoke(e);
            base.OnMouseEnter(e);
        }

        public event Action<EventArgs> OnMouseLeaveEvent;
        protected override void OnMouseLeave(EventArgs e)
        {
            OnMouseLeaveEvent?.Invoke(e);
            base.OnMouseLeave(e);
        }

        public event Action<MouseMoveEventArgs> OnMouseMoveEvent;
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            OnMouseMoveEvent?.Invoke(e);
            base.OnMouseMove(e);
        }

        public event Action<MouseButtonEventArgs> OnMouseUpEvent;
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            OnMouseUpEvent?.Invoke(e);
            base.OnMouseUp(e);
        }

        public event Action<MouseWheelEventArgs> OnMouseWheelEvent;
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            OnMouseWheelEvent?.Invoke(e);
            base.OnMouseWheel(e);
        }

        public event Action<EventArgs> OnMoveEvent;
        protected override void OnMove(EventArgs e)
        {
            OnMoveEvent?.Invoke(e);
            base.OnMove(e);
        }

        public event Action<FrameEventArgs> OnRenderFrameEvent;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            OnRenderFrameEvent?.Invoke(e);
            base.OnRenderFrame(e);
        }

        public event Action<EventArgs> OnResizeEvent;
        protected override void OnResize(EventArgs e)
        {
            OnResizeEvent?.Invoke(e);
            base.OnResize(e);
        }

        public event Action<EventArgs> OnTitleChangedEvent;
        protected override void OnTitleChanged(EventArgs e)
        {
            OnTitleChangedEvent?.Invoke(e);
            base.OnTitleChanged(e);
        }

        public event Action<EventArgs> OnUnloadEvent;
        protected override void OnUnload(EventArgs e)
        {
            OnUnloadEvent?.Invoke(e);
            base.OnUnload(e);
        }

        public event Action<FrameEventArgs> OnUpdateFrameEvent;
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            OnUpdateFrameEvent?.Invoke(e);
            base.OnUpdateFrame(e);
        }

        public event Action<EventArgs> OnVisibleChangedEvent;
        protected override void OnVisibleChanged(EventArgs e)
        {
            OnVisibleChangedEvent?.Invoke(e);
            base.OnVisibleChanged(e);
        }

        public event Action<EventArgs> OnWindowBorderChangedEvent;
        protected override void OnWindowBorderChanged(EventArgs e)
        {
            OnWindowBorderChangedEvent?.Invoke(e);
            base.OnWindowBorderChanged(e);
        }

        public event Action<EventArgs> OnWindowInfoChangedEvent;
        protected override void OnWindowInfoChanged(EventArgs e)
        {
            OnWindowInfoChangedEvent?.Invoke(e);
            base.OnWindowInfoChanged(e);
        }

        public event Action<EventArgs> OnWindowStateChangedEvent;
        protected override void OnWindowStateChanged(EventArgs e)
        {
            OnWindowStateChangedEvent?.Invoke(e);
            base.OnWindowStateChanged(e);
        }

        public event Action ExitEvent;
        public override void Exit()
        {
            ExitEvent?.Invoke();
            base.Exit();
        }

        public void Run(int v) => Run((float)v);
    }
}
