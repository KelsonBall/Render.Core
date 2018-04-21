using Render.Core.Input.Joystick;
using Render.Core.Input.Keyboard;
using Render.Core.Input.Mouse;
using Render.Core.Input.PointerDevice;

namespace Render.Core.Input
{
    public interface IInputManager
    {
        IMouseDevice Mouse { get; }
        IKeyboardDevice Keyboard { get; }
        IPointerDevice Pointer { get; }
        IJoystickDevice[] Joysticks { get; }
        IGamePadDevice[] GamePads { get; }
    }
}
