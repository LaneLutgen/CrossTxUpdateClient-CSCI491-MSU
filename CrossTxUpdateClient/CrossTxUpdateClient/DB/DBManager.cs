using System;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using LumenWorks.Framework.IO.Csv;
using System.ComponentModel;
using CrossTxUpdateClient.UpdateAPI;
using System.Windows;
using System.Collections.Generic;

namespace CrossTxUpdateClient.DB
{
    /// <summary>
    /// This class will be responsible for anything and everything database related
    /// </summary>
    public class DBManager
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        private const string orgTable = "npi_organization_data";
        private const string provTable = "npi_provider_data";

        private string filePath;
        private NPI_TYPE type;


        public DBManager(string server, string database, string uid, string password)
        {
            Initialize(server, database, uid, password);
        }


        private void Initialize(string server, string database, string uid, string password)
        {
            this.server = server;
            this.database = database;
            this.uid = uid;
            this.password = password;
            string connectionString;
            connectionString = "server=" + server + ";" + "database=" +
            database + ";" + "uid=" + uid + ";" + "password=" + password + ";";

            try
            {
                connection = new MySqlConnection(connectionString);
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error connecting to the database. Make sure all credentials are correct.");
            }

        }


        public bool OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Unable to connect to server.");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password.");
                        break;
                }
                return false;
            }
        }


        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }


        public void BulkInsert(String FilePath, String Table, String Delimeter)
        {
            if (this.OpenConnection() == true)
            {

                string query = "LOAD DATA LOCAL INFILE" + FilePath + "INTO TABLE" + Table + "FIELDS TERMINATED BY" + Delimeter;
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public void ExecuteQuery(String query)
        {

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public void SortedInsert(string filePath, NPI_TYPE type)
        {
            this.filePath = filePath;
            this.type = type;

            BackgroundWorker dbWorker = new BackgroundWorker();
            dbWorker.DoWork += SortedInsert_DoWork;
            dbWorker.RunWorkerCompleted += SortedInsert_Complete;

            dbWorker.RunWorkerAsync();
        }

        private void SortedInsert_DoWork(object sender, DoWorkEventArgs e)
        {
            //System.Diagnostics.Stopwatch clock = new System.Diagnostics.Stopwatch();

            //clock.Start();

            //this.OpenConnection();

            CsvReader reader = new CsvReader(new StreamReader(filePath), true);
            QueryGen generator = new QueryGen(reader.GetFieldHeaders());

            int[] organizationIndeces = new int[]
                {0, 4, 11, 12, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 42, 43, 45, 313, 46, 47, 48, 49, 50, 307, 308};

            int[] providerIndexes = new int[]
                {0, 5, 6, 8, 9, 10, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 32, 33, 34, 35, 47, 48, 49, 50, 307};

            String[] NPIOrganizationData = {"NPI",
                                            "Name",
                                            "OtherName",
                                            "OtherNameTypeCode",
                                            "FirstLineMailingAddress",
                                            "SecondLineMailingAddress",
                                            "MailingAddressCity",
                                            "MailingAddressState",
                                            "MailingAddressPostalCode",
                                            "MailingAddressCountryCode",
                                            "MailingAddressTelephone",
                                            "MailingAddressFax",
                                            "FirstLinePracticeAddress",
                                            "SecondLinePracticeAddress",
                                            "PracticeAddressCity",
                                            "PracticeAddressState",
                                            "PracticeAddressPostalCode",
                                            "PracticeAddressCountryCode",
                                            "PracticeAddressTelephone",
                                            "PracticeAddressFax",
                                            "AuthorizedOfficialLastName",
                                            "AuthorizedOfficialFirstName",
                                            "AuthorizedOfficialTitle",
                                            "AuthorizedOfficialCredential",
                                            "AuthorizedOfficialTelephone",
                                            "TaxonomyCode1",
                                            "LicenseNumber1",
                                            "LicenseStateCode1",
                                            "TaxonomySwitch1",
                                            "IsSoleProprietor",
                                            "IsOrganizationSubpart",
                                            "DeactivationDate"};

            String[] NPIProviderData = {"NPI",
                                        "ProviderLastName",
                                        "ProviderFirstName",
                                        "ProviderNamePrefix",
                                        "ProviderNameSuffix",
                                        "ProviderCredentialText",
                                        "FirstLineMailingAddress",
                                        "SecondLineMailingAddress",
                                        "MailingAddressCity",
                                        "MailingAddressState",
                                        "MailingAddressPostalCode",
                                        "MailingAddressCountryCode",
                                        "MailingAddressTelephone",
                                        "MailingAddressFax",
                                        "FirstLinePracticeAddress",
                                        "SecondLinePracticeAddress",
                                        "PracticeAddressCity",
                                        "PracticeAddressPostalCode",
                                        "PracticeAddressCountryCode",
                                        "PracticeAddressTelephone",
                                        "PracticeAddressFaxNumber",
                                        "TaxonomyCode1",
                                        "LicenseNumber1",
                                        "LicenseStateCode1",
                                        "TaxonomySwitch1",
                                        "IsSoleProprietor",
                                        "DeactivationDate"};

            int counter = 0;
            String Table1Query;
            String Table2Query;

            //Skip header line
            //reader.ReadNextRecord();

            while (reader.ReadNextRecord())
            {
                string[] line = new string[330];

                ++counter;
                reader.CopyCurrentRecordTo(line);

                Console.WriteLine(counter);
                try
                {
                    if (line[1].Trim().Equals("2"))
                        ExecuteQuery(generator.makeQuery(NPIOrganizationData, line, orgTable, organizationIndeces));
                    else
                        ExecuteQuery(generator.makeQuery(NPIProviderData, line, provTable, providerIndexes));
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("USER ERROR, NOT DEVELOPER (Donnel you cheeky bastard)");
                }

            }

            Console.WriteLine(counter);

            //this.CloseConnection();

            //clock.Stop();
            //Console.WriteLine("Duration: " + clock.ElapsedMilliseconds);
        }

        private void SortedInsert_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            switch(type)
            {
                case NPI_TYPE.Full:
                    AddLinkToDB(DownloadManager.CsvURL, type.ToString());
                    break;
                case NPI_TYPE.Update:
                    AddLinkToDB(DownloadManager.UpdateURL, type.ToString());
                    break;
                case NPI_TYPE.Deactivation:
                    AddLinkToDB(DownloadManager.DeactivationURL, type.ToString());
                    break;
            }
            
        }

        


        /*Deletes a row that exists within either the npi_organization_data table 
        *or the npi_provider_data, using the NPI as a key. Then imports the NPI and 
        *date of deactivation into a separate table to keep track of deactivations.
        */
        public int Remove(string filePath)
        {
            CsvReader reader = new CsvReader(new StreamReader(filePath), true);

            string orginizationsTable = "npi_organization_data";
            string providersTable = "npi_provider_data";
            string deactivationTable = "deactivated_data";

            int counter = 0;

            while (reader.ReadNextRecord())
            {
                string[] line = new string[2];

                reader.CopyCurrentRecordTo(line);
                string NPI = line[0];
                string deactivationDate = line[1];

                string query = "DELETE FROM " + orginizationsTable + " INNER JOIN " + providersTable + " WHERE " + orginizationsTable + ".NPI=" + NPI + " AND " + providersTable + ".NPI=" + NPI;
                ExecuteQuery(query);

                query = "REPLACE INTO " + deactivationTable + " ( NPI, DeactivationDate) VALUES (" + NPI + ", " + deactivationDate + ")";
                ExecuteQuery(query);

                counter++;
            }

            return counter;
        }

        public void AddLinkToDB(string link, string type)
        {
            string[] headers = new string[] { "Link", "Date", "Type" };

            QueryGen generator = new QueryGen(headers);

            string query = generator.makeDownloadLinkQuery(headers, new string[] { link, DateTime.Now.ToString(), type }, "past_links");
            try
            {
                ExecuteQuery(query);
            }
            catch(MySqlException e)
            {
                Console.WriteLine("Bad things happened");
            }
        }

        public List<LinkObject> GetLinksFromDB()
        {
            List<LinkObject> links = new List<LinkObject>();

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * from past_links";

            try
            {
                OpenConnection();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        links.Add(new LinkObject(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
                    }
                }

                CloseConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error connecting to DB");
            }

            return links;
        }
    }
}

