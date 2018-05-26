using DAL.Entities.JobPostManagement;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.JobPostManagmentRepositories
{
    public class JobTypeRepository : IRepository<JobType>
    {
        ApplicationDbContext db;

        public JobTypeRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public void Create(JobType item)
        {
            db.JobTypes.Add(item);
        }

        public void Delete(int Id)
        {
            JobType jobType = db.JobTypes.Find(Id);
            if (jobType != null)
                db.JobTypes.Remove(jobType);
        }

        public IEnumerable<JobType> Find(Func<JobType, bool> predicate)
        {
            return db.JobTypes.Where(predicate).ToList();
        }

        public JobType Get(int Id)
        {
            return db.JobTypes.Find(Id);
        }

        public IEnumerable<JobType> GetAll()
        {
            return db.JobTypes;
        }

        public void Update(JobType item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
