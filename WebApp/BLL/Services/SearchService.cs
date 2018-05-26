using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.JobPostManagement;
using BLL.DTO.SeekerResumeBuilder;
using DAL.Entities.JobPostManagement;
using DAL.Entities.SeekerResumeBilder;
using DAL.Interfaces;
using AutoMapper;

namespace BLL.Services
{
    public class SearchService
    {
        IUnitOfWork Database { get; set; }

        public IEnumerable<JobPostDTO> FindVacancies(IEnumerable<JobTypeDTO> types,
                                                    IEnumerable<DateTime> dateTimes,
                                                    IEnumerable<SkillSetDTO> skillSets) //NEEDS TESTING, DATA COMPARISON COULD BE INCORRECT
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPost, JobPostDTO>()).CreateMapper();
            var posts = mapper.Map<IEnumerable<JobPost>, List<JobPostDTO>>(Database.JobPosts.GetAll());
            return posts.Where(p => p.JobType.Any(x => types.Contains(x))
                               && dateTimes.Any(x => x < p.CreatedDate)
                               && p.SkillSets.Any(x => skillSets.Contains(x)));
        }

        public IEnumerable<SeekerResumeDTO> FindResumes(IEnumerable<SkillSetDTO> skillSets,
                                                    IEnumerable<DateTime> dates) //NEEDS TESTING
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResume, SeekerResumeDTO>()).CreateMapper();
            var resumes = mapper.Map<IEnumerable<SeekerResume>, List<SeekerResumeDTO>>(Database.SeekerResumes.GetAll());
            return resumes.Where(p => p.ExperienceDetails.Any(x => dates.Any(y => (x.EndDate - x.StartDate) >= y.TimeOfDay))
                               && p.SkillSets.Any(x => skillSets.Contains(x)));
        }
    }
}
