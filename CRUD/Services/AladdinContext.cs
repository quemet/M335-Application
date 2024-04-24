using CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Services
{
    public class AladdinContext : DbContext
    {
        public DbSet<Wish> Wishes { get; set; }

        public AladdinContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "aladdin.sqlite");
            options.UseSqlite($"Filename={dbPath}");
        }
    }
}