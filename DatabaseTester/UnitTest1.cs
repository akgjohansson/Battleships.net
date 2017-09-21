using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleships.net.DataBase;

namespace DatabaseTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCreateDataBase()
        {
            HandleDataBase hdb = new HandleDataBase();
            hdb.BuildDataBase();
        }
    }
}
