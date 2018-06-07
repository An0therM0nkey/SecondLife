using DAL.Entities.JobPostManagement;
using DAL.Entities.SeekerResumeBilder;
using DAL.Interfaces;
using DAL.Repositories.JobPostManagmentRepositories;
using DAL.Repositories.SeekerResumeBulderRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext db;

        private SeekerResumeRepository SeekerResumeRepository;
        private EducationDetailRepository EducationDetailRepository;
        private ExperienceDetailRepository ExperienceDetailRepository;
        private SkillSetRepository SkillSetRepository;

        private JobPostRepository JobPostRepository;
        private JobLocationRepository JobLocationRepository;
        private JobTypeRepository JobTypeRepository;

        public EFUnitOfWork() { }

        public EFUnitOfWork(string connectionString)
        {
            db = new ApplicationDbContext(connectionString);
        }

        public IRepository<JobPost> JobPosts
        {
            get
            {
                if (JobPostRepository == null)
                    JobPostRepository = new JobPostRepository(db);
                return JobPostRepository;
            }
        }

        public IRepository<JobLocation> JobLocations
        {
            get
            {
                if (JobLocationRepository == null)
                    JobLocationRepository = new JobLocationRepository(db);
                return JobLocationRepository;
            }
        }


        public IRepository<JobType> JobTypes
        {
            get
            {
                if (JobTypeRepository == null)
                    JobTypeRepository = new JobTypeRepository(db);
                return JobTypeRepository;
            }
        }

        public IRepository<SeekerResume> SeekerResumes
        {
            get
            {
                if (SeekerResumeRepository == null)
                    SeekerResumeRepository = new SeekerResumeRepository(db);
                return SeekerResumeRepository;
            }
        }

        public IRepository<EducationDetail> EducationDetails
        {
            get
            {
                if (EducationDetailRepository == null)
                    EducationDetailRepository = new EducationDetailRepository(db);
                return EducationDetailRepository;
            }
        }

        public IRepository<ExperienceDetail> ExperienceDetails
        {
            get
            {
                if (ExperienceDetailRepository == null)
                    ExperienceDetailRepository = new ExperienceDetailRepository(db);
                return ExperienceDetailRepository;
            }
        }

        public IRepository<SkillSet> SkillSets
        {
            get
            {
                if (SkillSetRepository == null)
                    SkillSetRepository = new SkillSetRepository(db);
                return SkillSetRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
