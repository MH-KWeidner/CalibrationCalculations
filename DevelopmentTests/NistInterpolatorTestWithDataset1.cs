using CertificateGeneration.Models;
using CertificateGeneration.IoC.Modifiers;
using DevelopmentTests.NISTDataSets;
using CertificateGeneration.CertificateCalculations.Interpolation;
using static System.Net.Mime.MediaTypeNames;

namespace DevelopmentTests
{
    /// <summary>
    /// Defines the <see cref="NistInterpolatorTestWithDataset1" />
    /// </summary>
    [TestClass]
    public class NistInterpolatorTestWithDataset1
    {
        /// <summary>
        /// The InterpolateSeries_ValidInput_ReturnsExpectedInterpolatedValues
        /// </summary>
        [TestMethod]
        public void InterpolateSeries_ValidInput_ReturnsExpectedInterpolatedValues()
        {
            // Arrange
            double[] appliedForce = MethodBNistTestData1.GetAppliedForce();
            Series series1 = Series.CreateSeries(1, appliedForce, MethodBNistTestData1.GetRawDataSeries1());
            Series series2 = Series.CreateSeries(2, appliedForce, MethodBNistTestData1.GetRawDataSeries2());
            Series series3 = Series.CreateSeries(3, appliedForce, MethodBNistTestData1.GetRawDataSeries3());

            // Act
            IInterpolate interpolator = InterpolatorFactory.CreateInterpolator(CertificateGeneration.Common.InterpolationTypes.MethodB);
            series1.Interpolate(interpolator);
            series2.Interpolate(interpolator);
            series3.Interpolate(interpolator);

            List<int> TransientForceMeasurementsByIndex = [12];
            series1.RemoveValuesByIndex(TransientForceMeasurementsByIndex);
            series2.RemoveValuesByIndex(TransientForceMeasurementsByIndex);
            series3.RemoveValuesByIndex(TransientForceMeasurementsByIndex);

            IModifySeriesSize modifier = new RemoveZeroValueForceItems();
            series1.Modify(modifier);
            series2.Modify(modifier);
            series3.Modify(modifier);

            IOrderSeries reorder = new OrderByAppliedForceAscending();
            series1.Order(reorder);
            series2.Order(reorder);
            series3.Order(reorder);

            List<SingleRunPoint> LabSchedulePointsSeries1 = MethodBLabScheduleResultsTestData1Series1.dataList;
            List<SingleRunPoint> LabSchedulePointsSeries2 = MethodBLabScheduleResultsTestData1Series2.dataList;
            List<SingleRunPoint> LabSchedulePointsSeries3 = MethodBLabScheduleResultsTestData1Series3.dataList;

            // Assert
            Assert.AreEqual(series1.CountValues(), LabSchedulePointsSeries1.Count);
            Assert.AreEqual(series2.CountValues(), LabSchedulePointsSeries2.Count);
            Assert.AreEqual(series3.CountValues(), LabSchedulePointsSeries3.Count);

            const int ROUNDING_DIGITS = 8;

            for (int i = 0; i < series1.CountValues(); i++)
                Assert.AreEqual(Math.Round(series1.GetValue(i), ROUNDING_DIGITS), (double)LabSchedulePointsSeries1[i].Value);

            for (int i = 0; i < series2.CountValues(); i++)
                Assert.AreEqual(Math.Round(series2.GetValue(i), ROUNDING_DIGITS), (double)LabSchedulePointsSeries2[i].Value);

            for (int i = 0; i < series3.CountValues(); i++)
                Assert.AreEqual(Math.Round(series3.GetValue(i), ROUNDING_DIGITS), (double)LabSchedulePointsSeries3[i].Value);
        }
    }
}
