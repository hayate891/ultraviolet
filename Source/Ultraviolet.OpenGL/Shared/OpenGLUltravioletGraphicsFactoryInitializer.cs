﻿using Ultraviolet.Core;
using Ultraviolet.Graphics;
using Ultraviolet.Graphics.Graphics2D;
using Ultraviolet.OpenGL.Bindings;
using Ultraviolet.OpenGL.Graphics;
using Ultraviolet.OpenGL.Graphics.Graphics2D;
using Ultraviolet.Platform;
using Ultraviolet.SDL2.Platform;

namespace Ultraviolet.OpenGL
{
    /// <summary>
    /// Initializes factory methods for the OpenGL/SDL2 implementation of the graphics subsystem.
    /// </summary>
    [Preserve(AllMembers = true)]
    public sealed class OpenGLUltravioletGraphicsFactoryInitializer : IUltravioletFactoryInitializer
    {
        /// <inheritdoc/>
        public void Initialize(UltravioletContext owner, UltravioletFactory factory)
        {
            // Core classes
            factory.SetFactoryMethod<CursorFactory>((uv, surface, hx, hv) => new OpenGLCursor(uv, surface, hx, hv));
            factory.SetFactoryMethod<GeometryStreamFactory>((uv) => new OpenGLGeometryStream(uv));
            factory.SetFactoryMethod<VertexBufferFactory>((uv, vdecl, vcount) => new OpenGLVertexBuffer(uv, vdecl, vcount, gl.GL_STATIC_DRAW));
            factory.SetFactoryMethod<IndexBufferFactory>((uv, itype, icount) => new OpenGLIndexBuffer(uv, itype, icount, gl.GL_STATIC_DRAW));
            factory.SetFactoryMethod<DynamicVertexBufferFactory>((uv, vdecl, vcount) => new OpenGLVertexBuffer(uv, vdecl, vcount, gl.GL_DYNAMIC_DRAW));
            factory.SetFactoryMethod<DynamicIndexBufferFactory>((uv, itype, icount) => new OpenGLIndexBuffer(uv, itype, icount, gl.GL_DYNAMIC_DRAW));
            factory.SetFactoryMethod<Surface2DFactory>((uv, width, height) => new OpenGLSurface2D(uv, width, height));
            factory.SetFactoryMethod<Surface2DFromSourceFactory>((uv, source) => new OpenGLSurface2D(uv, source));
            factory.SetFactoryMethod<Texture2DFactory>((uv, width, height, immutable) => new OpenGLTexture2D(uv, width, height, immutable));
            factory.SetFactoryMethod<RenderTarget2DFactory>((uv, width, height, usage) => new OpenGLRenderTarget2D(uv, width, height, usage));
            factory.SetFactoryMethod<RenderBuffer2DFactory>((uv, format, width, height, options) => new OpenGLRenderBuffer2D(uv, format, width, height, options));

            // Core effects
            factory.SetFactoryMethod<BasicEffectFactory>((uv) => new OpenGLBasicEffect(uv));
            factory.SetFactoryMethod<SpriteBatchEffectFactory>((uv) => new OpenGLSpriteBatchEffect(uv));
            factory.SetFactoryMethod<BlurEffectFactory>((uv) => new OpenGLBlurEffect(uv));

            // BlendState
            var blendStateOpaque = OpenGLBlendState.CreateOpaque(owner);
            var blendStateAlphaBlend = OpenGLBlendState.CreateAlphaBlend(owner);
            var blendStateAdditive = OpenGLBlendState.CreateAdditive(owner);
            var blendStateNonPremultiplied = OpenGLBlendState.CreateNonPremultiplied(owner);

            factory.SetFactoryMethod<BlendStateFactory>((uv) => new OpenGLBlendState(uv));
            factory.SetFactoryMethod<BlendStateFactory>("Opaque", (uv) => blendStateOpaque);
            factory.SetFactoryMethod<BlendStateFactory>("AlphaBlend", (uv) => blendStateAlphaBlend);
            factory.SetFactoryMethod<BlendStateFactory>("Additive", (uv) => blendStateAdditive);
            factory.SetFactoryMethod<BlendStateFactory>("NonPremultiplied", (uv) => blendStateNonPremultiplied);

            // DepthStencilState
            var depthStencilStateDefault = OpenGLDepthStencilState.CreateDefault(owner);
            var depthStencilStateDepthRead = OpenGLDepthStencilState.CreateDepthRead(owner);
            var depthStencilStateNone = OpenGLDepthStencilState.CreateNone(owner);

            factory.SetFactoryMethod<DepthStencilStateFactory>((uv) => new OpenGLDepthStencilState(uv));
            factory.SetFactoryMethod<DepthStencilStateFactory>("Default", (uv) => depthStencilStateDefault);
            factory.SetFactoryMethod<DepthStencilStateFactory>("DepthRead", (uv) => depthStencilStateDepthRead);
            factory.SetFactoryMethod<DepthStencilStateFactory>("None", (uv) => depthStencilStateNone);

            // RasterizerState
            var rasterizerStateCullClockwise = OpenGLRasterizerState.CreateCullClockwise(owner);
            var rasterizerStateCullCounterClockwise = OpenGLRasterizerState.CreateCullCounterClockwise(owner);
            var rasterizerStateCullNone = OpenGLRasterizerState.CreateCullNone(owner);

            factory.SetFactoryMethod<RasterizerStateFactory>((uv) => new OpenGLRasterizerState(uv));
            factory.SetFactoryMethod<RasterizerStateFactory>("CullClockwise", (uv) => rasterizerStateCullClockwise);
            factory.SetFactoryMethod<RasterizerStateFactory>("CullCounterClockwise", (uv) => rasterizerStateCullCounterClockwise);
            factory.SetFactoryMethod<RasterizerStateFactory>("CullNone", (uv) => rasterizerStateCullNone);

            // SamplerState
            var samplerStatePointClamp = OpenGLSamplerState.CreatePointClamp(owner);
            var samplerStatePointWrap = OpenGLSamplerState.CreatePointWrap(owner);
            var samplerStateLinearClamp = OpenGLSamplerState.CreateLinearClamp(owner);
            var samplerStateLinearWrap = OpenGLSamplerState.CreateLinearWrap(owner);
            var samplerStateAnisotropicClamp = OpenGLSamplerState.CreateAnisotropicClamp(owner);
            var samplerStateAnisotropicWrap = OpenGLSamplerState.CreateAnisotropicWrap(owner);

            factory.SetFactoryMethod<SamplerStateFactory>((uv) => new OpenGLSamplerState(uv));
            factory.SetFactoryMethod<SamplerStateFactory>("PointClamp", (uv) => samplerStatePointClamp);
            factory.SetFactoryMethod<SamplerStateFactory>("PointWrap", (uv) => samplerStatePointWrap);
            factory.SetFactoryMethod<SamplerStateFactory>("LinearClamp", (uv) => samplerStateLinearClamp);
            factory.SetFactoryMethod<SamplerStateFactory>("LinearWrap", (uv) => samplerStateLinearWrap);
            factory.SetFactoryMethod<SamplerStateFactory>("AnisotropicClamp", (uv) => samplerStateAnisotropicClamp);
            factory.SetFactoryMethod<SamplerStateFactory>("AnisotropicWrap", (uv) => samplerStateAnisotropicWrap);

            // Platform services
            var powerManagementService = new SDL2PowerManagementService();
            factory.SetFactoryMethod<PowerManagementServiceFactory>(() => powerManagementService);
        }
    }
}
