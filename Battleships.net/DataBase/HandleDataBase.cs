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


namespace Battleships.net.DataBase
{
    /// <summary>
    /// Bygger databasen utifrån mappningen
    /// </summary>
    public class HandleDataBase
    {
        public void BuildDataBase()
        {
            Configuration config = new Configuration();
            Configuration cfg = new Configuration()
                           .DataBaseIntegration(db =>
                           {
                               db.ConnectionString = @"Server = (localdb)\mssqllocaldb; Database = Battleships; Trusted_Connection = True;";
                               db.Dialect<MsSql2008Dialect>();
                           });

            var mapper = new ModelMapper();
            Type[] myTypes = Assembly.GetExecutingAssembly().GetExportedTypes();
            mapper.AddMappings(myTypes);

            HbmMapping mapping = new AutoMapper().Map();
            cfg.AddMapping(mapping);
            var schema = new SchemaExport(cfg);
            schema.SetOutputFile("outputFile.dll");

        }
    }
}