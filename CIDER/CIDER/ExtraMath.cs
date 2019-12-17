using System;

namespace CIDER
{
    /// <summary>
    /// This class contains static functions to calculate different equations
    /// </summary>
    public static class ExtraMath
    {
        /// <summary>
        /// This functions converts angles from degrees to radians
        /// </summary>
        /// <param name="angle">The angle in degrees</param>
        /// <returns>The given angle in radians</returns>
        public static double DegToRad(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        /// <summary>
        /// This functions converts angles from radians to degrees
        /// </summary>
        /// <param name="angle">The angle in radians</param>
        /// <returns>The given angle in degrees</returns>
        public static double RadToDeg(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
    }
}