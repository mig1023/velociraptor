using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace velociraptor
{
    class OperatorBoard
    {

        const string BOARD_URL = "http://192.168.99.94";
        const string WINDOW = "05";
    
        private static string NUMBER = String.Empty;
        private static int NEW_NUMBER_VISIBILITY = 0;

        private static System.Timers.Timer printingCycl = new System.Timers.Timer(300);

        static OperatorBoard()
        {
            printingCycl.Elapsed += new ElapsedEventHandler(printingPrint);
            printingCycl.Enabled = true;
            printingCycl.Start();
        }

        public static void printingPrint(object obj, ElapsedEventArgs e)
        {
            if (!String.IsNullOrEmpty(NUMBER))
            {
                if (NEW_NUMBER_VISIBILITY <= 10)
                    NEW_NUMBER_VISIBILITY += 1;

                ShowNumber(NUMBER, (NEW_NUMBER_VISIBILITY % 2 == 0));
            }
        }

        private static void ShowNumber(string number, bool hideNumber)
        {
            SendCommand("cls");

            SendCommand("print", WINDOW, 1, 0, 9, 1);
            SendCommand("print", (hideNumber ? String.Empty : number), 2, 18, 25, 2);
            SendCommand("outscr");
        }

        public static void NextNumber(string number)
        {
            NUMBER = number;
            NEW_NUMBER_VISIBILITY = 0;
        }

        public static void Clean()
        {
            NUMBER = String.Empty;

            SendCommand("cls");
            SendCommand("outscr");
        }

        private static void SendCommand(string type, string text = "", int color = 0, int x = 0, int y = 0, int font = 0)
        {
            string sendResult = String.Empty;

            string sendLine = String.Empty;

            if (type == "print")
            {
                sendLine = String.Format(
                    "/?command={0}&text={1}&fcolor={2}&x={3}&y={4}&font={5}&interval=1",
                    type, text, color, x, y, font
                );
            }
            else
                sendLine = String.Format("/?command={0}", type);

            string url = BOARD_URL + sendLine;

            try
            {
                sendResult = SendHttp(url);
            }
            catch (WebException e)
            {
                // nothing to do here
            }
        }

        private static string SendHttp(string url)
        {
            WebClient client = new WebClient();
            using (Stream data = client.OpenRead(url))
            {
                using (StreamReader reader = new StreamReader(data))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
