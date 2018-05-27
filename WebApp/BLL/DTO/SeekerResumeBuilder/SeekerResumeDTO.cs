using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.JobPostManagement;

namespace BLL.DTO.SeekerResumeBuilder
{
    public class SeekerResumeDTO
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double? CurrentSalary { get; set; }
        public string IsAnnuallyMonthly { get; set; }
        public string Currency { get; set; }
        public IEnumerable<JobTypeDTO> JobType { get; set; } = new List<JobTypeDTO>();
        public IEnumerable<EducationDetailDTO> EducationDetails { get; set; } = new List<EducationDetailDTO>();
        public IEnumerable<ExperienceDetailDTO> ExperienceDetails { get; set; } = new List<ExperienceDetailDTO>();
        public IEnumerable<SkillSetDTO> SkillSets { get; set; } = new List<SkillSetDTO>();
    }
}
