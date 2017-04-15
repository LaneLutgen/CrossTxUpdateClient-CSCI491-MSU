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


        public DBManager()
        {
            Initialize();
        }


        private void Initialize()
        {
            server = "localhost";
            database = "crosstx";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "server=" + server + ";" + "database=" +
            database + ";" + "uid=" + uid + ";" + "password=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }


        private bool OpenConnection()
        {
            try
            {
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

        public void SortedInsert()
        {

            try(CsvReader reader = new CsvReader(new FileReader("sampleFile.csv"));){

                QueryGen generator = new QueryGen(reader.readNext());

                String[] nextLine;
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

                while ((nextLine = reader.readNext()) != null) {
                    ++counter;
                    Console.Write(generator.makeQuery(NPIOrganizationData, nextLine));
                    Console.Write(generator.makeQuery(NPIProviderData, nextLine));
                    Console.Write(counter);

                    ExecuteQuery(generator.makeQuery(NPIOrganizationData, nextLine));
                    ExecuteQuery(generator.makeQuery(NPIProviderData, nextLine));
                }

            }
            } catch (MySqlException ex) {
                Console.Write("Error occurred:" + ex);
            }

        }

    }

}