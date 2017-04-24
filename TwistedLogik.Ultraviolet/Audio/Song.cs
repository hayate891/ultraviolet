﻿using System;

namespace Ultraviolet.Audio
{
    /// <summary>
    /// <para>Represents a song.</para>
    /// <para>Songs are audio resources, usually music, which are streamed from disk during playback.</para>
    /// </summary>
    public abstract class Song : UltravioletResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Song"/> class.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        protected Song(UltravioletContext uv)
            : base(uv)
        {

        }

        /// <summary>
        /// Gets the song's collection of tags.
        /// </summary>
        public abstract SongTagCollection Tags { get; }

        /// <summary>
        /// Gets the song's duration.
        /// </summary>
        public abstract TimeSpan Duration { get; }
    }
}
