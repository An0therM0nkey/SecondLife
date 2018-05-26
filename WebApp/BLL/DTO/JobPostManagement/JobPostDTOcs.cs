using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.SeekerResumeBuilder;

namespace BLL.DTO.JobPostManagement
{
    public class JobPostDTO
    {
        public int Id { get; set; }
        public string PostedByID { get; set; }
        public int JobTypeID { get; set; }
        public IEnumerable<JobTypeDTO> JobType { get; set; } = new List<JobTypeDTO>();
        public string CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string JobDescription { get; set; }
        public int JobLocationID { get; set; }
        public JobLocationDTO JobLocation { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<SeekerResumeDTO> SubmitedResumes { get; set; } //Containe submited resumes for this vacancy
        public IEnumerable<SkillSetDTO> SkillSets { get; set; } //Skills for this vacancy
    }
}
