using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace velociraptor
{
    class History
    {
        string Page;
        string Actions;

        public static List<History> Line = new List<History>();

        public History(string nextPage)
        {
            Page = nextPage;
        }
    }
}
