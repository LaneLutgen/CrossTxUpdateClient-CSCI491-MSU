using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossTxUpdateClient.UpdateAPI
{
    interface CSVParser<T>
    {
        void Parse();
    }

    public class Activate : CSVParser<Activate>
    {
        string filePath; 
        public Activate(string path) //Path leading to .csv 
        {
            filePath = path;
        }

        void CSVParser<Activate>.Parse()
        {
            throw new NotImplementedException();
        }

    }

    class Deactivate : CSVParser<Deactivate>
    {
        string filePath;
        public Deactivate(string path) //Path leading to .csv 
        {
           filePath = path;
        }

        void CSVParser<Deactivate>.Parse()
        {
            throw new NotImplementedException();
        }

    }
}
