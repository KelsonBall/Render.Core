using OpenTK.Graphics;
using Render.Core.Input;
using Render.Core.Math;
using Render.Core.Rendering;
using System;

namespace Render.Core
{
    public interface ICanvas : IRenderer2d, IDisposable, IInputManager
    {
        Font Font { get; set; }
        Color Fill { get; set; }
        Color Stroke { get; set; }
        float StrokeWeight { get; set; }

        PVector MousePosition { get; }

        int Width { get; }
        int Height { get; }

        ulong FrameCount { get; set; }

        event Action<ICanvas> Setup;
        event Action<ICanvas> Draw;

        void WithStyle(Style style, Action action);
        void WithStyle(Action action);
        void PushStyle();
        void PushStyle(Style style);
        Style PopStyle();
        void Show();
    }
}
