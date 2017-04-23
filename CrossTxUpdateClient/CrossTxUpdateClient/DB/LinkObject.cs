using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossTxUpdateClient.DB
{
    public class LinkObject
    {
        public string Link { get; set; }

        public string Date { get; set; }

        public string Type { get; set; }

        public LinkObject(string link, string date, string type)
        {
            Link = link;
            Date = date;
            Type = type;
        }
    }
}
