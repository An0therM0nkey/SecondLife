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
    public class EducationDetailRepository : IRepository<EducationDetail>
    {
        ApplicationDbContext db;

        public EducationDetailRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public void Create(EducationDetail item)
        {
            db.EducationDetails.Add(item);
        }

        public void Delete(int Id)
        {
            EducationDetail edu = db.EducationDetails.Find(Id);
            if (edu != null)
                db.EducationDetails.Remove(edu);
        }

        public IEnumerable<EducationDetail> Find(Func<EducationDetail, bool> predicate)
        {
            return db.EducationDetails.Where(predicate).ToList();
        }

        public EducationDetail Get(int Id)
        {
            return db.EducationDetails.Find(Id);
        }

        public IEnumerable<EducationDetail> GetAll()
        {
            return db.EducationDetails;
        }

        public void Update(EducationDetail item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
