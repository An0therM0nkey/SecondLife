using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.SeekerResumeBilder
{
    public class EducationDetail
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }
        [ForeignKey("User")]
        public virtual string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Key]
        [Column(Order = 2)]
        public string CertificateDegreeName { get; set; }
        [Key]
        [Column(Order = 3)]
        public string Major { get; set; }
        public string InstituteUniversityName { get; set; }
        [Column(TypeName = "Date")] // May be an error, "Sequence contains no matching element", if DB does not support date.
        public DateTime StartingDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? CompletionDate { get; set; }
    }
}
