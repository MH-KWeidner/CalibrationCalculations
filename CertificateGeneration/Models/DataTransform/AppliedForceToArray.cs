﻿namespace CertificateGeneration.Models.DataTransform
{
    /// <summary>
    /// Defines the <see cref="AppliedForceToArray" />
    /// </summary>
    public class AppliedForceToArray : ITransformToDoubleArray
    {
        /// <summary>
        /// The ToArray
        /// </summary>
        /// <param name="seriesValues">The seriesValues<see cref="List{SeriesValue}?"/></param>
        /// <returns>The <see cref="double[]"/></returns>
        public double[] ToArray(List<SeriesValue>? seriesValues)
        {
            ArgumentNullException.ThrowIfNull(seriesValues);

            return seriesValues.Select(sv => sv.AppliedForce).ToArray();
        }
    }
}
