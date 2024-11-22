﻿using CertificateGeneration.Models;

namespace CertificateGeneration.Interpolation
{
    /// <summary>
    /// Defines the <see cref="IInterpolate" />
    /// </summary>
    public interface IInterpolate
    {
        /// <summary>
        /// The Interpolate
        /// </summary>
        /// <param name="series">The series<see cref="Series"/></param>
        public void Interpolate(Series series);
    }
}