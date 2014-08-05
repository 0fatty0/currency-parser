using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FxParser
{
    public class CurrencyDbContext : DbContext
    {
        public CurrencyDbContext()
        {
            Database.SetInitializer<CurrencyDbContext>(new DropCreateDatabaseAlways<CurrencyDbContext>());
        }

        public DbSet<CurrencyStamp> CurrencyStamps { get; set; }

        
    }
}