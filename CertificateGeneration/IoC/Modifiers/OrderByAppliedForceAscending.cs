﻿using CertificateGeneration.Models;

namespace CertificateGeneration.IoC.Modifiers
{
    /// <summary>
    /// Defines the <see cref="OrderByAppliedForceAscending" />
    /// </summary>
    public class OrderByAppliedForceAscending : IOrderSeries
    {
        /// <summary>
        /// The Order
        /// </summary>
        /// <param name="seriesValues">The dataPoints<see cref="List{SeriesValue}?"/></param>
        /// <returns>The <see cref="List{SeriesValue}?"/></returns>
        public List<DataPoint>? Order(List<DataPoint>? seriesValues)
        {
            // TODO add more exception handling

            // Check for null argument
            if (seriesValues == null)
            {
                // TODO: fix this

                // throw new ArgumentNullException(nameof(dataPoints), "The dataPoints list cannot be null.");
            }

            // Ensure the list is not empty
            if (!seriesValues.Any())
            {
                throw new ArgumentException("The seriesValues list cannot be empty.", nameof(seriesValues));

                // TODO: fix this
                // ArgumentNullException.ThrowIfNull(dataPoints);
            }

            return seriesValues?.OrderBy(sv => sv.AppliedForce).ToList();
        }
    }
}
