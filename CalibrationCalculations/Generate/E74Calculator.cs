﻿using CalibrationCalculations.IoC.DataTransforms;
using CalibrationCalculations.IoC.Modifiers;
using CalibrationCalculations.Models;
using CalibrationCalculations.StandardCalculations.DegreeOfBestFit;
using CalibrationCalculations.StandardCalculations.Interpolation;

namespace CalibrationCalculations.Generate
{
    /// <summary>
    /// Defines the <see cref="E74Calculator" />
    /// </summary>
    static public class E74Calculator
    {
        /// <summary>
        /// Defines the REFERENCE_SERIES_FOR_FORCE
        /// </summary>
        internal const int REFERENCE_SERIES_FOR_FORCE = 0;

        /// <summary>
        /// The Calculate
        /// </summary>
        /// <param name="configuration">The configuration<see cref="E74Configuration"/></param>
        /// <param name="appliedForces">The appliedForces<see cref="double[]"/></param>
        /// <param name="measurementData">The measurementData<see cref="double[][]"/></param>
        /// <returns>The <see cref="E74Result"/></returns>
        static public E74Result Calculate(E74Configuration configuration, double[] appliedForces, params double[][] measurementData)
        {
            E74Result result = new();

            MeasurementApplication application = new(appliedForces, measurementData);

            application.InterpolateSeriesData(InterpolatorFactory.CreateInterpolator(configuration.InterpolationType));
            application.RemoveValuesByIndex(configuration.TransientForceMeasurementsByIndex);
            application.ModifySeriesSize(new RemoveZeroValueForceItems());
            application.ReorderSeriesData(new RereorderByAppliedForceAscending());

            double[] forces = application.Transform(new AppliedForceToArray(), REFERENCE_SERIES_FOR_FORCE);
            double[][] valuesForAllSeries = application.Transform(new SeriesValueToArray());

            result.DegreeOfBestFit = SelectBestDegreeOfFit.Select(configuration.SelectedDegreeOfFit, forces, valuesForAllSeries);

            if (configuration.ApplyTemperatureCorrection)
                application.ApplyTemperatureCorrection(
                    ambientTemperature: configuration.AmbientTemperature,
                    standardCalibrationTemperature: configuration.StandardTemperatureOfCalibration,
                    temperatureCorrectionValuePer1Degree: configuration.TemperatureCorrectionValuePer1Degree);

            if (configuration.ApplyNominalForceCorrection)
            {

            }

            return new E74Result();
        }
    }
}
