using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Render.Core.Rendering;
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
using Render.Core.Vectors;
using MathNet.Numerics.LinearAlgebra;
using Render.Core.Transforms;

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
        private (Rektor offset, Rektor size) _baseViewport = (Rektor.O, Rektor.O);
        private Stack<(Rektor offset, Rektor size)> _boundryStack = new Stack<(Rektor, Rektor)>();

        public Font Font { get => _styleStack.Peek().Font; set => _styleStack.Peek().Font = value; }
        public Color Fill { get => _styleStack.Peek().Fill; set => _styleStack.Peek().Fill = value; }
        public Color Stroke { get => _styleStack.Peek().Stroke; set => _styleStack.Peek().Stroke = value; }
        public float StrokeWeight { get => _styleStack.Peek().StrokeWeight; set => _styleStack.Peek().StrokeWeight = value; }

        public ulong FrameCount { get; set; } = 0;

        public event Action<ICanvas> Setup;
        public event Action<double> Step;
        public event Action<ICanvas> Draw;

        public Rektor ScreenCenter { get => (Width / 2, Height / 2); }
        public Rektor MousePosition { get => (Mouse.X, Mouse.Y) - ScreenCenter; }

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

        public void PushBoundry(Rektor offset, Rektor size)
        {
            _boundryStack.Push((offset, size));
            ApplyBoundry();
        }

        public (Rektor offset, Rektor size) PopBoundry()
        {
            var result = _boundryStack.Pop();
            ApplyBoundry();
            return result;
        }

        public void WithBoundry(Rektor offset, Rektor size, Action action)
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
                var offset = _boundryStack.Select(b => b.offset).Aggregate(Rektor.O, (acc, off) => acc + off);
                var size = _boundryStack.Peek().size;
                foreach (var boundry in _boundryStack.Reverse())
                    _g.Scissor((int)offset.X, Height - (int)offset.Y - (int)size.Y, (int)size.X, (int)size.Y);
            }

        }

        #region InputEvents

        private MouseDevice _mouse;
        public IMouseDevice Mouse { get => _mouse; }

        private KeyboardDevice _keyboard;
        public IKeyboardDevice Keyboard { get => _keyboard; }

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
            _mouse = new MouseDevice();
            window.OnMouseDownEvent += e => _mouse.InvokeMouseButtonDown(toLocal(e.Button), window.Mouse.GetState());
            window.OnMouseUpEvent += e => _mouse.InvokeMouseButtonUp(toLocal(e.Button), window.Mouse.GetState());
            window.OnMouseMoveEvent += e => _mouse.InvokeMouseMoved(e.X, e.Y, e.XDelta, e.YDelta, window.Mouse.GetState());


            _keyboard = new KeyboardDevice();
            window.OnKeyDownEvent += e => _keyboard.InvokeKeyDown(new KeyDownArgs(toLocal(e.Key)));
            // TODO time key down durations
            window.OnKeyUpEvent += e => _keyboard.InvokeKeyReleased(new KeyReleaseArgs(toLocal(e.Key), 0, 0));
            _keyboard.SetKeyboardState(window.Keyboard.GetState());

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

            _g.ClearColor(Color.Constants.Black);
            _g.Enable(EnableCap.Blend);
            _g.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            _g.MatrixMode(MatrixMode.Modelview);

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
            Step?.Invoke(e.Time);
            //MousePosition = (Mouse.X, Mouse.Y);

            //if (Keyboard.KeyIsPressed(Key.Escape))
            //    _window.Exit();
        }

        private void OnRenderFrameAction(FrameEventArgs e)
        {
            FrameCount++;

            _g.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _g.MatrixMode(MatrixMode.Modelview);
            _g.LoadIdentity();
            _g.Ortho(-Width / 2, Width / 2, Height / 2, -Height / 2, -1, 1);
            Draw?.Invoke(this);

            _window.SwapBuffers();
        }

        public void Background(Color4 color)
        {
            _g.ClearColor(color);
        }

        public void Triangle(Rektor a, Rektor b, Rektor c)
        {
            Shape((0, 0), a, b, c);
        }

        public void Rectangle(Rektor position, Rektor size)
        {
            position -= size / 2;
            Quad(position,
                 position + (size.X, 0),
                 position + size,
                 position + (0, size.Y));
        }

        public void Quad(Rektor a, Rektor b, Rektor c, Rektor d)
        {
            Shape((0, 0), a, b, c, d);
        }

        public void Ellipse(Rektor position, Rektor size)
        {
            Shape(position, 0d.Lerp(Tau, count: size.Magnitude()).Select(t => new Rektor(size.X * Cos(t), size.Y * Sin(t))).ToArray());
        }

        public void Line(Rektor a, Rektor b)
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

        public void Image(Texture image, Rektor position)
        {
            WithOrtho(() =>
            {
                _g.Color4(Color4.White);
                _g.Translate((position - image.Size / 2).ToVector3());
                _g.Disable(EnableCap.Lighting);
                _g.Enable(EnableCap.Texture2D);
                _g.Enable(EnableCap.Blend);

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

        public TextRenderResult Text(string text, Rektor position) => new TextRenderResult(RenderText(text, position));

        private IEnumerable<CharacterRenderResult> RenderText(string text, Rektor position)
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

        public void Icon(FontAwesomeIcons icon, float size, Rektor position)
        {
            Image(icons[icon, size, Fill], position);
        }

        public void Shape(Rektor position, params Rektor[] points)
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
                if (_boundryStack.Count > 0)
                    _g.Translate(_boundryStack.Select(b => b.offset).Aggregate(Rektor.O, (acc, off) => acc + off).ToVector3());
                action();
            }
            _g.PopMatrix();
        }

        public void PushFrame(OrthoFrame frame)
        {
            _g.MultMatrix(frame.Matrix.AsColumnMajorArray());
        }

        public void PopFrame(OrthoFrame frame)
        {
            _g.MultMatrix(frame.Inverse.AsColumnMajorArray());
        }

        public void LoadMatrix(Matrix<double> load)
        {
            _g.LoadMatrix(load.AsColumnMajorArray());
        }

        public void MultMatrix(Matrix<double> matrix)
        {
            _g.MultMatrix(matrix.AsColumnMajorArray());
        }

        private void WithTexture(Texture image, Action action)
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

        public void PushMatrix(Matrix<double> matrix)
        {
            throw new NotImplementedException();
        }

        public void PopMatrix(Matrix<double> matrix)
        {
            throw new NotImplementedException();
        }
    }
}
