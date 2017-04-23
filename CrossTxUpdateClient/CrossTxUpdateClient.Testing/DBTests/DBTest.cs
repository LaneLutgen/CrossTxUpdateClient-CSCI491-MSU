using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CrossTxUpdateClient.DB;
using System.Collections.Generic;

namespace CrossTxUpdateClient.Testing.DBTests
{
    [TestClass]
    public class DBTest
    {
        [TestMethod]
        public void LinkStorage_Test()
        {
            List<LinkObject> strings = new List<LinkObject>();
            DBManager db = new DBManager("127.0.0.1", "nppes_1", "root", "<password>");

            db.AddLinkToDB("blah blah blah", "Update");
            strings = db.GetLinksFromDB();
        }
    }
}
