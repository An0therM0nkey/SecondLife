using DAL.Entities.SeekerResumeBilder;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.SeekerResumeBulderRepositories
{
    public class SeekerResumeRepository : IRepository<SeekerResume>
    {
        ApplicationDbContext db;

        public SeekerResumeRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public void Create(SeekerResume item)
        {
            db.SeekerResumes.Add(item);
        }

        public void Delete(int Id)
        {
            SeekerResume resume = db.SeekerResumes.Find(Id);
            if (resume != null)
                db.SeekerResumes.Remove(resume);
        }

        public IEnumerable<SeekerResume> Find(Func<SeekerResume, bool> predicate)
        {
            return db.SeekerResumes.Where(predicate).ToList();
        }

        public SeekerResume Get(int Id)
        {
            return db.SeekerResumes.Find(Id);
        }

        public IEnumerable<SeekerResume> GetAll()
        {
            return db.SeekerResumes;    
        }

        public void Update(SeekerResume item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
