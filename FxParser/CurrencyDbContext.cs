using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FxParser
{
    public class CurrencyDbContext : DbContext
    {
        public bool DropDatabaseOnStart = Boolean.Parse(ConfigurationManager.AppSettings["DropDatabaseOnStart"]);
        public CurrencyDbContext()
        {
            if (DropDatabaseOnStart)
            {
                Database.SetInitializer<CurrencyDbContext>(new DropCreateDatabaseAlways<CurrencyDbContext>());
            }
            else
            {
                Database.SetInitializer<CurrencyDbContext>(new CreateDatabaseIfNotExists<CurrencyDbContext>());
            }
            
        }

        public DbSet<CurrencyStamp> CurrencyStamps { get; set; }

        
    }
}