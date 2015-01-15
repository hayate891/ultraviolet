﻿using System;

namespace TwistedLogik.Ultraviolet.UI.Presentation
{
    /// <summary>
    /// Represents the options that can be applied to a dependency property.
    /// </summary>
    [Flags]
    public enum DependencyPropertyOptions
    {
        /// <summary>
        /// No special options.
        /// </summary>
        None = 0,

        /// <summary>
        /// The dependency property's value is inherited from its container.
        /// </summary>
        Inherited = 1,

        /// <summary>
        /// The dependency property's value influences the arrangement of its object.
        /// </summary>
        AffectsArrange = 2,

        /// <summary>
        /// The dependency property's value influences the measurement of its object.
        /// </summary>
        AffectsMeasure = 4,
    }
}
