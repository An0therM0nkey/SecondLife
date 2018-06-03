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

        IMapper mapperFromDTO,
                mapperToDTO;

        public ResumeService(IUnitOfWork uow)
        {
            this.Database = uow;

            var edMapper = new MapperConfiguration(cfg => cfg.CreateMap<EducationDetailDTO, EducationDetail>()).CreateMapper();
            var exMapper = new MapperConfiguration(cfg => cfg.CreateMap<ExperienceDetailDTO, ExperienceDetail>()).CreateMapper();
            var jtMapper = new MapperConfiguration(cfg => cfg.CreateMap<JobTypeDTO, JobType>()).CreateMapper();
            var ssMapper= new MapperConfiguration(cfg => cfg.CreateMap<SkillSetDTO, SkillSet>()).CreateMapper();
            mapperFromDTO = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResumeDTO, SeekerResume>()
                                                .ForMember(d => d.SkillSets, o => o.MapFrom(s => ssMapper.Map<IEnumerable<SkillSetDTO>, IEnumerable<SkillSet>>(s.SkillSets)))
                                                .ForMember(d => d.JobType, o => o.MapFrom(s => jtMapper.Map<IEnumerable<JobTypeDTO>, IEnumerable<JobType>>(s.JobType)))
                                                .ForMember(d => d.EducationDetails, o => o.MapFrom(s => edMapper.Map<IEnumerable<EducationDetailDTO>, IEnumerable<EducationDetail>>(s.EducationDetails)))
                                                .ForMember(d => d.ExperienceDetails, o => o.MapFrom(s => exMapper.Map<IEnumerable<ExperienceDetailDTO>, IEnumerable<ExperienceDetail>>(s.ExperienceDetails)))).CreateMapper();
            var edMapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<EducationDetail, EducationDetailDTO>()).CreateMapper();
            var exMapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<ExperienceDetail, ExperienceDetailDTO>()).CreateMapper();
            var jtMapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<JobType, JobTypeDTO>()).CreateMapper();
            var ssMapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<SkillSet, SkillSetDTO>()).CreateMapper();
            mapperToDTO = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResume, SeekerResumeDTO>()
                                                .ForMember(d => d.SkillSets, o => o.MapFrom(s => ssMapperToDTO.Map<IEnumerable<SkillSet>, IEnumerable<SkillSetDTO>>(s.SkillSets)))
                                                .ForMember(d => d.JobType, o => o.MapFrom(s => jtMapperToDTO.Map<IEnumerable<JobType>, IEnumerable<JobTypeDTO>>(s.JobType)))
                                                .ForMember(d => d.EducationDetails, o => o.MapFrom(s => edMapperToDTO.Map<IEnumerable<EducationDetail>, IEnumerable<EducationDetailDTO>>(s.EducationDetails)))
                                                .ForMember(d => d.ExperienceDetails, o => o.MapFrom(s => exMapperToDTO.Map<IEnumerable<ExperienceDetail>, IEnumerable<ExperienceDetailDTO>>(s.ExperienceDetails)))).CreateMapper();
        }

        public void Create(SeekerResumeDTO resume)
        {
            if (Database.SeekerResumes.Get(resume.Id) != null)
                throw new ValidationException("Job post already exists", "SeekerResume");
            var newResume = mapperFromDTO.Map<SeekerResumeDTO, SeekerResume>(resume);
            newResume.VacanciesAcceptedBy = new List<JobPost>();
            Database.SeekerResumes.Create(newResume);
        }

        public IEnumerable<SeekerResumeDTO> Find(IEnumerable<JobTypeDTO> types,
                                                   IEnumerable<DateTime> dateTimes,
                                                   IEnumerable<SkillSetDTO> skillSets) //NEEDS TESTING
        {
            var resumes = mapperToDTO.Map<IEnumerable<SeekerResume>, List<SeekerResumeDTO>>(Database.SeekerResumes.GetAll());
            if (resumes.Count == 0)
                throw new ValidationException("No matches", "SeekerResume");
            return resumes.Where(p => p.ExperienceDetails.Any(x => dateTimes.Any(y => (x.EndDate - x.StartDate) >= y.TimeOfDay))
                               && p.SkillSets.Any(x => skillSets.Contains(x))
                               && p.JobType.Any(x => types.Contains(x)));
        }

        public IEnumerable<SeekerResumeDTO> Find(string key)
        {
            var resumes = mapperToDTO.Map<IEnumerable<SeekerResume>, List<SeekerResumeDTO>>(Database.SeekerResumes.GetAll());
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
            return mapperToDTO.Map<SeekerResume, SeekerResumeDTO>(resume);
        }

        public IEnumerable<SeekerResumeDTO> GetAll()
        {
            var resumes = mapperToDTO.Map<IEnumerable<SeekerResume>, List<SeekerResumeDTO>>(Database.SeekerResumes.GetAll());
            if (resumes.Count == 0)
                throw new ValidationException("No resumes yet", "SeekerResume");
            return resumes;
        }

        public IEnumerable<SeekerResumeDTO> GetAll(string id)
        {
            var resumes = mapperToDTO.Map<IEnumerable<SeekerResume>, List<SeekerResumeDTO>>(Database.SeekerResumes.GetAll());
            if (resumes.Count == 0)
                throw new ValidationException("No matches", "SeekerResume");
            return resumes.Where(p => p.UserID == id);
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
            var newResume = mapperFromDTO.Map<SeekerResumeDTO, SeekerResume>(value);
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
            var jlMapper = new MapperConfiguration(cfg => cfg.CreateMap<JobLocation, JobLocationDTO>()).CreateMapper();
            var jtMapper = new MapperConfiguration(cfg => cfg.CreateMap<JobType, JobTypeDTO>()).CreateMapper();
            var ssMapper = new MapperConfiguration(cfg => cfg.CreateMap<SkillSet, SkillSetDTO>()).CreateMapper();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPost, JobPostDTO>()
                                                .ForMember(d => d.SkillSets, o => o.MapFrom(s => ssMapper.Map<IEnumerable<SkillSet>, IEnumerable<SkillSetDTO>>(s.SkillSets)))
                                                .ForMember(d => d.JobLocation, o => o.MapFrom(s => jlMapper.Map<JobLocation, JobLocationDTO>(s.JobLocation)))
                                                .ForMember(d => d.JobType, o => o.MapFrom(s => jtMapper.Map<IEnumerable<JobType>, IEnumerable<JobTypeDTO>>(s.JobType)))).CreateMapper();
            var posts = mapper.Map<IEnumerable<JobPost>, List<JobPostDTO>>(resume.VacanciesAcceptedBy);
            if (posts.Count == 0)
                throw new ValidationException("No posts yet", "JobPost");
            return posts;
        }
    }
}
