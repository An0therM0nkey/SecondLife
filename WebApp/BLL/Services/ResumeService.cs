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
    public class ResumeService : IService<SeekerResumeDTO>
    {
        IUnitOfWork Database { get; set; }

        public ResumeService(IUnitOfWork uow)
        {
            this.Database = uow;
        }

        public bool Create(SeekerResumeDTO resume)
        {
            if (Database.SeekerResumes.Get(resume.Id) != null)
                throw new ValidationException("Job post already exists", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResumeDTO, SeekerResume>()).CreateMapper();
            var newResume = mapper.Map<SeekerResumeDTO, SeekerResume>(resume);
            Database.SeekerResumes.Create(newResume);
            if (Database.SeekerResumes.Get(resume.Id) != null)
                return true;
            else
                return false;

        }

        public IEnumerable<SeekerResumeDTO> Find(IEnumerable<JobTypeDTO> types,
                                                   IEnumerable<DateTime> dateTimes,
                                                   IEnumerable<SkillSetDTO> skillSets) //NEEDS TESTING
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResume, SeekerResumeDTO>()).CreateMapper();
            var resumes = mapper.Map<IEnumerable<SeekerResume>, List<SeekerResumeDTO>>(Database.SeekerResumes.GetAll());
            return resumes.Where(p => p.ExperienceDetails.Any(x => dateTimes.Any(y => (x.EndDate - x.StartDate) >= y.TimeOfDay))
                               && p.SkillSets.Any(x => skillSets.Contains(x))
                               && p.JobType.Any(x => types.Contains(x)));
        }

        public SeekerResumeDTO Get(int? Id)
        {
            if (Id == null)
                throw new ValidationException("Id not set", "");
            var resume = Database.SeekerResumes.Get(Id.Value);
            if (resume == null)
                throw new ValidationException("Resume not found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResume, SeekerResumeDTO>()).CreateMapper();
            return mapper.Map<SeekerResume, SeekerResumeDTO>(resume);
        }

        public IEnumerable<SeekerResumeDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResume, SeekerResumeDTO>()).CreateMapper();
            var resumes = mapper.Map<IEnumerable<SeekerResume>, List<SeekerResumeDTO>>(Database.SeekerResumes.GetAll());
            if (resumes.Count == 0)
                throw new ValidationException("No reumes yet", "");
            return resumes;
        }

        public bool Delete(int? Id)
        {
            SeekerResume resume = Database.SeekerResumes.Get(Id.Value);
            if (resume != null)
                Database.SeekerResumes.Delete(Id.Value);
            if (Database.SeekerResumes.Get(resume.Id) == null)
                return true;
            else
                return false;
        }

        public bool Change(SeekerResumeDTO value)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SeekerResumeDTO, SeekerResume>()).CreateMapper();
            var resume = mapper.Map<SeekerResumeDTO, SeekerResume>(value);
            Database.SeekerResumes.Update(resume);
            if (Database.SeekerResumes.Get(resume.Id).Equals(resume))
                return true;
            else
                return false;
        }
    }
}
