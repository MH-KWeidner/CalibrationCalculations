﻿namespace CertificateGeneration.Models
{
    /// <summary>
    /// Defines the <see cref="IMeasurementPoint" />
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="IMeasurementPoint"/> class.
    /// </remarks>
    /// <param name="appliedForce">The appliedForce<see cref="double"/></param>
    /// <param name="rawValue">The rawValue<see cref="double"/></param>
    public class GenericMeasurementPoint(double appliedForce, double rawValue) : AbstractMeasurementPoint(appliedForce, rawValue)
    {
    }
}
