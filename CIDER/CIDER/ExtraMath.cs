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

        /// <summary>
        /// This function calculates an angle based on accelerometer values
        /// Calculation according to https://www.digikey.com/en/articles/techzone/2011/may/using-an-accelerometer-for-inclination-sensing
        /// </summary>
        /// <param name="AccelerationX">The acceleration in the X direction</param>
        /// <param name="AccelerationY">The acceleration in the X direction</param>
        /// <param name="AccelerationZ">The acceleration in the X direction</param>
        /// <returns>A tuple with the angles in x, y and z direction</returns>
        public static Tuple<float, float, float> CalculateAngle(float AccelerationX, float AccelerationY, float AccelerationZ)
        {
            var result = Math.Sqrt(Math.Pow(AccelerationY, 2) + Math.Pow(AccelerationZ, 2));
            result = AccelerationX / result;
            var Roll = Math.Atan(result);

            result = Math.Sqrt(Math.Pow(AccelerationZ, 2) + Math.Pow(AccelerationX, 2));
            result = AccelerationY / result;
            var Pitch = Math.Atan(result);

            result = Math.Sqrt(Math.Pow(AccelerationY, 2) + Math.Pow(AccelerationX, 2));
            result = AccelerationX / result;
            var Yaw = Math.Atan(result);

            return new Tuple<float, float, float>((float)RadToDeg(Roll), (float)RadToDeg(Pitch), (float)RadToDeg(Yaw));
        }
    }
}