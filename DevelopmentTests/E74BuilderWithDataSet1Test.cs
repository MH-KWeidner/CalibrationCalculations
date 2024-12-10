using CertificateGeneration.CertificateCreation;
using CertificateGeneration.Common;

namespace DevelopmentTests;

[TestClass]
public class E74BuilderWithDataSet1Test
{
    [TestMethod]
    public void BuildE74()
    {
        // Building this certificate:
        // CALIBRATION & ISSUE DATE: 07/01/2024
        // REPORT NO.: U-7989G0124

        // Arrange
        E74CertificateConfiguration configuration = new()
        {
            InterpolationType = InterpolationTypes.MethodB,
            TemperatureUnits = TemperatureUnits.Celsius,
            AmbientTemperature = 0.0,
            ApplyTemperatureCorrection = false,
            SelectedDegreeOfFit = DegreeOfFitTypes.CalculatedDegreeOfBestFit
        };

        configuration.AddTransientForceMeasurementsByIndex(12);


    }
}
