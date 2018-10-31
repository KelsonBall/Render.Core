using System;
using System.ComponentModel;
using OpenTK;
using OpenTK.Input;
using Render.Core.Input.Keyboard;
using Render.Core.Input.Mouse;

namespace Render.Core.WindowInterface
{
    public interface IWindow
    {
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
        
        event Action<EventArgs> OnLoadEvent;        
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