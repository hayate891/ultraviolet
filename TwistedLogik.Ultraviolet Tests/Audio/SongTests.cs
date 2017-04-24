﻿using System;
using NUnit.Framework;
using Ultraviolet.Audio;
using Ultraviolet.Testing;

namespace Ultraviolet.Tests.Audio
{
    [TestFixture]
    public class SongTests : UltravioletApplicationTestFramework
    {
        [Test]
        [Category("Audio")]
        public void Song_LoadsTags_FromOggVorbisFile()
        {
            var song = default(Song);

            GivenAnUltravioletApplicationWithNoWindow()
                .WithInitialization(uv => uv.GetAudio().AudioMuted = true)
                .WithContent(content =>
                {
                    song = content.Load<Song>("Songs/Tone");

                    TheResultingString(song.Tags["CUSTOM_TAG1"].Value)
                        .ShouldBe("Hello, world!");

                    TheResultingString(song.Tags["CUSTOM_TAG2"].Value)
                        .ShouldBe("1234");

                    TheResultingValue(song.Tags["CUSTOM_TAG2"].As<Int32>())
                        .ShouldBe(1234);
                })
                .RunForOneFrame();
        }
    }
}
