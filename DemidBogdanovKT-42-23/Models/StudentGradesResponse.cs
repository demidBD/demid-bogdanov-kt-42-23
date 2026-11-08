namespace DemidBogdanovKT_42_23.Models
{
    public class StudentGradesResponse
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public Dictionary<int, int> GradesCount { get; set; } = new();
    }
}