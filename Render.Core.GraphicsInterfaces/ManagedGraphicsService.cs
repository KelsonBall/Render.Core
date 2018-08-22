using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

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

    }

    public interface IProgram : IManagedAssetHandle
    {
        IShader FragmentShader { get; }
        IShader VertexShader { get; }
    }

    public interface IUniform<T> : IManagedAssetHandle
    {
        T Value { get; set; }
    }

    public interface IFrameBuffer : IManagedAssetHandle
    {

    }

    public interface ITexture : IManagedAssetHandle
    {
        this ARGB
        int Width { get; }
        int Height { get; }
        OpenTK.Graphics.OpenGL.TextureBufferTarget BufferTarget { get; }
        OpenTK.Graphics.OpenGL.TextureMagFilter MagFilter { get; }
        OpenTK.Graphics.OpenGL.TextureMinFilter MinFilter { get; }
        OpenTK.Graphics.OpenGL.TextureTarget Target { get; }
        OpenTK.Graphics.OpenGL.TextureUnit Unit { get; }
        OpenTK.Graphics.OpenGL.TextureWrapMode WrapMode { get; }
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
