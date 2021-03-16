using System;
using System.Collections.Generic;
using System.Text;

namespace TechnodomProject.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Item> Item { get; set; }

        //public DbSet<CategoryItem> CategoryItem { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-S75L8G1; Database=NewDataBase; Trusted_Connection=true;");
        }
    }
}
