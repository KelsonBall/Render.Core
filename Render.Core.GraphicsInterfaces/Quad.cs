using Kelson.Common.Transforms;
using Kelson.Common.Vectors;
using Render.Core.GraphicsInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Render.Core.GraphicsInterfaces
{
    public abstract class Quad
    {        
        [Uniform]
        public Vector2fd Origin { get; set; }
        [Uniform]
        public Vector2fd Size { get; set; }
        [Uniform]
        public Vector2fd Resolution { get; set; }

        internal class UniformTypeHandeler
        {
            public HashSet<Type> Types { get; set; }
            public string ShaderTypeName { get; set; }
            public Action<ShaderInput> SetUniform { get; set; }
        }

        internal static readonly UniformTypeHandeler[] TypeHandelers = new UniformTypeHandeler[]
        {
            new UniformTypeHandeler
            {
                Types = new HashSet<Type>{ typeof(float) },
                ShaderTypeName = "float",
                SetUniform = s =>
                {
                    var value = (float)s.get.DynamicInvoke();
                    s.quad.program.SetUniformFloat(s.property.Name, value);
                },
            },
            new UniformTypeHandeler
            {
                Types = new HashSet<Type>{ typeof(int), typeof(short), typeof(sbyte) },
                ShaderTypeName = "int",
                SetUniform = s =>
                {
                    var value = (int)s.get.DynamicInvoke();
                    s.quad.program.SetUniformInteger(s.property.Name, value);
                },
            },
            new UniformTypeHandeler
            {
                Types = new HashSet<Type>{ typeof(Vector2fd) },
                ShaderTypeName = "vec2",
                SetUniform = s =>
                {
                    var vec = (Vector2fd)s.get.DynamicInvoke();
                    s.quad.program.SetUniformVector(s.property.Name, vec);
                },
            },
            new UniformTypeHandeler
            {
                Types = new HashSet<Type>{ typeof(Vector3fd) },
                ShaderTypeName = "vec3",
                SetUniform = s =>
                {
                    var vec = (Vector3fd)s.get.DynamicInvoke();
                    s.quad.program.SetUniformVector(s.property.Name, vec);
                },
            },
            new UniformTypeHandeler
            {
                Types = new HashSet<Type>{ typeof(Vector4fd) },
                ShaderTypeName = "vec4",
                SetUniform = s =>
                {
                    var vec = (Vector4fd)s.get.DynamicInvoke();
                    s.quad.program.SetUniformVector(s.property.Name, vec);
                },
            },
            new UniformTypeHandeler
            {
                Types = new HashSet<Type>{ typeof(Transform) },
                ShaderTypeName = "mat4",
                SetUniform = s =>
                {
                    var mat = (Transform)s.get.DynamicInvoke();
                    s.quad.program.SetUniformTransform(s.property.Name, mat);
                },
            },
        };

        internal class ShaderInput
        {
            internal readonly PropertyInfo property;

            public Type ClrType => property.PropertyType;

            internal readonly Quad quad;

            internal readonly Delegate get;

            internal readonly UniformTypeHandeler handler;

            public ShaderInput(Quad quad, PropertyInfo property)
            {
                this.quad = quad;
                this.property = property;
                this.handler = TypeHandelers.First(t => t.Types.Contains(ClrType));
                this.get = property.GetGetMethod().CreateDelegate(typeof(Func<>).MakeGenericType(property.PropertyType), quad);
            }

            public void Set() => handler.SetUniform(this);

            public string ShaderSource() => $"uniform {handler.ShaderTypeName} {property.Name};";
        }

        const string VERTEX_SHADER_SOURCE = @"
in vec3 position;
uniform vec2 Origin;
uniform vec2 Size;
uniform vec2 Resolution;

void main()
{        
    float scale_x = Size.x / (Resolution.x / 2);
    float scale_y = Size.y / (Resolution.y / 2);
    float origin_x = (Origin.x / Resolution.x) * 2;
    float origin_y = (Origin.y / Resolution.y) * 2;
    gl_Position = vec4(vec2(position.x * scale_x + origin_x, position.y * scale_y + origin_y) - vec2(1, 1), 0, 1);
}
";

        public virtual async Task<string> Source()
        {
            using (var stream = GetType().Assembly.GetManifestResourceStream(GetType().FullName + ".glsl"))
            using (var reader = new StreamReader(stream))
                return await reader.ReadToEndAsync();
        }


        protected readonly ManagedGraphicsService graphics;

        private readonly ShaderProgram program;

        private readonly VertexArrayObject array;
        private readonly VertexBufferObject buffer;

        private readonly List<ShaderInput> uniforms = new List<ShaderInput>();

        public Quad(ManagedGraphicsService graphics)
        {                        
            var sourceTask = Source();
            uniforms.AddRange(GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => new ShaderInput(this, p)));
            this.graphics = graphics;
            array = graphics.CreateVertexArray();
            using (array.Binding())
            {
                buffer = graphics.CreateVertexBuffer(new Vector2fd[] { (0f, 0f), (0f, 1f), (1f, 1f), (1f, 0f) });
                string source = sourceTask.Result;
                foreach (var input in uniforms.Select(i => i.ShaderSource()))
                    Debug.Assert(source.Contains(input));                
                program = graphics.CreateProgram(VERTEX_SHADER_SOURCE, source);                
            }
        }

        public virtual void Update(TimeSpan time)
        {

        }

        public void Draw()
        {
            Resolution = (graphics.Width, graphics.Height);
            foreach (var uniform in uniforms)
                uniform.Set();

            using (array.Binding())
            using (buffer.Binding())
            using (program.Binding())
                graphics.gl.DrawArrays(OpenTK.Graphics.OpenGL.PrimitiveType.Quads, 0, 4);
        }
    }
}
