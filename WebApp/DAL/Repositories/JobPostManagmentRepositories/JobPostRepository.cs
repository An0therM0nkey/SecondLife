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
    public class JobPostRepository : IRepository<JobPost>
    {
        ApplicationDbContext db;

        public JobPostRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public void Create(JobPost item)
        {
            db.JobPosts.Add(item);
        }

        public void Delete(int Id)
        {
            JobPost jobPost = db.JobPosts.Find(Id);
            if (jobPost != null)
                db.JobPosts.Remove(jobPost);
        }

        public IEnumerable<JobPost> Find(Func<JobPost, bool> predicate)
        {
            return db.JobPosts.Where(predicate).ToList();
        }

        public JobPost Get(int Id)
        {
            return db.JobPosts.Find(Id);
        }

        public IEnumerable<JobPost> GetAll()
        {
            return db.JobPosts;
        }

        public void Update(JobPost item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
