namespace Render.Core.Input.Gamepad
{
    public interface IGamepadDevice
    {
        OpenTK.Input.GamePad[] GamePads { get; }
    }
}
