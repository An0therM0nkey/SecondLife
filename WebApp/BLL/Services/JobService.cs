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
    public class JobService : IService<JobPostDTO>
    {
        IUnitOfWork Database { get; set; }

        public JobService(IUnitOfWork uow)
        {
            this.Database = uow;
        }

        public IEnumerable<JobPostDTO> Find(IEnumerable<JobTypeDTO> types,
                                                   IEnumerable<DateTime> dateTimes,
                                                   IEnumerable<SkillSetDTO> skillSets) //NEEDS TESTING, DATA COMPARISON COULD BE INCORRECT
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPost, JobPostDTO>()).CreateMapper();
            var posts = mapper.Map<IEnumerable<JobPost>, List<JobPostDTO>>(Database.JobPosts.GetAll());
            if (posts.Count == 0)
                throw new ValidationException("No matches", "");
            return posts.Where(p => p.JobType.Any(x => types.Contains(x))
                               && dateTimes.Any(x => x < p.CreatedDate)
                               && p.SkillSets.Any(x => skillSets.Contains(x)));
        }

        public JobPostDTO Get(int? Id)
        {
            if (Id == null)
                throw new ValidationException("Id not set", "");
            var vacancy = Database.JobPosts.Get(Id.Value);
            if (vacancy == null)
                throw new ValidationException("Job post not found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPost, JobPostDTO>()).CreateMapper();
            return mapper.Map<JobPost, JobPostDTO>(vacancy);
        }

        public IEnumerable<JobPostDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPost, JobPostDTO>()).CreateMapper();
            var posts = mapper.Map<IEnumerable<JobPost>, List<JobPostDTO>>(Database.JobPosts.GetAll());
            if (posts.Count == 0)
                throw new ValidationException("No job posts yet", "");
            return posts;
        }

        public bool Delete(int? Id)
        {
            JobPost post = Database.JobPosts.Get(Id.Value);
            if (post != null)
                Database.JobPosts.Delete(Id.Value);
            if (Database.JobPosts.Get(post.Id) == null)
                return true;
            else
                return false;
        }

        public bool Change(JobPostDTO value)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobPostDTO, JobPost>()).CreateMapper();
            var post = mapper.Map<JobPostDTO, JobPost>(value);
            Database.JobPosts.Update(post);
            if (Database.JobPosts.Get(post.Id).Equals(post))
                return true;
            else
                return false;
        }
    }
}
