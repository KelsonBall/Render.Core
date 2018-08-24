using Kelson.Common.Transforms;
using Kelson.Common.Vectors;
using Render.Core.GraphicsInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Render.Core.GraphicsInterfaces
{
    public abstract class Quad
    {
        public Transform Transform { get; set; }

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
                Types = new HashSet<Type>{ typeof(float), typeof(double) },
                ShaderTypeName = "float",
                SetUniform = s => s.quad.graphics.gl.Uniform1(s.location.Value, (float)s.get.DynamicInvoke()),
            },
            new UniformTypeHandeler
            {
                Types = new HashSet<Type>{ typeof(int), typeof(short), typeof(sbyte) },
                ShaderTypeName = "int",
                SetUniform = s => s.quad.graphics.gl.Uniform1(s.location.Value, (int)s.get.DynamicInvoke()),
            },
            new UniformTypeHandeler
            {
                Types = new HashSet<Type>{ typeof(Vector2fd) },
                ShaderTypeName = "vec2",
                SetUniform = s =>
                {
                    var vec = (Vector2fd)s.get.DynamicInvoke();
                    s.quad.graphics.gl.Uniform2(s.location.Value, vec.X, vec.Y);
                },
            },
            new UniformTypeHandeler
            {
                Types = new HashSet<Type>{ typeof(Vector3fd) },
                ShaderTypeName = "vec3",
                SetUniform = s =>
                {
                    var vec = (Vector3fd)s.get.DynamicInvoke();
                    s.quad.graphics.gl.Uniform3(s.location.Value, vec.X, vec.Y, vec.Z);
                },
            },
            new UniformTypeHandeler
            {
                Types = new HashSet<Type>{ typeof(Vector4fd) },
                ShaderTypeName = "vec4",
                SetUniform = s =>
                {
                    var vec = (Vector4fd)s.get.DynamicInvoke();
                    s.quad.graphics.gl.Uniform4(s.location.Value, vec.X, vec.Y, vec.Z, vec.W);
                },
            },
            new UniformTypeHandeler
            {
                Types = new HashSet<Type>{ typeof(Transform) },
                ShaderTypeName = "vec4",
                SetUniform = s =>
                {
                    var mat = (Transform)s.get.DynamicInvoke();
                    s.quad.graphics.gl.UniformMatrix4(s.location.Value, 1, false, mat.AsSpan().ToArray());
                },
            },
        };

        internal class ShaderInput
        {
            private readonly PropertyInfo property;

            public Type ClrType => property.PropertyType;

            internal readonly Quad quad;

            internal readonly Delegate get;

            internal int? location;

            internal readonly UniformTypeHandeler handler;

            public ShaderInput(Quad quad, PropertyInfo property)
            {
                this.quad = quad;
                this.property = property;
                this.handler = TypeHandelers.First(t => t.Types.Contains(ClrType));
                this.get = property.GetGetMethod().CreateDelegate(typeof(Func<>).MakeGenericType(property.PropertyType), quad);
            }

            public void LoadUniformLocation() => quad.graphics.gl.GetUniformLocation(quad.program.Handle, property.Name);

            public void Set() => handler.SetUniform(this);


            public string ShaderSource() => $"uniform {handler.ShaderTypeName} {property.Name};";
        }

        const string VERTEX_SHADER_SOURCE = @"
in vec3 position;
int mat4 Transform;
void main()
{
    gl_Position = transform * vec4(position, 1.0);
}
";

        public abstract string Source { get; }

        internal readonly ManagedGraphicsService graphics;

        private readonly ShaderProgram program;

        private readonly List<ShaderInput> uniforms;

        internal Quad(ManagedGraphicsService graphics)
        {
            this.graphics = graphics;
            program = graphics.CreateProgram(VERTEX_SHADER_SOURCE, Source);
        }

        public void Draw()
        {

        }
    }
}
