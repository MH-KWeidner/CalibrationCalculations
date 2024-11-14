﻿using Models.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataTransform
{
    public class SeriesValueToArray : ITransformSeriesArray
    {
        public double[] ToArray(List<SeriesValue>? seriesValues)
        {
            ArgumentNullException.ThrowIfNull(seriesValues);

            return seriesValues.Select(sv => sv.Value).ToArray();
        }
    }
}
