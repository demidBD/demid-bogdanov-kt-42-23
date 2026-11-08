using DemidBogdanovKT_42_23.Database;
using DemidBogdanovKT_42_23.Models;
using Microsoft.EntityFrameworkCore;

namespace DemidBogdanovKT_42_23.Interfaces
{
    public interface IStudentService
    {
        Task<StudentGradesResponse[]> GetStudentsAsync(StudentFilter filter, CancellationToken ct);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;

        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentGradesResponse[]> GetStudentsAsync(StudentFilter filter, CancellationToken ct)
        {
            var query = _dbContext.Students.Where(s => !s.IsDeleted);

            if (!string.IsNullOrWhiteSpace(filter.LastName))
                query = query.Where(s => s.LastName == filter.LastName);

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
                query = query.Where(s => s.FirstName == filter.FirstName);

            if (!string.IsNullOrWhiteSpace(filter.GroupName))
                query = query.Where(s => s.Group.Name == filter.GroupName);

            var students = await query
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .Select(s => new
                {
                    s.StudentId,
                    s.LastName,
                    s.FirstName,
                    GroupName = s.Group.Name
                })
                .ToListAsync(ct);

            var ids = students.Select(s => s.StudentId).ToList();

            var grades = await _dbContext.Grades
                .Where(g => ids.Contains(g.StudentId))
                .Select(g => new { g.StudentId, g.Value })
                .ToListAsync(ct);

            return students.Select(s => new StudentGradesResponse
            {
                LastName = s.LastName,
                FirstName = s.FirstName,
                GroupName = s.GroupName,
                GradesCount = grades
                    .Where(g => g.StudentId == s.StudentId)
                    .GroupBy(g => g.Value)
                    .OrderBy(g => g.Key)
                    .ToDictionary(g => g.Key, g => g.Count())
            }).ToArray();
        }
    }
}