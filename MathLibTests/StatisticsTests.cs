﻿namespace MathLib.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MathLib;
    using MathNet.Numerics;

    namespace MathLib.Tests
    {
        [TestClass]
        public class StatisticsTests
        {
            [TestMethod]
            public void FitPolynomialToLeastSquares_ValidInput_ReturnsExpectedCoefficients()
            {
                // Arrange
                double[] x = { 1, 2, 3, 4, 5 };
                double[] y = { 1, 4, 9, 16, 25 };
                const int degree = 2;

                // Act
                double[] result = Statistics.FitPolynomialToLeastSquares(x, y, degree);

                // Assert
                Assert.AreEqual(3, result.Length);
                Assert.IsTrue(Math.Abs(result[0] - 0) < 1e-6);
                Assert.IsTrue(Math.Abs(result[1] - 0) < 1e-6);
                Assert.IsTrue(Math.Abs(result[2] - 1) < 1e-6);
            }

            [TestMethod]
            public void FitPolynomialToLeastSquares_InvalidInput_ThrowsException()
            {
                // Arrange
                double[] x = { 1, 2, 3 };
                double[] y = { 1, 4 };
                const int degree = 2;

                // Act & Assert
                var exception = Assert.ThrowsException<Exception>(() => Statistics.FitPolynomialToLeastSquares(x, y, degree));
                Assert.AreEqual("Error in Statistics.FitPolynomialToLeastSquares", exception.Message);
            }
        }
    }
}