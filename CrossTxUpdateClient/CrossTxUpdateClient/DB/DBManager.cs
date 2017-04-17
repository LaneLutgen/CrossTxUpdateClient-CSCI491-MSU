using System;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using LumenWorks.Framework.IO.Csv;

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
            catch(MySqlException e)
            {
                Console.WriteLine("Error connecting to the database");
            }
            
        }


        public bool OpenConnection()
        {
            try
            {
                if(connection.State == ConnectionState.Closed)
                    connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.Write("Unable to connect to server.");
                        break;

                    case 1045:
                        Console.Write("Invalid username/password.");
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

        public void SortedInsert(string filePath)
        {
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

                while (reader.ReadNextRecord()) {
                    string[] line = new string[330];

                    ++counter;
                    reader.CopyCurrentRecordTo(line);
                    Console.WriteLine(generator.makeQuery(NPIOrganizationData, line, orgTable, organizationIndeces));
                    Console.WriteLine(generator.makeQuery(NPIProviderData, line, provTable, providerIndexes));
                    Console.WriteLine(counter);
                    try
                    {
                        ExecuteQuery(generator.makeQuery(NPIOrganizationData, line, orgTable, organizationIndeces));
                        
                    }
                    catch(MySqlException ex)
                    {
                        Console.WriteLine("USER ERROR, NOT DEVELOPER (Donnel you cheeky bastard)");
                    }

                    try
                    {
                        ExecuteQuery(generator.makeQuery(NPIProviderData, line, provTable, providerIndexes));
                    }
                    catch(MySqlException ex)
                    {
                        Console.WriteLine("USER ERROR, NOT DEVELOPER (Donnel you cheeky bastard)");
                    }
                    
                }

                Console.WriteLine(counter);
            
            } 

        }

    }

