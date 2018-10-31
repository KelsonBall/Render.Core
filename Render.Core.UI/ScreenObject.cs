using Render.Core.GraphicsInterfaces;
using Render.Core.Input;
using Render.Core.UI.Render;
using System;
using System.Collections.Generic;
using System.Text;

namespace Render.Core.UI
{
    public readonly struct Dimension
    {
        public readonly double Scale;
        public readonly double Offset;

        public Dimension(double scale, double offset)
            => (Scale, Offset) = (scale, offset);

        public double Value(double parentLength) => parentLength * Scale + Offset;

        public static implicit operator Dimension ((double s, double o) so) => new Dimension(so.s, so.o);
    }

    public struct Box
    {
        public Dimension PositionX;
        public Dimension PositionY;
        public Dimension SizeX;
        public Dimension SizeY;

        public Box((double s, double o) px, (double s, double o) py, (double s, double o) sx, (double s, double o) sy)
        {
            PositionX = px;
            PositionY = py;
            SizeX = sx;
            SizeY = sy;
        }
    }    

    public interface IUiLayout : IUiComponent
    {
        IEnumerable<IUiComponent> Components { get; }
        void Add(IUiComponent component);
        void Remove(IUiComponent component);
        void LayOutChildren();
    }

    public interface IUiContainer : IUiComponent
    {
        IUiComponent Content { get; }
        void SetContent(IUiComponent content);
        IUiComponent RemoveContent();
        void LayOutChildren();
    }

    public class Rectangle : IUiComponent
    {
        public Box RelativeLayout { get; set; }
        public Box AbsoluteLayout { get; private set; }
        public Quad RenderQuad { get; private set; }

        public void ApplyLayout(Box window, Box parent)
        {
            
        }

        public void CreateQuad(ManagedGraphicsService graphics)
        {
            RenderQuad = new Border(graphics) { BorderThickness = (0, 0, 0, 0) };
        }

        public void HandleKeyboardState(IMouseState state)
        {
            
        }

        public void HandleMouseState(IMouseState state)
        {
            
        }
    }

    public class Button : IUiComponent
    {

    }

    public class Margin : IUiLayout
    {

    }

    public class Stack : IUiLayout
    {
        public Box RelativeLayout { get; set; }
        public Quad RenderQuad { get; set; }

        private readonly List<IUiComponent> _components = new List<IUiComponent>();

        public IEnumerable<IUiComponent> Components => _components;

        public void LayOutChildren()
        {
            throw new NotImplementedException();
        }

        public void Add(IUiComponent component)
        {
            throw new NotImplementedException();
        }

        public void Remove(IUiComponent component)
        {
            throw new NotImplementedException();
        }

        public void HandleMouseState(IMouseState state)
        {
            throw new NotImplementedException();
        }

        public void HandleKeyboardState(IMouseState state)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUiComponent
    {
        Box RelativeLayout { get; set; }
        Box AbsoluteLayout { get; }
        Quad RenderQuad { get; }
        void CreateQuad(ManagedGraphicsService graphics);
        void ApplyLayout(Box window, Box parent);
        void HandleMouseState(IMouseState state);
        void HandleKeyboardState(IMouseState state);
    }
}
