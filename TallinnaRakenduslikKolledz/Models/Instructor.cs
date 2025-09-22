using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledz.Models
{
    public class Instructor
    {
        [Key]
        public int ID { get; set; }
        /**/
        [Required]
        [StringLength(50)]
        [Display(Name = "Perekonnanimi")]
        public string LastName { get; set; }
        /**/
        [Required]
        [StringLength(50)]
        [Display(Name = "Eesnimi")]
        public string FirstName { get; set; }
        /**/
        [Display(Name = "Õpetaja nimi")]
        public string FullName
        {
            get {  return FirstName + " " + LastName; }
        }
        /**/
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Tööleasumiskuupäev")]
        public DateTime HirdeDate { get; set; }

        public ICollection<CourseAssignment>? CourseAssignments { get; set; }
        public OfficeAssignment? OfficeAssignment { get; set; }
        /**/

        [Display(Name = "Palk")]
        public int? Salary {  get; set; }
        [Display(Name = "Ruumi number")]
        public int? RoomID { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }

    }
}
