using System;
using RenderCore.Game;

namespace Render.Core.UI
{
    public class Button : ScreenObject
    {


        public Button(Action<Button> configure) : base(o => configure((Button)o))
        {
        }
    }
}
