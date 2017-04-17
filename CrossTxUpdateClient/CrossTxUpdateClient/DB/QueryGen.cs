using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossTxUpdateClient.DB
{
    public class QueryGen
    {

        private Dictionary<String, int> column_index;

        public QueryGen(String[] headers)
        {
            column_index = new Dictionary<String, int>();
            for (int i = 0; i < headers.Length; i++)
            {
                column_index.Add(headers[i], i);
            }
        }

        public String makeQuery(String[] insertColumns, String[] nextLine, string table, int[] columnIndeces)
        {
             String insertInto = "INSERT INTO "+table+" (";
             String values = " VALUES (";
             for(int i = 0; i < insertColumns.Length -1 ; i++)
             {
                String key = insertColumns[i];
                insertInto += key;

                values += "'" + nextLine[columnIndeces[i]].Trim() + "'";
                if (i == (insertColumns.Length - 2))
                {
                    insertInto += ")";
                    values += ")";
                }
                else
                {
                    insertInto += ",";
                    values += ",";
                }
            }

             String query = insertInto + values + ";";
             return query;
        }
    }

}
