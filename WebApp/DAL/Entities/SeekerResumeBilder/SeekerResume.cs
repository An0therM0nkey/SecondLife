using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.JobPostManagement;

namespace DAL.Entities.SeekerResumeBilder
{
    public class SeekerResume
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public virtual string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        
        public virtual IEnumerable<JobType> JobType { get; set; } = new List<JobType>();

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double? CurrentSalary { get; set; }
        public string IsAnnuallyMonthly { get; set; }
        public string Currency { get; set; }

        public IEnumerable<EducationDetail> EducationDetails { get; set; } = new List<EducationDetail>();
        public IEnumerable<ExperienceDetail> ExperienceDetails { get; set; } = new List<ExperienceDetail>();
        public IEnumerable<SkillSet> SkillSets { get; set; } = new List<SkillSet>();

    }
}
