using CalibrationCalculations.IoC.Modifiers;
using CalibrationCalculations.Models;
using CalibrationCalculations.StandardCalculations.Interpolation;
using DevelopmentTests.NISTDataSets;
using DevelopmentTests.TestData.MethodBTestData1;

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
            MeasurementSeries series1 = MeasurementSeries.Create(1, appliedForce, MethodBNistTestData1.GetRawDataSeries1());
            MeasurementSeries series2 = MeasurementSeries.Create(2, appliedForce, MethodBNistTestData1.GetRawDataSeries2());
            MeasurementSeries series3 = MeasurementSeries.Create(3, appliedForce, MethodBNistTestData1.GetRawDataSeries3());

            // Act
            IInterpolate interpolator = InterpolatorFactory.CreateInterpolator(CalibrationCalculations.Common.InterpolationTypes.MethodB);
            MeasurementSeries.Interpolate(interpolator, series1);
            MeasurementSeries.Interpolate(interpolator, series2);
            MeasurementSeries.Interpolate(interpolator, series3);

            List<int> TransientForceMeasurementsByIndex = [12];
            series1.RemoveValuesByIndex(TransientForceMeasurementsByIndex);
            series2.RemoveValuesByIndex(TransientForceMeasurementsByIndex);
            series3.RemoveValuesByIndex(TransientForceMeasurementsByIndex);

            IModifySeriesSize modifier = new RemoveZeroValueForceItems();
            series1.Modify(modifier);
            series2.Modify(modifier);
            series3.Modify(modifier);

            IReorderSeries reorder = new RereorderByAppliedForceAscending();
            series1.ReorderSeries(reorder);
            series2.ReorderSeries(reorder);
            series3.ReorderSeries(reorder);

            List<SingleRunPoint> LabSchedulePointsSeries1 = MethodBLabScheduleResultsTestData1Series1.dataList;
            List<SingleRunPoint> LabSchedulePointsSeries2 = MethodBLabScheduleResultsTestData1Series2.dataList;
            List<SingleRunPoint> LabSchedulePointsSeries3 = MethodBLabScheduleResultsTestData1Series3.dataList;

            // Assert
            Assert.AreEqual(series1.Count(), LabSchedulePointsSeries1.Count);
            Assert.AreEqual(series2.Count(), LabSchedulePointsSeries2.Count);
            Assert.AreEqual(series3.Count(), LabSchedulePointsSeries3.Count);

            const int ROUNDING_DIGITS = 8;

            for (int i = 0; i < series1.Count(); i++)
                Assert.AreEqual(Math.Round(series1.GetValue(i), ROUNDING_DIGITS), (double)LabSchedulePointsSeries1[i].Value);

            for (int i = 0; i < series2.Count(); i++)
                Assert.AreEqual(Math.Round(series2.GetValue(i), ROUNDING_DIGITS), (double)LabSchedulePointsSeries2[i].Value);

            for (int i = 0; i < series3.Count(); i++)
                Assert.AreEqual(Math.Round(series3.GetValue(i), ROUNDING_DIGITS), (double)LabSchedulePointsSeries3[i].Value);
        }
    }
}
