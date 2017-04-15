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
            Iterator<String> first = Arrays.asList(insertColumns).iterator();
            String insertInto = "insert into table (";
            String values = " values (";
            while (first.hasNext())
            {
                String key = first.next();
                insertInto += key;
                values += "'" + nextLine[column_index."?????"(key)].trim() + "'";
                if (first.hasNext())
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
