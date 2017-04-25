using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CrossTxUpdateClient.DB;
using System.Collections.Generic;
using CrossTxUpdateClient.Configurations;

namespace CrossTxUpdateClient.Testing.DBTests
{
    [TestClass]
    public class DBTest
    {
        private string server = "127.0.0.1";
        private string database= "nppes_1";
        private string uid = "root";
        private string password = "4ppropri4teP4ssword";

        [TestMethod]
        public void InitializeDBConnection_Test()
        {
            DBManager db = new DBManager(server, database, uid, password);
            Assert.IsTrue(db.OpenConnection());
        }

        [TestMethod]
        public void LinkStorage_Test()
        {
            List<LinkObject> strings = new List<LinkObject>();
            DBManager db = new DBManager(server, database, uid, password);

            LinkObject testObject = new LinkObject("blah blah blah", "04/24/2017", "Update");

            db.AddLinkToDB(testObject.Link, testObject.Type);

            strings = db.GetLinksFromDB();
            LinkObject[] returnObject = strings.ToArray();

            Assert.IsTrue(returnObject[0].Link.Equals(testObject.Link));
        }

        [TestMethod]
        public void ImportDataIntoDB_Test()
        {
            DBManager db = new DBManager(server, database, uid, password);
            string updateFile = "C:\\Users\\Ben\\Desktop\\CrossTxDownloadTest\\NPPESActivationFile.csv";
            db.SortedInsert(updateFile, UpdateAPI.NPI_TYPE.Update);
            System.Threading.Thread.Sleep(5000);
        }

        [TestMethod]
        public void DeactivateAccounts_Test()
        {
            DBManager db = new DBManager(server, database, uid, password);
            string updateFile = "C:\\Users\\Ben\\Desktop\\CrossTxDownloadTest\\NPPESActivationFile.csv";
            string deactivateFile = "C:\\Users\\Ben\\Desktop\\CrossTxDownloadTest\\NPPESDeactivationFile.csv";

            db.SortedInsert(updateFile, UpdateAPI.NPI_TYPE.Update);
            System.Threading.Thread.Sleep(2000);

            db.Remove(deactivateFile);
            System.Threading.Thread.Sleep(2000);
        }
    }
}
