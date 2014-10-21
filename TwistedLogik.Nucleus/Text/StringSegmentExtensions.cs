﻿using System;

namespace TwistedLogik.Nucleus.Text
{
    /// <summary>
    /// Contains extension methods relating to the StringSegment structure.
    /// </summary>
    public static class StringSegmentExtensions
    {
        /// <summary>
        /// Creates a string segment starting from the specified character and continuing to the end of the string.
        /// </summary>
        /// <param name="str">The string from which to create a string segment.</param>
        /// <param name="start">The index of the first character in the string segment.</param>
        /// <returns>A string segment that was created from the specified substring.</returns>
        public static StringSegment Segment(this String str, Int32 start)
        {
            Contract.Require(str, "str");

            return new StringSegment(str, start, str.Length - start);
        }

        /// <summary>
        /// Creates a string segment starting from the specified character and continuing for the specified number of characters.
        /// </summary>
        /// <param name="str">The string from which to create a string segment.</param>
        /// <param name="start">The index of the first character in the string segment.</param>
        /// <param name="length">The number of characters in the string segment.</param>
        /// <returns>A string segment that was created from the specified substring.</returns>
        public static StringSegment Segment(this String str, Int32 start, Int32 length)
        {
            Contract.Require(str, "str");

            return new StringSegment(str, start, length);
        }
    }
}