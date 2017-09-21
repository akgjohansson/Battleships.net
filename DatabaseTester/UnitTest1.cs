using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleships.net.DataBase;
using Battleships.net.DataBase.Builder;

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
    }
}
