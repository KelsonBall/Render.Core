using Render.Core.Transforms;
using MathNet.Numerics.LinearAlgebra;
using Render.Core.Input;
using Render.Core.Rendering;
using Render.Core.Vectors;
using System;

namespace Render.Core
{
    public interface ICanvas : IRenderer2d, IDisposable, IInputManager
    {
        Font Font { get; set; }
        Color Fill { get; set; }
        Color Stroke { get; set; }
        float StrokeWeight { get; set; }

        Rektor MousePosition { get; }

        int Width { get; }
        int Height { get; }

        ulong FrameCount { get; set; }

        event Action<ICanvas> Setup;
        event Action<double> Step;
        event Action<ICanvas> Draw;

        void WithStyle(Style style, Action action);
        void WithStyle(Action action);
        void PushStyle();
        void PushStyle(Style style);
        Style PopStyle();

        void PushFrame(OrthoFrame frame);
        void PopFrame(OrthoFrame frame);
        void LoadMatrix(Matrix<double> matrix);
        void MultMatrix(Matrix<double> matrix);

        void Show();
    }
}
