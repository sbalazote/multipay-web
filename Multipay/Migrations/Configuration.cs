namespace Multipay.Migrations
{
    using Multipay.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MultipayContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MultipayContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Users.AddOrUpdate(x => x.Id,
        new User() { Id = 1, Email = "pepe@gmail.com", Name = "Pepe123", Surname = "Pepe123", Password = "abc123", Active = true},
        new User() { Id = 2, Email = "test@hotmail.com", Name = "TestABC", Surname = "Pepe123", Password = "abc123", Active = true},
        new User() { Id = 3, Email = "prueba@gmail.com", Name = "Prueba", Surname = "Pepe123", Password = "abc123", Active = true}
        );
            context.SaveChanges(); 
        }
    }
}
