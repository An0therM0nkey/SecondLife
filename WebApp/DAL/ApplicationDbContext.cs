using DAL.Entities;
using DAL.Entities.JobPostManagement;
using DAL.Entities.SeekerResumeBilder;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("AppContext", throwIfV1Schema: false)
        {
        }

        public ApplicationDbContext(string connectionString)
            : base(connectionString, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //Seeker Profile Builder tables
        public virtual IDbSet<SeekerResume> SeekerResumes { get; set; }
        public virtual IDbSet<EducationDetail> EducationDetails { get; set; }
        public virtual IDbSet<ExperienceDetail> ExperienceDetails { get; set; }
        public virtual IDbSet<SkillSet> SkillSets { get; set; }

        //Job Post Management tables
        public virtual IDbSet<JobPost> JobPosts { get; set; }
        public virtual IDbSet<JobLocation> JobLocations { get; set; }
        public virtual IDbSet<JobType> JobTypes { get; set; }
    }
}
