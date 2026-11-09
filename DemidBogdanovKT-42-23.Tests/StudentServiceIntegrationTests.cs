using DemidBogdanovKT_42_23.Database;
using DemidBogdanovKT_42_23.Interfaces;
using DemidBogdanovKT_42_23.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DemidBogdanovKT_42_23.Tests
{
    public class StudentServiceIntegrationTests
    {
        private readonly DbContextOptions<StudentDbContext> _dbContextOptions;

        public StudentServiceIntegrationTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetStudentsAsync_GroupKT4223_TwoObjects()
        {
            // Arrange
            var ctx = new StudentDbContext(_dbContextOptions);
            var studentService = new StudentService(ctx);

            var group1 = new Group { Name = "KT-42-23", Course = 3, SpecialtyId = 1 };
            var group2 = new Group { Name = "KT-41-23", Course = 3, SpecialtyId = 1 };
            await ctx.Set<Group>().AddRangeAsync(group1, group2);

            var students = new List<Student>
            {
                new Student { FirstName = "Иван",  LastName = "Иванов",  Group = group1 },
                new Student { FirstName = "Пётр",  LastName = "Петров",  Group = group2 },
                new Student { FirstName = "Антон", LastName = "Сидоров", Group = group1 }
            };
            await ctx.Set<Student>().AddRangeAsync(students);
            await ctx.SaveChangesAsync();

            // Act
            var filter = new StudentFilter { GroupName = "KT-42-23" };
            var result = await studentService.GetStudentsAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Length);
            Assert.All(result, r => Assert.Equal("KT-42-23", r.GroupName));
        }

        [Fact]
        public async Task GetStudentsAsync_DeletedStudent_NotReturned()
        {
            // Arrange
            var ctx = new StudentDbContext(_dbContextOptions);
            var studentService = new StudentService(ctx);

            var group = new Group { Name = "KT-42-23", Course = 3, SpecialtyId = 1 };
            await ctx.Set<Group>().AddAsync(group);

            await ctx.Set<Student>().AddRangeAsync(
                new Student { FirstName = "Иван", LastName = "Иванов", Group = group },
                new Student { FirstName = "Пётр", LastName = "Петров", Group = group, IsDeleted = true }
            );
            await ctx.SaveChangesAsync();

            // Act
            var result = await studentService.GetStudentsAsync(new StudentFilter(), CancellationToken.None);

            // Assert
            Assert.Single(result);
            Assert.Equal("Иванов", result[0].LastName);
        }
    }
}