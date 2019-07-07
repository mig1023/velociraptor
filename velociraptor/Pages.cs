﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;

namespace velociraptor
{
    class Pages
    {
        public int Index;

        public string Title;
        public string MainText;
        public List<string> ButtonsNames = new List<string>();
        public List<int> ButtonsGoto = new List<int>();

        static List<Pages> AllPages = new List<Pages>(); 

        public static void LoadPages()
        {
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.Load("velociraptor.xml");

            foreach(XmlNode xmlPage in xmlFile.SelectNodes("pages/page"))
            {
                Pages page = new Pages();

                page.Index = int.Parse(xmlPage["index"].InnerText);
                page.Title = xmlPage["title"].InnerText;
                page.MainText = xmlPage["mainText"].InnerText;

                foreach (XmlNode xmlButtons in xmlPage.SelectNodes("buttons/button"))
                {
                    page.ButtonsNames.Add(xmlButtons["text"].InnerText);
                    page.ButtonsGoto.Add(int.Parse(xmlButtons["goto"].InnerText));
                }

                AllPages.Add(page);
            }
        }

        public static Pages FindPageByIndex(int index)
        {
            foreach(Pages page in AllPages)
                if (page.Index == index)
                    return page;

            return null;
        }
    }
}