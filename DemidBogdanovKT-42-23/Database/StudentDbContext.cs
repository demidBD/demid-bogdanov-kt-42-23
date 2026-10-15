using DemidBogdanovKT_42_23.Database.Configurations;
using DemidBogdanovKT_42_23.Models;
using Microsoft.EntityFrameworkCore;

namespace DemidBogdanovKT_42_23.Database
{
    public class StudentDbContext : DbContext
    {
        // Добавляем таблицы
        DbSet<Student> Students { get; set; }
        DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Добавляем конфигурации к таблицам
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
        }

        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
    }
}