using Kelson.Common.Transforms;
using OpenTK;
using OpenTK.Graphics;
using Render.Core.GraphicsInterface;
using System;

namespace ManagedRendering
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var graphics = new ManagedGraphicsService(new DebugGraphicsInterface()))
            using (var window = new Window(graphics))
                window.Run();
        }

        class Window : GameWindow
        {
            private readonly ManagedGraphicsService graphics;

            private readonly Grid grid2;
            private readonly Grid grid1;

            public Window(ManagedGraphicsService graphics) : base(300, 300, new GraphicsMode(), "Dotnet Core")
            {
                this.graphics = graphics;
                grid1 = new Grid(graphics);
                grid1.Origin = (10, 20);
                grid1.Size = (121, 121);
                grid2 = new Grid(graphics);
                
            }

            protected override void OnLoad(EventArgs e)
            {
                graphics.Load(this);
                base.OnLoad(e);
            }

            protected override void OnResize(EventArgs e)
            {
                graphics.SetViewport(ClientRectangle.Width, ClientRectangle.Height);
                base.OnResize(e);
            }

            protected override void OnUpdateFrame(FrameEventArgs e)
            {
                var time = TimeSpan.FromSeconds(e.Time);
                grid1.Update(time);
                //spinningGrid.Update(time);
                base.OnUpdateFrame(e);
            }

            protected override void OnRenderFrame(FrameEventArgs e)
            {
                graphics.Clear();

                grid1.Draw();
                //spinningGrid.Draw();

                SwapBuffers();
                base.OnRenderFrame(e);
            }
        }
    }
}
