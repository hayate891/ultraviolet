﻿using Ultraviolet.Content;
using Ultraviolet.Core;
using Ultraviolet.Graphics;
using Ultraviolet.SDL2.Native;

namespace Ultraviolet.OpenGL.Graphics
{
    /// <summary>
    /// Loads 2D surface assets.
    /// </summary>
    [Preserve(AllMembers = true)]
    [ContentProcessor]
    public sealed class OpenGLSurface2DProcessor : ContentProcessor<SDL_Surface, Surface2D>
    {
        /// <summary>
        /// Processes the specified data structure into a game asset.
        /// </summary>
        /// <param name="manager">The content manager with which the asset is being processed.</param>
        /// <param name="metadata">The asset's metadata.</param>
        /// <param name="input">The input data structure to process.</param>
        /// <returns>The game asset that was created.</returns>
        public override Surface2D Process(ContentManager manager, IContentProcessorMetadata metadata, SDL_Surface input)
        {
            var copy = input.CreateCopy();
            return new OpenGLSurface2D(manager.Ultraviolet, copy);
        }
    }
}
