using Kelson.Common.Vectors;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Render.Core.GraphicsInterface;
using System;

namespace ManagedRendering
{
    class Program
    {
        const string VERTEX_SHADER_SOURCE = @"
in vec2 position;

void main() {
    gl_Position = vec4(position, 0.0, 1.0);
}
";

        const string FRAG_SHADER_SOURCE = @"
uniform vec4 color;

void main()
{
    if (
}
";

        static void Main(string[] args)
        {
            using (var graphics = new ManagedGraphicsService(new LeanGraphicsInterface()))
            {



            }
        }

        class Window : GameWindow
        {
            private readonly ManagedGraphicsService graphics;

            private readonly VertexBufferObject vbo;
            private readonly ShaderProgram program;

            public Window(ManagedGraphicsService graphics) : base(256, 256)
            {
                this.graphics = graphics;
                vbo = graphics.CreateVertexBuffer(new Vector2fd[]
                {
                    (0f, 0f), (1f, 0f), (0f, 1f), (1f, 1f), (1f, 0f)
                });

                program = graphics.CreateProgram(VERTEX_SHADER_SOURCE, FRAG_SHADER_SOURCE);
            }

            protected override void OnLoad(EventArgs e)
            {
                graphics.gl.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
                graphics.gl.ClearColor(0f, 0f, 0f, 0f);

                base.OnLoad(e);
            }

            protected override void OnRenderFrame(FrameEventArgs e)
            {
                using (program.Binding())
                {
                    using (vbo.Binding())
                    {
                        graphics.gl.Draw8
                    }
                }
                base.OnRenderFrame(e);
            }
        }
    }
}
