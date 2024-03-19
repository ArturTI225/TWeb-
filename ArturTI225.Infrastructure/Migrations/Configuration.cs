
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ArturTI225.Infrastructure.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CarsShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}