using CertificateGeneration.Models;
using CertificateGeneration.IoC.DataTransforms;

namespace CertificateGenerationTests.ModelsTests
{
    /// <summary>
    /// Defines the <see cref="DataTransformTests" />
    /// </summary>
    [TestClass]
    public class DataTransformTests
    {
        /// <summary>
        /// The SeriesValueToArray_ValidOutout_ReturnsCorrectValues
        /// </summary>
        [TestMethod]
        public void SeriesValueToArray_ValidOutout_ReturnsCorrectValues()
        {
            // Arrange
            double[] force = [10, 20, 30];
            double[] values = [1.0, 2.0, 3.0];
            Series series = Series.CreateSeries(1, force, values);

            // Act
            double[] result = series.Transform(new SeriesValueToArray());

            // Assert
            Assert.AreEqual(series.CountValues(), result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
        }
    }
}
