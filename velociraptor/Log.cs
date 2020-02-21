using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace velociraptor
{
    class Log
    {
        public static void Add(string line)
        {
            string logFileName = "velociraptor.log";

            string dateLine = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss");

            try
            {
                using (StreamWriter sw = new StreamWriter(logFileName, true))
                    sw.WriteLine(dateLine + ' ' + line);
            }
            finally
            {
                // nothing to do here
            }
        }
    }
}
