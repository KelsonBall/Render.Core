using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Render.Core.Rendering;
using Render.Core.Math;
using Render.Core.Textures;
using System.Linq;
using Render.Core.Fonts;
using System.Collections.Generic;
using Render.Core.Extensions;
//using Render.Core.Transforms;
using Render.Core.Implementation;
using Render.Core.Input;
using static Render.Core.Math.Constants;
using static System.Math;
using MouseDevice = Render.Core.Input.Mouse.MouseDevice;
using MouseButton = Render.Core.Input.MouseButton;
using KeyboardDevice = Render.Core.Input.Keyboard.KeyboardDevice;
using Key = Render.Core.Input.Key;
using Render.Core.Input.Joystick;
using Render.Core.Input.Keyboard;
using Render.Core.Input.Mouse;
using Render.Core.Input.PointerDevice;
using Render.Core.GraphicsInterface;
using Render.Core.WindowInterface;

namespace Render.Core
{
    public static class Window
    {
        public static ICanvas New(int width, int height, string title = "Canvas") => new Canvas(width, height, title);

        public static ICanvas From(IWindow window) => throw new NotImplementedException();
    }
}

namespace Render.Core.Implementation
{
    internal class Canvas : ICanvas
    {
        private readonly IWindow _window;
        private readonly IGraphicsInterface _g;

        private Stack<Style> _styleStack = new Stack<Style>();
        //private Stack<Transform> _transformStack = new Stack<Transform>();
        private (PVector offset, PVector size) _baseViewport = (PVector.O, PVector.O);
        private Stack<(PVector offset, PVector size)> _boundryStack = new Stack<(PVector, PVector)>();

        public Font Font { get => _styleStack.Peek().Font; set => _styleStack.Peek().Font = value; }
        public Color Fill { get => _styleStack.Peek().Fill; set => _styleStack.Peek().Fill = value; }
        public Color Stroke { get => _styleStack.Peek().Stroke; set => _styleStack.Peek().Stroke = value; }
        public float StrokeWeight { get => _styleStack.Peek().StrokeWeight; set => _styleStack.Peek().StrokeWeight = value; }

        public ulong FrameCount { get; set; } = 0;

        public event Action<ICanvas> Setup;

        public event Action<ICanvas> Draw;

        public PVector MousePosition { get => (Mouse.X, Mouse.Y); }

        public void PushStyle()
        {
            _styleStack.Push(_styleStack.Peek().Copy());
        }

        public void PushStyle(Style style)
        {
            _styleStack.Push(style);
        }

        public Style PopStyle()
        {
            return _styleStack.Pop();
        }

        public void WithStyle(Style style, Action action)
        {
            PushStyle(style);
            action();
            PopStyle();
        }

        public void WithStyle(Action action)
        {
            PushStyle();
            action();
            PopStyle();
        }

        public void PushBoundry(PVector offset, PVector size)
        {
            _boundryStack.Push((offset, size));
            ApplyBoundry();
        }

        public (PVector offset, PVector size) PopBoundry()
        {
            var result = _boundryStack.Pop();
            ApplyBoundry();
            return result;
        }

        public void WithBoundry(PVector offset, PVector size, Action action)
        {
            PushBoundry(offset, size);
            action();
            PopBoundry();
        }

        private void ApplyBoundry()
        {
            if (_boundryStack.Count == 0)
            {
                _g.Disable(EnableCap.ScissorTest);
            }
            else
            {
                _g.Enable(EnableCap.ScissorTest);
                var offset = _boundryStack.Select(b => b.offset).Aggregate(PVector.O, (acc, off) => acc + off);
                var size = _boundryStack.Peek().size;
                foreach (var boundry in _boundryStack.Reverse())
                    _g.Scissor((int)offset.X, Height - (int)offset.Y - (int)size.Y, (int)size.X, (int)size.Y);
            }

        }

        #region InputEvents

        private MouseDevice _mouse;
        public IMouseDevice Mouse { get => _mouse; }

        public IKeyboardDevice Keyboard { get; private set; }

        public IPointerDevice Pointer { get; private set; }

        public IJoystickDevice[] Joysticks { get; private set; }

