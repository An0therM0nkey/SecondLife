using DAL.Entities.SeekerResumeBilder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.JobPostManagement
{
    public class JobPost
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public virtual string PostedByID { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual int JobTypeID { get; set; }
        public virtual IEnumerable<JobType> JobType { get; set; } = new List<JobType>();

        public string CompanyName { get; set; }
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { get; set; }
        public string JobDescription { get; set; }

        [ForeignKey("JobLocation")]
        public virtual int JobLocationID { get; set; }
        public virtual JobLocation JobLocation { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<SeekerResume> SubmitedResumes { get; set; } //Containe submited resumes for this vacancy
        public IEnumerable<SkillSet> SkillSets { get; set; } //Skills for this vacancy
    }
}
