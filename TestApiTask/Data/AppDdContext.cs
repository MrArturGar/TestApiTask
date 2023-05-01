
using Microsoft.EntityFrameworkCore;
using TestApiTask.Models;

namespace TestTask.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<DesignObject> DesignObjects { get; set; }
        public virtual DbSet<DocumentSet> DocumentationSets { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

    }

}
