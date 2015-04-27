using System.Data.Entity;
using Model.Entities;

namespace Model.DBInitializer
{
    public class MultipayContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public MultipayContext() : base("name=MultipayContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Identification> Identifications { get; set; }

        public DbSet<Phone> Phones { get; set; }

        public DbSet<Token> Tokens { get; set; }
    
    }
}
