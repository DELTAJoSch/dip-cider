using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER
{
    public static class ExtraMath
    {
        public static double DegToRad(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public static double RadToDeg(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
    }
}
