using Kelson.Common.Vectors;
using Render.Core.Input.Gamepad;

namespace Render.Core.Input
{
    public interface IKeyboardState
    {        
        uint KeyPressedTicks(Key key);
        double KeyPressedMilliseconds(Key key);
        bool KeyIsPressed(Key key);
    }

    public interface IMouseState
    {
        uint ButtonPressedTicks(MouseButton button);
        double ButtonPressedMilliseconds(MouseButton button);
        bool ButtonIsPressed(MouseButton button);

        bool IsDragging { get; }
        double Velocity { get; }
        double PointerX { get; }
        double PointerY { get; }
        double ScrollDelta { get; }
    }

    public interface IJoystickState
    {
        //OpenTK.Input.JoystickState
        bool IsConnected { get; }
        double AxisValue(int axis);
        double AxisDelta(int axis);

        uint ButtonPressedTicks(int button);
        double ButtonPressedMilliseconds(int button);
        bool ButtonIsPressed(int button);

        Vector2fd HatState(int hat);
    }

    public interface IGamepadState
    {
        //OpenTK.Input.GamePadState
        Vector2fd LeftAxis { get; }
        Vector2fd RightAxis { get; }
        double LeftTrigger { get; }
        double RightTrigger { get; }
        uint ButtonPressedTicks(GamepadButton button);
        double ButtonPressedMilliseconds(GamepadButton button);
        bool ButtonIsPressed(GamepadButton button);
    }    

    public interface IInputFrame
    {
        IKeyboardState PreviousKeyboard { get; }
        IKeyboardState CurrentKeyboard { get; }
        IMouseState PreviousMouse { get; }
        IMouseState CurrentMouse { get; }
        IJoystickState PreviousJoystick { get; }
        IJoystickState CurrentJoystick { get; }
        IGamepadState[] PreviousGamepads { get; }
        IGamepadState[] CurrentGamepads { get; }
    }
}
