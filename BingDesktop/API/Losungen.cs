using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BingDesktop.API
{
    public static class Losungen
    {
        public static string ReadFromXml(string filename, DateTime time)
        {
            var doc = XDocument.Load(filename);
            var losung = doc.Descendants("Losungen").
                FirstOrDefault(l => l.Element("Datum").Value == time.ToString("yyyy-MM-dd") + "T00:00:00");

            var text = losung.Element("Losungstext").Value;
            var vers = losung.Element("Losungsvers").Value;

            return text + Environment.NewLine + "(" + vers + ")";
        }
    }
}
