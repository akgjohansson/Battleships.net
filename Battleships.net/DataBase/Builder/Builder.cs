using System;
using System.Collections.Generic;
using NHibernate.Cfg;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using System.Reflection;
using NHibernate.Cfg.MappingSchema;

namespace Battleships.net.DataBase.Builder
{
    public class Builder : HandleDataBase
    {
        public void BuildDataBase()
        {
            using (SqlConnection con = new SqlConnection(@"Server = (localdb)\mssqllocaldb;"))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand("if db_id('Battleships') is null create database Battleships", con))
                {
                    command.ExecuteNonQuery();
                }
            }
            Configuration config = new Configuration();

            var schema = new SchemaExport(Configure());
            schema.SetOutputFile("outputFile.sql");
            schema.Create(true, false);
            //schema.Drop(true, true);
            schema.Create(true, true);

        }




        private static Configuration Configure()
        {
            Configuration cfg = new Configuration()
                           .DataBaseIntegration(db =>
                           {
                               db.ConnectionString = ConnectionString + " Trusted_Connection = True;";
                               db.Dialect<MsSql2008Dialect>();
                           });

            var mapper = new ModelMapper();
            Type[] myTypes = Assembly.GetExecutingAssembly().GetExportedTypes();
            mapper.AddMappings(myTypes);

            HbmMapping mapping = new AutoMapper().Map();
            cfg.AddMapping(mapping);

            return cfg;
        }

        
    }
}