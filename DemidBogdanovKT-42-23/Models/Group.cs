using System.Text.RegularExpressions;
namespace DemidBogdanovKT_42_23.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Course { get; set; }
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        public bool IsDeleted { get; set; }
        public static bool IsValidGroupName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            return Regex.IsMatch(name, @"^[A-Za-zА-Яа-я]{2,}-\d{2}-\d{2}$");
        }
    }
}