using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;
using DAL.Interfaces;
using BLL.DTO.JobPostManagement;
using BLL.DTO.SeekerResumeBuilder;
using DAL.Entities.JobPostManagement;
using DAL.Entities.SeekerResumeBilder;
using BLL.Interfaces;
using BLL.Infrastructure;
using AutoMapper;

namespace BLL.Services
{
    public class ResumeService : IResumeService
    {
        IUnitOfWork Database { get; set; }

        public ResumeService(IUnitOfWork uow)
        {
            this.Database = uow;
        }

        public void Create(SeekerResumeDTO resume)
        {
            if (Database.SeekerResumes.Get(resume.Id) != null)
                throw new ValidationException("Job post already exists", "SeekerResume");
            var edMapper = new MapperConfiguration(cfg => cfg.CreateMap<EducationDetailDTO, EducationDetail>()).CreateMapper();
            var exMapper = new MapperConfiguration(cfg => cfg.CreateMap<ExperienceDetailDTO, ExperienceDetail>()).CreateMapper();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResumeDTO, SeekerResume>()
                                                .ForMember(d => d.EducationDetails, o => o.MapFrom(s => edMapper.Map<IEnumerable<EducationDetailDTO>, IEnumerable<EducationDetail>>(s.EducationDetails)))
                                                .ForMember(d => d.ExperienceDetails, o => o.MapFrom(s => exMapper.Map<IEnumerable<ExperienceDetailDTO>, IEnumerable<ExperienceDetail>>(s.ExperienceDetails)))).CreateMapper();
            var newResume = mapper.Map<SeekerResumeDTO, SeekerResume>(resume);
            newResume.VacanciesAcceptedBy = new List<JobPost>();
            Database.SeekerResumes.Create(newResume);
        }

        public IEnumerable<SeekerResumeDTO> Find(IEnumerable<JobTypeDTO> types,
                                                   IEnumerable<DateTime> dateTimes,
                                                   IEnumerable<SkillSetDTO> skillSets) //NEEDS TESTING
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResume, SeekerResumeDTO>()).CreateMapper();
            var resumes = mapper.Map<IEnumerable<SeekerResume>, List<SeekerResumeDTO>>(Database.SeekerResumes.GetAll());
            if (resumes.Count == 0)
                throw new ValidationException("No matches", "SeekerResume");
            return resumes.Where(p => p.ExperienceDetails.Any(x => dateTimes.Any(y => (x.EndDate - x.StartDate) >= y.TimeOfDay))
                               && p.SkillSets.Any(x => skillSets.Contains(x))
                               && p.JobType.Any(x => types.Contains(x)));
        }

        public IEnumerable<SeekerResumeDTO> Find(string key)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResume, SeekerResumeDTO>()).CreateMapper();
            var resumes = mapper.Map<IEnumerable<SeekerResume>, List<SeekerResumeDTO>>(Database.SeekerResumes.GetAll());
            if (resumes.Count == 0)
                throw new ValidationException("No matches", "SeekerResume");
            return resumes.Where(p => p.FirstName.ToLower().Contains(key.ToLower()) 
                                || p.LastName.ToLower().Contains(key.ToLower()));
        }

        public SeekerResumeDTO Get(int? Id)
        {
            if (Id == null)
                throw new ValidationException("Id not set", "Id");
            var resume = Database.SeekerResumes.Get(Id.Value);
            if (resume == null)
                throw new ValidationException("Resume not found", "SeekerResume");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResume, SeekerResumeDTO>()).CreateMapper();
            return mapper.Map<SeekerResume, SeekerResumeDTO>(resume);
        }

        public IEnumerable<SeekerResumeDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResume, SeekerResumeDTO>()).CreateMapper();
            var resumes = mapper.Map<IEnumerable<SeekerResume>, List<SeekerResumeDTO>>(Database.SeekerResumes.GetAll());
            if (resumes.Count == 0)
                throw new ValidationException("No resumes yet", "SeekerResume");
            return resumes;
        }

        public void Delete(int? Id)
        {
            SeekerResume resume = Database.SeekerResumes.Get(Id.Value);
            if (resume != null)
                Database.SeekerResumes.Delete(Id.Value);
            else
                throw new ValidationException("Resume does not exist", "SeekerResume");
        }

        public void Change(SeekerResumeDTO value)
        {
            var edMapper = new MapperConfiguration(cfg => cfg.CreateMap<EducationDetailDTO, EducationDetail>()).CreateMapper();
            var exMapper = new MapperConfiguration(cfg => cfg.CreateMap<ExperienceDetailDTO, ExperienceDetail>()).CreateMapper();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResumeDTO, SeekerResume>()
                                                .ForMember(d => d.EducationDetails, o => o.MapFrom(s => edMapper.Map<IEnumerable<EducationDetailDTO>, IEnumerable<EducationDetail>>(s.EducationDetails)))
                                                .ForMember(d => d.ExperienceDetails, o => o.MapFrom(s => exMapper.Map<IEnumerable<ExperienceDetailDTO>, IEnumerable<ExperienceDetail>>(s.ExperienceDetails)))).CreateMapper();
            var newResume = mapper.Map<SeekerResumeDTO, SeekerResume>(value);
            var oldResume = Database.SeekerResumes.Get(value.Id);
            newResume.VacanciesAcceptedBy = oldResume.VacanciesAcceptedBy;
            Database.SeekerResumes.Update(newResume);
        }

        public void SendResume(int senderId, int recieverId)
        {
            var resume = Database.SeekerResumes.Get(senderId);
            var vacancy = Database.JobPosts.Get(recieverId);
            if (resume == null)
                throw new ValidationException("Resume does not exist", "SeekerResume");
            if (vacancy == null)
                throw new ValidationException("Vacancy does not exitst", "JobPost");
            vacancy.SubmitedResumes.Concat(new[] { resume });
            Database.JobPosts.Update(vacancy);
        }

        public IEnumerable<JobPostDTO> ReviewVacancies(int id)
        {
            var resume = Database.SeekerResumes.Get(id);
            if (resume == null)
                throw new ValidationException("Resume does not exist", "Seeker Resume");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPost, JobPostDTO>()).CreateMapper();
            var posts = mapper.Map<IEnumerable<JobPost>, List<JobPostDTO>>(resume.VacanciesAcceptedBy);
            if (posts.Count == 0)
                throw new ValidationException("No posts yet", "JobPost");
            return posts;
        }
    }
}
