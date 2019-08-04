namespace DAL
{
    using DAL.Models;
    using Microsoft.EntityFrameworkCore;

    public class APIContext : DbContext
    {
        public APIContext()
        {
            Database.EnsureCreated();
        }

        public APIContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<AttributeDAL> Attributes { get; set; }

        public virtual DbSet<EntityDAL> Entities { get; set; }

        public virtual DbSet<TypeDAL> Types { get; set; }

        public virtual DbSet<StringAttribute> StringAttribute { get; set; }

        public virtual DbSet<StringRequired> StringRequired { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=DB_TestTask;Integrated Security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeDAL>()
            .HasMany(i => i.Attributes).WithOne(o => o.TypeDAL).HasForeignKey(p => p.TypeDALId)
            .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TypeDAL>()
            .HasMany(i => i.Attributes).WithOne(o => o.TypeDAL).HasForeignKey(p => p.TypeDALId)
            .OnDelete(DeleteBehavior.Cascade);
            
        }
    }


}