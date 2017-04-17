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

        public String makeQuery(String[] insertColumns, String[] nextLine)
        {
             String insertInto = "insert into table (";
             String values = " values (";
             for(int i = 0; i < insertColumns.Length; i++)
             {
                String key = insertColumns[i];
                insertInto += key;

                int index;
                column_index.TryGetValue(key, out index);

                values += "'" + nextLine[index].Trim() + "'";
                if (i == (insertColumns.Length - 1))
                {
                    insertInto += ",";
                    values += ",";
                }
                else
                {
                    insertInto += ")";
                    values += ")";
                }
            }

             String query = insertInto + values + ";";
             return query;
        }
    }

}
