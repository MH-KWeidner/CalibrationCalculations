﻿namespace CertificateGeneration.Models.Modifiers
{
    /// <summary>
    /// Defines the <see cref="OrderByAppliedForceAscending" />
    /// </summary>
    public class OrderByAppliedForceAscending : IOrderSeries
    {
        /// <summary>
        /// The Order
        /// </summary>
        /// <param name="seriesValues">The seriesValues<see cref="List{SeriesValue}?"/></param>
        /// <returns>The <see cref="List{SeriesValue}?"/></returns>
        public List<SeriesValue>? Order(List<SeriesValue>? seriesValues)
        {
            // TODO add more exception handling

            // Check for null argument
            if (seriesValues == null)
            {
                throw new ArgumentNullException(nameof(seriesValues), "The seriesValues list cannot be null.");
            }

            // Ensure the list is not empty
            if (!seriesValues.Any())
            {
                throw new ArgumentException("The seriesValues list cannot be empty.", nameof(seriesValues));

                ArgumentNullException.ThrowIfNull(seriesValues);
            }

            return seriesValues?.OrderBy(sv => sv.AppliedForce).ToList();
        }
    }
}
