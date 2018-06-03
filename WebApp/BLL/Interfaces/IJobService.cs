using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.JobPostManagement;
using BLL.DTO.SeekerResumeBuilder;

namespace BLL.Interfaces
{
    public interface IJobService
    {
        void Create(JobPostDTO value);

        IEnumerable<JobPostDTO> Find(IEnumerable<JobTypeDTO> types,
                            IEnumerable<DateTime> dateTimes,
                            IEnumerable<SkillSetDTO> skillSets);

        IEnumerable<JobPostDTO> Find(string key);

        JobPostDTO Get(int? Id);

        IEnumerable<JobPostDTO> GetAll();

        IEnumerable<JobPostDTO> GetAll(string id);

        void Delete(int? Id);

        void Change(JobPostDTO value);

        void NotifySeeker(int senderId, int recieverId);

        IEnumerable<SeekerResumeDTO> ReviewResumes(int id);
    }
}
