/* Copyright (C) 2020  Johannes Schiemer 
	This program is free software: you can redistribute it and/or modify 
	it under the terms of the GNU General Public License as published by 
	the Free Software Foundation, either version 3 of the License, or 
	(at your option) any later version. 
	This program is distributed in the hope that it will be useful, 
	but WITHOUT ANY WARRANTY; without even the implied warranty of 
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
	GNU General Public License for more details. 
	You should have received a copy of the GNU General Public License 
	along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/
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