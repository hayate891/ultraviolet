﻿using System;
using System.IO;
using Ultraviolet.Content;
using Ultraviolet.Core;

namespace Ultraviolet.BASS.Audio
{
    /// <summary>
    /// Imports .mp3, .ogg, and .wav files.
    /// </summary>
    [Preserve(AllMembers = true)]
    [ContentImporter(".mp3")]
    [ContentImporter(".ogg")]
    [ContentImporter(".wav")]
    public sealed class BASSMediaImporter : ContentImporter<String>
    {
        /// <summary>
        /// Imports the data from the specified file.
        /// </summary>
        /// <param name="metadata">The asset metadata for the asset to import.</param>
        /// <param name="stream">The stream that contains the data to import.</param>
        /// <returns>The data structure that was imported from the file.</returns>
        public override String Import(IContentImporterMetadata metadata, Stream stream)
        {
            return metadata.AssetFilePath;
        }
    }
}
