using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.SeekerResumeBilder
{
    public class ExperienceDetail
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }
        [ForeignKey("User")]
        public virtual string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

        public bool IsCurrentJob { get; set; }
        [Key]
        [Column(Order = 2, TypeName = "Date")]
        public DateTime StartDate { get; set; }
        [Key]
        [Column(Order = 3, TypeName = "Date")]
        public DateTime EndDate { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string JobLocationCity { get; set; }
        public string JobLocationCountry { get; set; }
        public string Description { get; set; }
    }
}
