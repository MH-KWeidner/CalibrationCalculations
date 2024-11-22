﻿namespace CertificateGeneration.Helpers
{
    /// <summary>
    /// Defines the <see cref="ArrayHelper" />
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// The StackArray
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrays">The arrays<see cref="T[][]"/></param>
        /// <returns>The <see cref="T[]"/></returns>
        public static T[] StackArray<T>(params T[][] arrays)
        {
            // TODO need error handling

            return arrays.SelectMany(array => array).ToArray();
        }

        /// <summary>
        /// The StackArrayNTimes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array<see cref="T[]"/></param>
        /// <param name="n">The n<see cref="int"/></param>
        /// <returns>The <see cref="T[]"/></returns>
        public static T[] StackArrayNTimes<T>(T[] array, int n)
        {
            // TODO need error handling

            if (n < 1)
                throw new ArgumentException("The number of times to stack the array must be at least 1.", nameof(n));

            return Enumerable.Repeat(array, n).SelectMany(a => a).ToArray();
        }
    }
}
