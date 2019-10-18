using System;

namespace CIDER
{
    public class ColorWriter
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IKeyManagerReader _reader;

        public ColorWriter(IKeyManagerReader reader)
        {
            _reader = reader;
        }

        public Tuple<string, string> GetSetTheming()
        {
            try
            {
                string[] cfg = _reader.ReadAllLines("CIDER.cfg");

                string Theme = cfg[1];
                string Accent = cfg[2];

                return new Tuple<string, string>(Theme, Accent);
            }
            catch (IndexOutOfRangeException ex)
            {
                logger.Info(ex, "No Color Found");
                throw new ColorWriterNoColorException();
            }
        }

        public void SetTheming(string Accent, string Theme)
        {
            try
            {
                string[] cfg = _reader.ReadAllLines("CIDER.cfg");
                string[] newcfg = { "", Theme, Accent }; ;

                try
                {
                    newcfg[0] = cfg[0];
                }
                catch (IndexOutOfRangeException ex)
                {
                    logger.Info(ex, "no api key set");
                    newcfg[0] = "";
                }
                _reader.WriteAllLines(newcfg, "CIDER.cfg");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error whilst adding API Key");
                throw new ColorWriterWritingException();
            }
        }
    }
}