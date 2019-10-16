using System;

namespace CIDER
{
    public static class ExtraMath
    {
        public static double DegToRad(double angle)
        // returns the angle in radians
        {
            return (Math.PI / 180) * angle;
        }

        public static double RadToDeg(double angle)
        // returns the angle in degrees
        {
            return angle * (180.0 / Math.PI);
        }

        public static Tuple<float, float, float> CalculateAngle(float AccelerationX, float AccelerationY, float AccelerationZ)
        //Calculation according to https://www.digikey.com/en/articles/techzone/2011/may/using-an-accelerometer-for-inclination-sensing
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