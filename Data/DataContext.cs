using Microsoft.EntityFrameworkCore;
using SuperHeroApi.DotNet8.Entities;

namespace SuperHeroApi.DotNet8.Data
{
    // for crating DataBase
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
