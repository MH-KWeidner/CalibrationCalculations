using CertificateGeneration.Models;
using CertificateGeneration.IoC.Modifiers;
using CertificateGeneration.CertificateFactory;

using CertificateGeneration.Common;
using CertificateGeneration.CertificateCalculations.Interpolation;

namespace CertificateGenerationTests.CertificatesTesting
{
    // [Ignore]

    /// <summary>
    /// Defines the <see cref="TestE74" />
    /// </summary>
    [TestClass]
    public class TestE74
    {
        /// <summary>
        /// The TestBuild
        /// </summary>
        [TestMethod]
        public void TestBuild()
        {
            // Building this certificate:
            // CALIBRATION & ISSUE DATE: 07/01/2024
            // REPORT NO.: U-7989G0124

            E74Configuration configuration = new()
            {
                InterpolationType = InterpolationTypes.MethodB,
                TemperatureUnits = CertificateGeneration.Common.Temperature.TemperatureUnits.Celsius,
                AmbientTemperature = 50.0,
                TransientForceMeasurementsByIndex = [12],
                ExcludedSeriesByIndex = [],
                DegreeOfFit = DegreeOfFitTypes.DegreeOfBestFit
            };

            ForceApplication application = new(
                DataSets.E74v18DataSet1.GetAppliedForce(),
                DataSets.E74v18DataSet1.GetRawDataSeries1(),
                DataSets.E74v18DataSet1.GetRawDataSeries2(),
                DataSets.E74v18DataSet1.GetRawDataSeries3()
            );

            application.RemoveValuesByIndex(configuration.TransientForceMeasurementsByIndex);

            // NIST Interpolate (Method B). Apply zero reduction and sort.
            application.InterpolateSeriesData(InterpolatorFactory.CreateInterpolator(configuration.InterpolationType));
            application.RemoveValuesByIndex(configuration.TransientForceMeasurementsByIndex);
            application.ModifySeriesData(new RemoveZeroValueForceItems());
            application.OrderSeriesData(new OrderByAppliedForceAscending());
        }
    }
}
