using DAL.Entities.JobPostManagement;
using DAL.Entities.SeekerResumeBilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<JobPost> JobPosts { get; }
        IRepository<JobLocation> JobLocations { get; }
        IRepository<JobType> JobTypes { get; }

        IRepository<SeekerResume> SeekerResumes { get; }
        IRepository<EducationDetail> EducationDetails { get; }
        IRepository<ExperienceDetail> ExperienceDetails { get; }
        IRepository<SkillSet> SkillSets { get; }
        void Save();
    }
}
