using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER
{
    public class SystemTime
    /*/Summary
     * This class provides the current Time instead of DateTime.Now. Using this class the current time can be faked
     * so it can be used in unit tests. There should be no other mentions of DateTime.Now anywhere in the production code.
    /*/
    {
        private static DateTime _time = DateTime.MinValue;

        public static void Set(DateTime time)
        //Allows setting the "fake" time
        {
            _time = time;
        }

        public static void Reset()
        //Allows resetting of the fake time
        {
            _time = DateTime.MinValue;
        }
        public DateTime Now
        //returns either fake time or current time
        {
            get
            {
                if(_time == DateTime.MinValue)
                {
                    return DateTime.Now;
                }
                return _time;
            }
        }
    }
}
