using Render.Core.GraphicsInterface;
using System;

namespace ManagedRendering
{
    class Program
    {
        const string VERTEX_SHADER_SOURCE = @"
#version 140

in vec2 position;

void main() {
    gl_Position = vec4(position, 0.0, 1.0);
}
";

        const string FRAG_SHADER_SOURCE = @"
#version 140
precision mediump float;

uniform float u_time;
uniform vec2 u_resolution;

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
    }
}
