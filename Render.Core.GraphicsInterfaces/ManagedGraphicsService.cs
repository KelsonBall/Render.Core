using OpenTK.Graphics.OpenGL;
using Render.Core.Images;
using Render.Core.Images.PixelFormats;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Render.Core.GraphicsInterface
{
    public readonly struct AssetBinding : IDisposable
    {
        private readonly Action dispose;

        public AssetBinding(Action dispose)
            => this.dispose = dispose;

        public void Dispose() => dispose();
    }

    public interface IManagedAssetHandle : IDisposable
    {
        ManagedGraphicsService GraphicsService { get; }
        int Handle { get; }
        AssetBinding Binding();
    }

    public interface IShader : IManagedAssetHandle
    {
        void IncludeUniform<T>(IUniform<T> uniform) where T : struct;
        void IncludeBuffer<T>(IFrameBuffer<T> buffer) where T : struct;
        void IncludeTexture(ITexture texture);

    }

    public interface IProgram : IManagedAssetHandle
    {
        IShader FragmentShader { get; }
        IShader VertexShader { get; }
    }

    public interface IUniform<T> : IManagedAssetHandle where T : struct
    {
        T Value { get; set; }
        string Name { get; }
    }

    public interface IFrameBuffer<T> : IManagedAssetHandle where T : struct
    {

    }

    public interface ITexture : IManagedAssetHandle, IImage
    {                
        OpenTK.Graphics.OpenGL.TextureBufferTarget BufferTarget { get; }
        OpenTK.Graphics.OpenGL.TextureMagFilter MagFilter { get; }
        OpenTK.Graphics.OpenGL.TextureMinFilter MinFilter { get; }
        OpenTK.Graphics.OpenGL.TextureTarget Target { get; }
        OpenTK.Graphics.OpenGL.TextureUnit Unit { get; }
        OpenTK.Graphics.OpenGL.TextureWrapMode WrapMode { get; }
    }

    public class Texture : ITexture
    {
        internal Texture(ManagedGraphicsService graphics, IImage image)
        {

        }

        public IPixelFormatRgba<byte> this[int x, int y] => throw new NotImplementedException();

        public TextureBufferTarget BufferTarget => throw new NotImplementedException();

        public TextureMagFilter MagFilter => throw new NotImplementedException();

        public TextureMinFilter MinFilter => throw new NotImplementedException();

        public TextureTarget Target => throw new NotImplementedException();

        public TextureUnit Unit => throw new NotImplementedException();

        public TextureWrapMode WrapMode => throw new NotImplementedException();

        public ManagedGraphicsService GraphicsService => throw new NotImplementedException();

        public int Handle => throw new NotImplementedException();

        public int Width => throw new NotImplementedException();

        public int Height => throw new NotImplementedException();

        public AssetBinding Binding()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IPixelFormatRgba<byte>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void WithDataPtr(Action<IntPtr> action)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class ManagedGraphicsService : IDisposable
    {

        private readonly IGraphicsInterface gl;
        private readonly ConcurrentBag<IManagedAssetHandle> assets = new ConcurrentBag<IManagedAssetHandle>();

        public ManagedGraphicsService(IGraphicsInterface graphics)
        {
            gl = graphics;
        }

        public void Dispose()
        {
            while (assets.TryTake(out IManagedAssetHandle asset))            
                asset.Dispose();            
        }
    }
}
