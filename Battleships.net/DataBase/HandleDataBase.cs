using System;
using System.Collections.Generic;
using NHibernate.Cfg;
using System.Linq;
using System.Web;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using System.Reflection;
using NHibernate.Dialect;
using System.Data.SqlClient;

namespace Battleships.net.DataBase
{
    /// <summary>
    /// Bygger databasen utifrån mappningen
    /// </summary>
    public class HandleDataBase
    {
        public static string ConnectionString { get; set; }
        public HandleDataBase()
        {
            ConnectionString = @"Server = (localdb)\mssqllocaldb; Database = Battleships;";//
        }
        public HandleDataBase(string connectionString)
        {
            ConnectionString = connectionString;
        }



    }
}