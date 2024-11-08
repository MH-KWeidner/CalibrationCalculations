﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Helpers.Models;
using Helpers;
using Models;

namespace Interpolation
{
    public static class NISTInterpolatedZeroReduction
    {
        // TODO consider better naming for the class and method 

        public static void InterpolateSeriesList(double[] appliedForce, List<Series> seriesList)
        {
            const double DOUBLE_ZERO = 0.0;
            var zeroValuedElements = ArrayHelper.GetElementsByValue(DOUBLE_ZERO, appliedForce);

            DoubleValueArrayElement zeroStartElement;
            DoubleValueArrayElement zeroEndElement;

            for (int i = 0; i < zeroValuedElements.Length; i++)
            {
                if (i + 1 == zeroValuedElements.Length)
                    break;

                zeroStartElement = zeroValuedElements[i];
                zeroEndElement = zeroValuedElements[i + 1];

                // 2 consecutive zero values can not be used to calculate interpolation
                if (zeroEndElement.ArrayPosition - 1 == zeroStartElement.ArrayPosition)
                    continue;

                foreach (var series in seriesList)
                    InterpolateSeries(zeroStartElement, zeroEndElement, series);
            }
        }

        public static void InterpolateSeries(DoubleValueArrayElement zeroStartElement, DoubleValueArrayElement zeroEndElement, Series series)
        {
            int numberOfNonZeroForcePoints = zeroEndElement.ArrayPosition - zeroStartElement.ArrayPosition - 1;

            int seriesIndexingStart = zeroStartElement.ArrayPosition + 1;         
            int seriesIndexingEnd = zeroEndElement.ArrayPosition;

            if(numberOfNonZeroForcePoints == 1)
            {
                var interpolatedValue = InterpolateByZeroForceAverage(
                    startZeroValue: series.GetRawValue(zeroStartElement.ArrayPosition),
                    endZeroValue: series.GetRawValue(zeroEndElement.ArrayPosition),
                    forceReading: series.GetRawValue(seriesIndexingStart));
                    
                series.SetInterpolatedValue(seriesIndexingStart, interpolatedValue);
                
                return;
            }

            int OneBasedSeriesPositionForNonZeroForce = 1;

            for (int i = seriesIndexingStart; i < seriesIndexingEnd; i++)
            {
                var interpolatedValue = CalculateNISInterpolatedValue(
                     startZeroValue: series.GetRawValue(zeroStartElement.ArrayPosition),
                     endZeroValue: series.GetRawValue(zeroEndElement.ArrayPosition),
                     numberOfNonZeroForcePoints: numberOfNonZeroForcePoints,
                     forceReading: series.GetRawValue(i),
                     OneBasedSeriesPositionForNonZeroForce: OneBasedSeriesPositionForNonZeroForce);

                series.SetInterpolatedValue(i, interpolatedValue);

                OneBasedSeriesPositionForNonZeroForce++;
            }
        }

        public static double CalculateNISInterpolatedValue(double startZeroValue, double endZeroValue, int numberOfNonZeroForcePoints, double forceReading, int OneBasedSeriesPositionForNonZeroForce)
        {
            //TODO better naming
            
            try
            {
                return forceReading - (startZeroValue + ((endZeroValue - startZeroValue) * (OneBasedSeriesPositionForNonZeroForce - 1) / (numberOfNonZeroForcePoints - 1)));
            }
            catch
            {
                //TODO add specific error handling
                throw new Exception("Error in Statistics.CalculateNISInterpolatedValue");
            }
        }

        public static double InterpolateByZeroForceAverage(double startZeroValue, double endZeroValue, double forceReading)
        {
            //TODO better naming

            try
            {
                return forceReading - ((endZeroValue + startZeroValue)/2);
            }
            catch
            {
                //TODO add specific error handling
                throw new Exception("Error in Statistics.CalculateNISInterpolatedValue");
            }
        }
    }
}
