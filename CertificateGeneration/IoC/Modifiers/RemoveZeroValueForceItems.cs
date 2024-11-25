﻿using CertificateGeneration.Models;

namespace CertificateGeneration.IoC.Modifiers
{
    /// <summary>
    /// Removes all SeriesValue items having zero force applied
    /// </summary>
    public class RemoveZeroValueForceItems : IModifySeriesSize
    {
        /// <summary>
        /// Removes all SeriesValue items having zero force applied
        /// </summary>
        /// <param name="seriesValues">The local list of SeriesValues to be modified</param>
        /// <returns>A new list of SeriesValues</returns>
        public List<DataPoint>? Modify(List<DataPoint>? seriesValues)
        {
            // TODO add more exception handling

            ArgumentNullException.ThrowIfNull(seriesValues);

            return seriesValues?.Where(sv => sv.AppliedForce > 0).ToList();
        }
    }
}
