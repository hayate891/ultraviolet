﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Android.App;
using Org.Libsdl.App;
using TwistedLogik.Nucleus;
using TwistedLogik.Nucleus.Messages;
using TwistedLogik.Ultraviolet.Platform;
using System.Xml.Linq;

namespace TwistedLogik.Ultraviolet.Android
{
    /// <summary>
    /// Represents an Ultraviolet activity's internal settings.
    /// </summary>
    internal class UltravioletActivitySettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UltravioletActivitySettings"/> class.
        /// </summary>
        private UltravioletActivitySettings()
        {

        }

        /// <summary>
        /// Saves the specified application settings to the specified file.
        /// </summary>
        /// <param name="path">The path to the file in which to save the application settings.</param>
        /// <param name="settings">The <see cref="UltravioletApplicationSettings"/> to serialize to the specified file.</param>
        public static void Save(String path, UltravioletActivitySettings settings)
        {
            var xml = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Settings",
                    null
                ));
            xml.Save(path);
        }

        /// <summary>
        /// Loads a set of application settings from the specified file.
        /// </summary>
        /// <param name="path">The path to the file from which to load the application settings.</param>
        /// <returns>The <see cref="UltravioletApplicationSettings"/> which were deserialized from the specified file.</returns>
        public static UltravioletActivitySettings Load(String path)
        {
            var xml = XDocument.Load(path);

            var settings = new UltravioletActivitySettings();

            return settings;
        }

        /// <summary>
        /// Creates a set of application settings from the current application state.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <returns>The <see cref="UltravioletApplicationSettings"/> which was retrieved.</returns>
        public static UltravioletActivitySettings FromCurrentSettings(UltravioletContext uv)
        {
            Contract.Require(uv, "uv");

            var settings = new UltravioletActivitySettings();

            return settings;
        }

        /// <summary>
        /// Applies the specified settings.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        public void Apply(UltravioletContext uv)
        {

        }
    }
}