        public IGamePadDevice[] GamePads { get; private set; }

        public int Width => _window.Width;

        public int Height => _window.Height;

        #endregion

        public Canvas(int sizex, int sizey) : this(sizex, sizey, "Canvas")
        {
        }


        public Canvas(int sizex, int sizey, string title) : this(sizex, sizey, title, new LeanGraphicsInterface())
        {
        }

        public Canvas(int sizex, int sizey, string title, IGraphicsInterface glInterface)
        {
            _g = glInterface;
            _window = new ApiWindow(sizex, sizey, new GraphicsMode(32, 24, 0, 8), title);
            _mouse = new MouseDevice();
            _window.OnMouseMoveEvent += _window_OnMouseMoveEvent;
            BindWindow(_window);
        }

        private void _window_OnMouseMoveEvent(OpenTK.Input.MouseMoveEventArgs obj)
        {
            _mouse.InvokeMouseMoved(_window.Mouse.X, _window.Mouse.Y, _window.Mouse.XDelta, _window.Mouse.YDelta, _window.Mouse.GetState());
        }

        private void BindWindow(IWindow window)
        {
            var mouse = new MouseDevice();
            window.OnMouseDownEvent += e => mouse.InvokeMouseButtonDown(toLocal(e.Button), window.Mouse.GetState());
            window.OnMouseUpEvent += e => mouse.InvokeMouseButtonUp(toLocal(e.Button), window.Mouse.GetState());
            window.OnMouseMoveEvent += e => mouse.InvokeMouseMoved(e.X, e.Y, e.XDelta, e.YDelta, window.Mouse.GetState());

            var keyboard = new KeyboardDevice();

            window.VSync = VSyncMode.On;
            window.OnLoadEvent += OnLoadAction;
            window.OnResizeEvent += OnResizeAction;
            window.OnUpdateFrameEvent += OnUpdateFrameAction;
            window.OnRenderFrameEvent += OnRenderFrameAction;
        }

        private MouseButton toLocal(OpenTK.Input.MouseButton mb) => (MouseButton)(int)mb;

        private Key toLocal(OpenTK.Input.Key key) => (Key)(int)key;

        private void OnLoadAction(EventArgs e)
        {
            _g.Viewport(_window.ClientRectangle.X, _window.ClientRectangle.Y, _window.ClientRectangle.Width, _window.ClientRectangle.Height);

            _g.ClearColor(Color.Constants.CornflowerBlue);
            _g.Enable(EnableCap.Blend);
            _g.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            _g.MatrixMode(MatrixMode.Projection);

            _styleStack.Push(new Style());

            icons = new IconCache();

            Setup?.Invoke(this);
        }

        private void OnResizeAction(EventArgs e)
        {
            _g.Viewport(_window.ClientRectangle.X, _window.ClientRectangle.Y, _window.ClientRectangle.Width, _window.ClientRectangle.Height);
        }

        private void OnUpdateFrameAction(FrameEventArgs e)
        {

            //MousePosition = (Mouse.X, Mouse.Y);

            //if (Keyboard.KeyIsPressed(Key.Escape))
            //    _window.Exit();
        }

        private void OnRenderFrameAction(FrameEventArgs e)
        {
            FrameCount++;

            _g.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Draw?.Invoke(this);

            _window.SwapBuffers();
        }

        public void Background(Color4 color)
        {
            _g.ClearColor(color);
        }

        public void Triangle(PVector a, PVector b, PVector c)
        {
            Shape((0, 0), a, b, c);
        }

        public void Rectangle(PVector position, PVector size)
        {
            Quad(position,
                 position + (size.X, 0),
                 position + size,
                 position + (0, size.Y));
        }

        public void Quad(PVector a, PVector b, PVector c, PVector d)
        {
            Shape((0, 0), a, b, c, d);
        }

        public void Ellipse(PVector position, PVector size)
        {
            Shape(position, 0d.Lerp(Tau, count: size.Magnitude()).Select(t => new PVector(size.X * Cos(t), size.Y * Sin(t))).ToArray());
        }

