using Kelson.Common.Vectors;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Render.Core.GraphicsInterface;
using System;

namespace ManagedRendering
{
    class Program
    {
        const string VERTEX_SHADER_SOURCE = @"
in vec3 position;

void main() 
{    
    gl_Position = vec4(position, 1.0);
}
";

        const string FRAG_SHADER_SOURCE = @"
uniform vec4 color;
uniform float width;
uniform float thickness;

void main()
{
    float mod_x = mod(gl_FragCoord.x, width);
    float mod_y = mod(gl_FragCoord.y, width);
    if (mod_x <= thickness || mod_y <= thickness)
    {
        gl_FragColor = vec4(0.0, 0.0, 0.0, 1.0);
    }
    else
    {
        gl_FragColor = color;
    }
}
";

        static void Main(string[] args)
        {
            using (var graphics = new ManagedGraphicsService(new DebugGraphicsInterface()))
            using (var window = new Window(graphics))
                window.Run();
        }

        class Window : GameWindow
        {
            private readonly ManagedGraphicsService graphics;

            private readonly VertexBufferObject vbo;
            private readonly VertexArrayObject vao;
            private readonly ShaderProgram program;

            public Window(ManagedGraphicsService graphics) : base(256, 256, new GraphicsMode(), "Dotnet Core")
            {
                this.graphics = graphics;
                vao = graphics.CreateVertexArray();
                using (vao.Binding())
                {
                    vbo = graphics.CreateVertexBuffer(new Vector2fd[] { (-1f, -1f), (-1f, 1f), (1f, 1f), (-1f, -1f), (1f, -1f) });
                    program = graphics.CreateProgram(VERTEX_SHADER_SOURCE, FRAG_SHADER_SOURCE);                    
                }
            }

            protected override void OnLoad(EventArgs e)
            {
                graphics.gl.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
                graphics.gl.ClearColor(0f, 0f, 0f, 0f);
                graphics.gl.Enable(EnableCap.Blend);                
                base.OnLoad(e);
            }

            protected override void OnResize(EventArgs e)
            {
                graphics.gl.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
                base.OnResize(e);
            }

            private double counter = 0;
            private float width = 10f;
            private float thickness = 2f;

            protected override void OnRenderFrame(FrameEventArgs e)
            {
                counter += 0.01;
                graphics.gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                graphics.gl.ClearColor(0.2f, 0.3f, 0.4f, 1f);
                graphics.gl.MatrixMode(MatrixMode.Projection);
                graphics.gl.LoadIdentity();                
                program.SetUniformVector("color", ((float)(Math.Sin(counter) / 4) + 0.5f, (float)(Math.Cos(counter) / 4) + 0.5f, 1f, 1f));
                program.SetUniformFloat(nameof(width), width);
                program.SetUniformFloat(nameof(thickness), thickness);
                using (program.Binding())
                {
                    graphics.gl.DrawArrays(PrimitiveType.TriangleStrip, 0, 5);
                }                

                SwapBuffers();
                base.OnRenderFrame(e);
            }
        }
    }
}
