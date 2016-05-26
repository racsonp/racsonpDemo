using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using racsonpDemo.Models.Entities;



namespace racsonpDemo.DataAcceses
{
    public class DataBaseContext : DbContext
    {
         public DataBaseContext() : base("DataBaseContext")
        {

        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Producto> Productos{ get; set; }

       // SE COMENTA CUABDO APP HARRBORD

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        //    base.OnModelCreating(modelBuilder);
        //} 
    }
}


/////*

//CREATE TABLE [dbo].[Agent](
//    [Id] [int] IDENTITY(1,1) NOT NULL,
//    [FirstName] [nvarchar](max) NOT NULL,
//    [LastName] [nvarchar](max) NOT NULL,
//    [Biography] [nvarchar](max) NULL,
// CONSTRAINT [PK_dbo.Agent] PRIMARY KEY CLUSTERED 
//(
//    [Id] ASC
//)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
//) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

//GO

