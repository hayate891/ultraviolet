﻿using System;
using Ultraviolet.Core;

namespace Ultraviolet.Graphics
{
    /// <summary>
    /// Represents a vertex containing 3D position, color, and texture coordinate data.
    /// </summary>
    [Preserve(AllMembers = true)]
    public struct VertexPositionColorTexture : IVertexType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VertexPositionColorTexture"/> structure.
        /// </summary>
        /// <param name="position">The vertex position.</param>
        /// <param name="color">The vertex color.</param>
        /// <param name="textureCoordinate">The texture coordinate.</param>
        public VertexPositionColorTexture(Vector3 position, Color color, Vector2 textureCoordinate)
        {
            this.Position = position;
            this.Color = color;
            this.TextureCoordinate = textureCoordinate;
        }

        /// <summary>
        /// Gets the vertex declaration.
        /// </summary>
        VertexDeclaration IVertexType.VertexDeclaration
        {
            get { return VertexDeclaration; }
        }

        /// <summary>
        /// The vertex declaration.
        /// </summary>
        public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new[] {
            new VertexElement(0, VertexFormat.Vector3, VertexUsage.Position, 0),
            new VertexElement(sizeof(Single) * 3, VertexFormat.Color, VertexUsage.Color, 0),
            new VertexElement(sizeof(Single) * 3 + sizeof(Byte) * 4, VertexFormat.Vector2, VertexUsage.TextureCoordinate, 0),
        });

        /// <summary>
        /// The vertex position.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The vertex color.
        /// </summary>
        public Color Color;

        /// <summary>
        /// The texture coordinate.
        /// </summary>
        public Vector2 TextureCoordinate;
    }
}
