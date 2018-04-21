using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Render.Core.Input
{
    public abstract class KeyboardEventArgs
    {
        public KeyboardState Keyboard { get; internal set; }
    }
}
