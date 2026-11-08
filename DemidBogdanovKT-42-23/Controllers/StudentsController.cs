using DemidBogdanovKT_42_23.Interfaces;
using DemidBogdanovKT_42_23.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemidBogdanovKT_42_23.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("GetStudents")]
        public async Task<IActionResult> GetStudentsAsync(StudentFilter filter, CancellationToken ct = default)
        {
            var result = await _studentService.GetStudentsAsync(filter, ct);
            return Ok(result);
        }
    }
}