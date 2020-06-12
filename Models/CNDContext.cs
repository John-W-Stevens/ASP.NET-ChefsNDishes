using System;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Models
{
    public class CNDContext : DbContext
    {
        public CNDContext(DbContextOptions options) : base(options) { }

        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Dish> Dishes { get; set; }
    }
}

