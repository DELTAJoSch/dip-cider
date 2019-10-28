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