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
    public class SkillSetRepository : IRepository<SkillSet>
    {
        ApplicationDbContext db;

        public SkillSetRepository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public void Create(SkillSet item)
        {
            db.SkillSets.Add(item);
        }

        public void Delete(int Id)
        {
            SkillSet skill = db.SkillSets.Find(Id);
            if (skill != null)
                db.SkillSets.Remove(skill);
        }

        public IEnumerable<SkillSet> Find(Func<SkillSet, bool> predicate)
        {
            return db.SkillSets.Where(predicate).ToList();
        }

        public SkillSet Get(int Id)
        {
            return db.SkillSets.Find(Id);
        }

        public IEnumerable<SkillSet> GetAll()
        {
            return db.SkillSets;
        }

        public void Update(SkillSet item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
