using System;

namespace Render.Core.Input.PointerDevice
{
    public interface IPointerDevice
    {
        double X { get; }
        double Y { get; }

        uint PointerButtonPressedTicks(MouseButton button);
        double PointerButtonPressedMilliseconds(MouseButton button);
        bool PointerButtonIsPressed(MouseButton button);

        event Action<MouseMoveArgs> PointerMoved;
        event Action<MouseDownArgs> PointerButtonDown;
        event Action<MouseUpArgs> PointerButtonUp;
        event Action<MouseDragStartArgs> PointerDragStarted;
        event Action<MouseDragArgs> PointerDraging;
        event Action<MouseDragEndArgs> PointerDragEnding;
        event Action<MouseScrollArgs> PointerScrollEvent;
    }
}
