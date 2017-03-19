using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossTxUpdateClient.UpdateAPI
{
    abstract class CSVParser
    {

        protected string filePath;

        public CSVParser()
        {
        }

        public void diffEntry() //Compares the new entry to determine if it needs to be replaced or inserted 
        {
            //If found in DB, replace entry
            if (inDB())
            {
                replaceEntry();
            }
            else //Else
            {
                insertEntry();
            }
        }

        abstract public void insertEntry(); //Inserts if NPI does not exist
        abstract public void replaceEntry(); //Replaces if NPI exists       
       


    }

    class Activate : CSVParser
    {

        public Activate(string path) //Path leading to .csv 
        {
            base.filePath = path;
        }

        public override void insertEntry()
        {
            throw new NotImplementedException();
        }

        public override void replaceEntry()
        {
            throw new NotImplementedException();
        }

    }

    class Deactivate : CSVParser
    {
        public Deactivate(string path) //Path leading to .csv 
        {
            base.filePath = path;
        }

        public override void insertEntry()
        {
            throw new NotImplementedException();
        }

        public override void replaceEntry()
        {
            throw new NotImplementedException();
        }

    }

    class Initialize : CSVParser
    {
        public Initialize(string path) //Path leading to .csv 
        {
            base.filePath = path;
        }

        public override void insertEntry()
        {
            throw new NotImplementedException();
        }

        public override void replaceEntry()
        {
            throw new NotImplementedException();
        }

    }
}
