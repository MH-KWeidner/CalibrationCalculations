﻿namespace CertificateGeneration.Models
{
    /// <summary>
    /// Defines the <see cref="MeasurementDataPoint" />
    /// </summary>
    public class MeasurementDataPoint
    {
        // TODO condiser if RawValue can be removed. intended only for interpolation

        //TODO consider making as a private internal class

        //TODO consider chaning index property to id property

        //TODO consider removing nullabe from Value property

        // TODO consider removing rawValue

        // TODO better better name for OriginalIndex

        //TODO consider what should be nullable

        /// <summary>
        /// Gets or sets the AppliedForce
        /// </summary>
        public double AppliedForce { get; set; }

        /// <summary>
        /// Gets or sets the RawValue
        /// </summary>
        public double RawValue { get; set; }

        /// <summary>
        /// Gets or sets the Value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementDataPoint"/> class.
        /// </summary>
        /// <param name="index">The index<see cref="int"/></param>
        /// <param name="appliedForce">The appliedForce<see cref="double"/></param>
        /// <param name="rawValue">The rawValue<see cref="double"/></param>
        public MeasurementDataPoint(double appliedForce, double rawValue) => (AppliedForce, RawValue, Value) = (appliedForce, rawValue, rawValue);
    }
}
