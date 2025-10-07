using System.ComponentModel.DataAnnotations;

namespace TallinnaRakenduslikKolledz.Models
{
    public enum Violation
    {
        Kaklus = 1,
        Relv = 2,
        Nuga = 3,

    }

    public class Delinquent
    {
        [Key]
        public int Id { get; set; }
        public string Eesnimi { get; set; }
        public string Perekonnanimi { get; set; }
        public Violation? CurrentViolation { get; set; }


    }
}
