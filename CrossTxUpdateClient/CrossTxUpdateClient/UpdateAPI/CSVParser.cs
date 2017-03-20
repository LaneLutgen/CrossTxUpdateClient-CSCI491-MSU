using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossTxUpdateClient.DB;

namespace CrossTxUpdateClient.UpdateAPI
{
    interface CSVParser<T>
    {
        void Parse();
        void Update();
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

        void CSVParser<Activate>.Update()
        {
            
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

        void CSVParser<Deactivate>.Update()
        {
            DBManager DB = new DBManager();
            //DB.deleteEntry() //
        }

    }
}
