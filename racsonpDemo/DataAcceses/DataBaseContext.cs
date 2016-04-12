using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using racsonpDemo.Models.Entities;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace racsonpDemo.DataAcceses
{
    public class DataBaseContext : DbContext
    {
         public DataBaseContext() : base("DataBaseContext")
        {

        }

        public DbSet<Agent> Agents { get; set; }

       // public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        } 
    }
}