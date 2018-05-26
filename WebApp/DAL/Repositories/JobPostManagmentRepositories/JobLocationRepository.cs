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
    public class JobLocationRepository : IRepository<JobLocation>
    {
        ApplicationDbContext db;

        public JobLocationRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public void Create(JobLocation item)
        {
            db.JobLocations.Add(item);
        }

        public void Delete(int Id)
        {
            JobLocation jobLocation = db.JobLocations.Find(Id);
            if (jobLocation != null)
                db.JobLocations.Remove(jobLocation);
        }

        public IEnumerable<JobLocation> Find(Func<JobLocation, bool> predicate)
        {
            return db.JobLocations.Where(predicate).ToList();
        }

        public JobLocation Get(int Id)
        {
            return db.JobLocations.Find(Id);
        }

        public IEnumerable<JobLocation> GetAll()
        {
            return db.JobLocations;
        }

        public void Update(JobLocation item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