        public void Line(PVector a, PVector b)
        {
            if (StrokeWeight <= 0)
                return;

            WithOrtho(() =>
            {
                _g.LineWidth(StrokeWeight);
                _g.Color4(Stroke);
                WithPrimitive(PrimitiveType.Lines, () =>
                {
                    _g.Vertex2(a);
                    _g.Vertex2(b);
                });
            });
        }

        public void Arc(PVector position, PVector size, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void Image(PImage image, PVector position)
        {
            WithOrtho(() =>
            {
                _g.Color4(Color4.White);
                _g.Translate(position.ToVector3());
                _g.Disable(EnableCap.Lighting);
                _g.Enable(EnableCap.Texture2D);

                WithTexture(image, () =>
                {
                    WithPrimitive(PrimitiveType.Quads, () =>
                    {
                        _g.TexCoord2(1f, 1f);
                        _g.Vertex2(image.Width, image.Height);
                        _g.TexCoord2(0f, 1f);
                        _g.Vertex2(0, image.Height);
                        _g.TexCoord2(0f, 0f);
                        _g.Vertex2(0, 0);
                        _g.TexCoord2(1.0f, 0.0f);
                        _g.Vertex2(image.Width, 0);
                    });
                });
            });
        }

        public TextRenderResult Text(string text, PVector position) => new TextRenderResult(RenderText(text, position));

        private IEnumerable<CharacterRenderResult> RenderText(string text, PVector position)
        {
            var characters = text.Select(c => (c, Font[c, Fill]))
                                 .Select(t => (letter: t.Item1, texture: t.Item2.texture, yshift: t.Item2.yshift));

            foreach (var character in characters)
            {
                Image(character.texture, position + (0, -character.texture.Height + Font.MaxHeight + character.yshift));
                position += (character.texture.Width, 0);
                yield return new CharacterRenderResult
                {
                    Height = character.texture.Height + character.yshift,
                    Width = character.texture.Width,
                    Text = character.letter.ToString()
                };
            }
        }

        private static IconCache icons;

        public void Icon(FontAwesomeIcons icon, float size, PVector position)
        {
            Image(icons[icon, size, Fill], position);
        }

        public void Shape(PVector position, params PVector[] points)
        {
            var scaled = points.Indecies(i =>
            {
                var vectors = points[i].GetOuterFaceVector(points.Previous(i), points.Next(i));
                return (points[i] + vectors.pv * StrokeWeight, points[i] + vectors.ov * StrokeWeight, points[i] + vectors.nv * StrokeWeight);
            });

            WithOrtho(() =>
            {
                _g.Translate(position.ToVector3());
                _g.Color4(Stroke);
                WithPrimitive(PrimitiveType.Polygon, () =>
                {
                    foreach (var vertexSet in scaled)
                    {
                        _g.Vertex2(vertexSet.Item3);
                        _g.Vertex2(vertexSet.Item2);
                        _g.Vertex2(vertexSet.Item1);
                    }
                });

                _g.Color4(Fill);
                WithPrimitive(PrimitiveType.Polygon, () =>
                {
                    foreach (var vertex in points)
                        _g.Vertex2(vertex);
                });
            });
        }

        private void WithPrimitive(PrimitiveType type, Action action)
        {
            _g.Begin(type);
            action();
            _g.End();
        }

        private void WithOrtho(Action action)
        {
            _g.PushMatrix();
            {
                _g.LoadIdentity();
                _g.Ortho(0, Width, Height, 0, -1, 1);

                if (_boundryStack.Count > 0)
                {
                    var offset = _boundryStack.Select(b => b.offset).Aggregate(PVector.O, (acc, off) => acc + off);
                    _g.Translate(offset.ToVector3());
                }

                action();
            }
            _g.PopMatrix();
        }

        private void WithTexture(PImage image, Action action)
        {
            _g.BindTexture(TextureTarget.Texture2D, image);
            {
                action();
            }
            _g.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            _window.Dispose();
        }

        public uint KeyPressDuration(Input.Key key)
        {
            throw new NotImplementedException();
        }

        public bool KeyIsPressed(Input.Key key)
        {
            throw new NotImplementedException();
        }

        public void AddKeyCombo(params Input.Key[] keys)
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            _window.Run(60);
        }
    }
}
