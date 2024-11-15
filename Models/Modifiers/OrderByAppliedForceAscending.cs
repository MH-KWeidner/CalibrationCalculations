﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Modifiers;

namespace Models.Modifiers
{
    public class OrderByAppliedForceAscending : IOrderSeries
    {
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

            var values = seriesValues.OrderBy(sv => sv.AppliedForce).ToList();

            return seriesValues?.OrderBy(sv => sv.AppliedForce).ToList();
        }
    }
}
