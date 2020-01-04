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
    /// This class provides the current Time instead of DateTime.Now. Using this class the current time can be faked
    /// so it can be used in unit tests.There should be no other mentions of DateTime.Now anywhere in the production code.
    /// </summary>
    public class SystemTime
    {
        private static DateTime _time = DateTime.MinValue;

        /// <summary>
        /// Allows setting the "fake" time
        /// </summary>
        /// <param name="time">The time to be set</param>
        public static void Set(DateTime time)
        {
            _time = time;
        }

        /// <summary>
        /// Allows resetting of the fake time
        /// </summary>
        public static void Reset()
        {
            _time = DateTime.MinValue;
        }

        /// <summary>
        /// returns either fake time or current time
        /// </summary>
        public DateTime Now
        {
            get
            {
                if (_time == DateTime.MinValue)
                {
                    return DateTime.Now;
                }
                return _time;
            }
        }
    }
}