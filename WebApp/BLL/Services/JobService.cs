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
    public class JobService : IJobService
    {
        IUnitOfWork Database { get; set; }

        public JobService(IUnitOfWork uow)
        {
            this.Database = uow;
        }

        public void Create(JobPostDTO jobPost)
        {
            if (Database.JobPosts.Get(jobPost.Id) != null)
                throw new ValidationException("Job post already exists", "JobPost");
            var jlMapper = new MapperConfiguration(cfg => cfg.CreateMap<JobLocationDTO, JobLocation>()).CreateMapper();
            var jtMapper = new MapperConfiguration(cfg => cfg.CreateMap<JobTypeDTO, JobType>()).CreateMapper();
            var ssMapper = new MapperConfiguration(cfg => cfg.CreateMap<SkillSetDTO, SkillSet>()).CreateMapper();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPostDTO, JobPost>()
                                                .ForMember(d => d.SkillSets, o => o.MapFrom(s => ssMapper.Map<IEnumerable<SkillSetDTO>, IEnumerable<SkillSet>>(s.SkillSets)))
                                                .ForMember(d => d.JobLocation, o => o.MapFrom(s => jlMapper.Map<JobLocationDTO, JobLocation>(s.JobLocation)))
                                                .ForMember(d => d.JobType, o => o.MapFrom(s => jtMapper.Map<IEnumerable<JobTypeDTO>, IEnumerable<JobType>>(s.JobType)))).CreateMapper();
            var post = mapper.Map<JobPostDTO, JobPost>(jobPost);
            post.SubmitedResumes = new List<SeekerResume>();
            Database.JobPosts.Create(post);
        }

        public IEnumerable<JobPostDTO> Find(IEnumerable<JobTypeDTO> types,
                                                   IEnumerable<DateTime> dateTimes,
                                                   IEnumerable<SkillSetDTO> skillSets) //NEEDS TESTING, DATA COMPARISON COULD BE INCORRECT
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPost, JobPostDTO>()).CreateMapper();
            var posts = mapper.Map<IEnumerable<JobPost>, List<JobPostDTO>>(Database.JobPosts.GetAll());
            if (posts.Count == 0)
                throw new ValidationException("No matches", "JobPost");
            return posts.Where(p => p.JobType.Any(x => types.Contains(x))
                               && dateTimes.Any(x => x < p.CreatedDate)
                               && p.SkillSets.Any(x => skillSets.Contains(x)));
        }

        public IEnumerable<JobPostDTO> Find(string key)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPost, JobPostDTO>()).CreateMapper();
            var posts = mapper.Map<IEnumerable<JobPost>, List<JobPostDTO>>(Database.JobPosts.GetAll());
            if (posts.Count == 0)
                throw new ValidationException("No matches", "JobPost");
            return posts.Where(p => p.CompanyName.ToLower().Contains(key.ToLower()));
        }

        public JobPostDTO Get(int? Id)
        {
            if (Id == null)
                throw new ValidationException("Id not set", "Id");
            var vacancy = Database.JobPosts.Get(Id.Value);
            if (vacancy == null)
                throw new ValidationException("Job post not found", "JobPost");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPost, JobPostDTO>()).CreateMapper();
            return mapper.Map<JobPost, JobPostDTO>(vacancy);
        }

        public IEnumerable<JobPostDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPost, JobPostDTO>()).CreateMapper();
            var posts = mapper.Map<IEnumerable<JobPost>, List<JobPostDTO>>(Database.JobPosts.GetAll());
            if (posts.Count == 0)
                throw new ValidationException("No job posts yet", "JobPost");
            return posts;
        }

        public void Delete(int? Id)
        {
            if (Database.JobPosts.Get(Id.Value) != null)
                Database.JobPosts.Delete(Id.Value);
            else
                throw new ValidationException("Job post doesn not exist", "SeekerResume");
        }

        public void Change(JobPostDTO value) 
        {
            var jlMapper = new MapperConfiguration(cfg => cfg.CreateMap<JobLocationDTO, JobLocation>()).CreateMapper();
            var jtMapper = new MapperConfiguration(cfg => cfg.CreateMap<JobTypeDTO, JobType>()).CreateMapper();
            var ssMapper = new MapperConfiguration(cfg => cfg.CreateMap<SkillSetDTO, SkillSet>()).CreateMapper();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPostDTO, JobPost>()
                                                .ForMember(d => d.SkillSets, o => o.MapFrom(s => ssMapper.Map<IEnumerable<SkillSetDTO>, IEnumerable<SkillSet>>(s.SkillSets)))
                                                .ForMember(d => d.JobLocation, o => o.MapFrom(s => jlMapper.Map<JobLocationDTO, JobLocation>(s.JobLocation)))
                                                .ForMember(d => d.JobType, o => o.MapFrom(s => jtMapper.Map<IEnumerable<JobTypeDTO>, IEnumerable<JobType>>(s.JobType)))).CreateMapper();
            var newPost = mapper.Map<JobPostDTO, JobPost>(value);
            var oldPost = Database.JobPosts.Get(value.Id);
            newPost.SubmitedResumes = oldPost.SubmitedResumes;
            Database.JobPosts.Update(newPost);
        }

        public void NotifySeeker(int senderId, int recieverId) //Implement?
        {
            var resume = Database.SeekerResumes.Get(senderId);
            var vacancy = Database.JobPosts.Get(recieverId);
            if (resume == null)
                throw new ValidationException("Resume does not exist", "SeekerResume");
            if (vacancy == null)
                throw new ValidationException("Vacancy does not exitst", "JobPost");
            resume.VacanciesAcceptedBy.Concat(new[] { vacancy });
            Database.SeekerResumes.Update(resume);
        }

        public IEnumerable<SeekerResumeDTO> ReviewResumes(int id) //Implement?
        {
            var post = Database.JobPosts.Get(id);
            if (post == null)
                throw new ValidationException("Job post does not exist", "JobPost");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResume, SeekerResumeDTO>()).CreateMapper();
            var resumes = mapper.Map<IEnumerable<SeekerResume>, List<SeekerResumeDTO>>(post.SubmitedResumes);
            if (resumes.Count == 0)
                throw new ValidationException("No resumes yet", "SeekerResume");
            return resumes;
        }
    }
}
