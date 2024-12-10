﻿using CertificateGeneration.Models;
using CertificateGeneration.IoC.Modifiers;
using DevelopmentTests.InitialZeroDataSets;
using CertificateGeneration.IoC.Modifiers;
using CertificateGeneration.CertificateCalculations.Interpolation;

namespace DevelopmentTests
{
    /// <summary>
    /// Defines the <see cref="InitialZeroValueInterpolatorTests" />
    /// </summary>
    [Ignore]
    [TestClass]
    public class InitialZeroValueInterpolatorTests
    {
        /// <summary>
        /// The interpolateSeries_ValidInput_ReturnsExpectedInterpolatedValues
        /// </summary>
        [TestMethod]
        public void interpolateSeries_ValidInput_ReturnsExpectedInterpolatedValues()
        {
            // Arrange

            // TODO add actualAppliedForce
            double[] appliedForce = MethodAInitialZeroTestData1.GetAppliedForce();
            MeasurementSeries series = MeasurementSeries.CreateSeries(1, appliedForce, appliedForce, MethodAInitialZeroTestData1.GetRawDataSeries1());

            // Act
            InitialZeroValueInterpolator interoplater = new InitialZeroValueInterpolator();
            interoplater.Interpolate(series);

            IModifySeriesSize removeZeroValueForceItems = new RemoveZeroValueForceItems();
            series.Modify(removeZeroValueForceItems);

            IReorderSeries reorderByAppliedForceAscending = new RereorderByAppliedForceAscending();
            series.ReorderSeries(reorderByAppliedForceAscending);

            // Assert
            Assert.AreEqual(-0.08193, series.GetValue(1));
            Assert.AreEqual(-0.40851, series.GetValue(2));
        }
    }
}
