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
    public class ExperienceDetailRepository : IRepository<ExperienceDetail>
    {
        ApplicationDbContext db;

        public ExperienceDetailRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public void Create(ExperienceDetail item)
        {
            db.ExperienceDetails.Add(item);
        }

        public void Delete(int Id)
        {
            ExperienceDetail exp = db.ExperienceDetails.Find(Id);
            if (exp != null)
                db.ExperienceDetails.Remove(exp);
        }

        public IEnumerable<ExperienceDetail> Find(Func<ExperienceDetail, bool> predicate)
        {
            return db.ExperienceDetails.Where(predicate).ToList();
        }

        public ExperienceDetail Get(int Id)
        {
            return db.ExperienceDetails.Find(Id);
        }

        public IEnumerable<ExperienceDetail> GetAll()
        {
            return db.ExperienceDetails;
        }

        public void Update(ExperienceDetail item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
