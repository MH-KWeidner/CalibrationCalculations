﻿using CertificateGeneration.Interpolation;
using CertificateGeneration.Models.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CertificateGeneration.MathLib;
using CertificateGeneration.Models.DataTransform;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace CertificateGeneration.Models
{
    public class Application
    {
        private List<Series> seriesList;

        public Application(double[] appliedForce, params double[][] rawData)
        {
            seriesList = [];

            foreach (var data in rawData)
                seriesList.Add(Series.CreateSeries(seriesList.Count + 1, appliedForce, data));
        }

        public void InterpolateSeriesData(IInterpolate interpolator)
        {
            foreach (var series in seriesList)
                series.Interpolate(interpolator);
        }

        public void ModifySeriesData(IModifySeriesSize modifier)
        {
            foreach (var series in seriesList)
                series.Modify(modifier);
        }

        public void OrderSeriesData(IOrderSeries modifier)
        {
            foreach (var series in seriesList)
                series.Order(modifier);
        }

        public IList<double> GetSeriesRow(int row)
        {
            // TODO better naming

            // TODO: error handling

            // TODO: tansform with IoC?

            List<double> seriesRow = [];

            foreach (var series in seriesList)
                seriesRow.Add(series.GetValue(row));

            return seriesRow;
        }

        public int DetermineDegreeOfBestFittingPolynomial()
        {
            // how to test this??

            const int MIN_DEGREE_OF_FIT = 1;

            const int MAX_DEGREE_OF_FIT = 4;

            double[] stackedAppliedForces = StackData(new AppliedForceToArray());

            double[] stackedSeriesData = StackData(new SeriesValueToArray());

            double[] observedMean = CalculateSeriesMean();

            double[] appliedForces = GetAppliedForces();

            var rangeOfDegrees = Enumerable.Range(MIN_DEGREE_OF_FIT, MAX_DEGREE_OF_FIT).Reverse();

            int returnBestFitDegree = 0;

            // TODO: consider double?
            double currentResidualDeviation = 0;
            double previousResidualDeviation = 0;

            foreach (int degree in rangeOfDegrees)
            {
                returnBestFitDegree = degree;

                double[] polynomials = MathLib.Statistics.FitPolynomialToLeastSquares(stackedAppliedForces, stackedSeriesData, degree);

                double[] predictedFit = new double[appliedForces.Length];

                for (int i = 0; i < predictedFit.Length; i++)
                    predictedFit[i] = MathLib.Statistics.CalculatePolynomial(polynomials, appliedForces[i]);

                previousResidualDeviation = currentResidualDeviation;

                currentResidualDeviation = MathLib.Statistics.CalculateResidualStandardDeviation(observedMean, predictedFit, degree);

                if (degree == MAX_DEGREE_OF_FIT)
                    continue;

                if (IsBestFitPolynomialFit(currentResidualDeviation, previousResidualDeviation, observedMean.Length, degree))
                    break;
            }

            return returnBestFitDegree;
        }

        public int DetermineDegreeOfBestFittingPolynomialz()
        {
            // TODO: consider best way to get series size

            // TODO: test for symmetry of series data and applied forces

            //TODO; what are the boundry conditions for the degree of the polynomial?

            double[] appliedForces = GetAppliedForces();

            double[] seriesMean = CalculateSeriesMean();

            const int MAX_DEGREE_OF_FIT = 5;

            int degreeOfFit = MAX_DEGREE_OF_FIT;

            double[] residualStandardDeviations = new double[MAX_DEGREE_OF_FIT];

            for (int i = MAX_DEGREE_OF_FIT; i >= 1; i--)
            {
                double[] bestFit = Statistics.FitPolynomialToLeastSquares(appliedForces, seriesMean, degreeOfFit);

                residualStandardDeviations[i] = CertificateGeneration.MathLib.Statistics.CalculateResidualStandardDeviation(seriesMean, bestFit, degreeOfFit);

                if (i == MAX_DEGREE_OF_FIT)
                    continue;

                if (IsBestFitPolynomialFit(residualStandardDeviations[i], residualStandardDeviations[i + 1], seriesMean.Length, degreeOfFit))
                    break;

                degreeOfFit--;
            }

            return degreeOfFit;
        }

        public bool IsBestFitPolynomialFit(double residualStandardDeviation1, double residualStandardDeveian2, int numOfNonZeroForceIncrements, int degreeOfPolynomialFit)
        {
            return (residualStandardDeviation1 / residualStandardDeveian2) > Statistics.CalculateCFactor(numOfNonZeroForceIncrements, degreeOfPolynomialFit);
        }

        public int GetSeriesSize()
        {
            // TODO consider verifying that all series are the same size

            return seriesList[0].CountValues();
        }

        public double[] CalculateSeriesMean()
        {
            // TODO : better naming

            int seriesSize = GetSeriesSize();

            double[] seriesMean = new double[seriesSize];

            for (int i = 0; i < seriesSize; i++)
                seriesMean[i] = Statistics.CalculateMean(GetSeriesRow(i));

            return seriesMean;
        }

        public double[] GetAppliedForces()
        {
            // TODO: better way to handle this wihout using the first series "[0]"

            const int SERIES_TO_USE = 0;

            return seriesList[SERIES_TO_USE].Transform(new AppliedForceToArray());
        }

        public double[] StackData(ITransformToDoubleArray transform)
        {
            // TODO : add error handling

            // TODO: values need to be ordered

            // TODO: how to test while seriesList is encapsulated

            List<double> stacked = [];

            foreach (var series in seriesList)
                stacked.AddRange(series.Transform(transform));

            return [.. stacked];
        }

        public void RemoveSeriesValueForTestPurpose(int seriesIndex, int itemNumber)
        {
            seriesList[seriesIndex].RemoveSeriesValueForTestPurpose(itemNumber);
        }
    }
}
