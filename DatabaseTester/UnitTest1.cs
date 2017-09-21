using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleships.net.DataBase;
using Battleships.net.DataBase.Builder;
using NHibernate;
using Battleships.net.DataBase.Setup;
using Battleships.net.Services;

namespace DatabaseTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateDataBase()
        {
            Builder builder = new Builder();
            builder.BuildDataBase();
            
        }
        [TestMethod]
        public void TestCreateGrid()
        {
            var session = DbService.OpenSession();
            Setup setup = new Setup(session);
            setup.SetupGrid(8, 8);
            DbService.CloseSession(session);
            //setup.Cleanup();
        }
    }
}
