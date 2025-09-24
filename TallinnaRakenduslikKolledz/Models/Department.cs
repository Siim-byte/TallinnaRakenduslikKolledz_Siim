using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledz.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public decimal Budget {  get; set; }
        public DateTime StartDate { get; set; }
        public int? InstructorID { get; set; }
        public Instructor? Administrator { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public byte? RowVersion { get; set; }
        
        public int? Quota { get; set; }
        public string? LeaderName {  get; set; }
        public string? Pet {  get; set; }
    }
}
