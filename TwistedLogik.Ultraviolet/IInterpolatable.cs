﻿using System;

namespace TwistedLogik.Ultraviolet
{
    /// <summary>
    /// Represents an object which can be interpolated.
    /// </summary>
    public interface IInterpolatable<T>
    {
        /// <summary>
        /// Interpolates between this value and the specified value.
        /// </summary>
        /// <param name="target">The target value.</param>
        /// <param name="t">A value between 0.0 and 1.0 representing the interpolation factor.</param>
        /// <returns>The interpolated value.</returns>
        T Interpolate(T target, Single t);
    }
}