using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.JobPostManagement;
using BLL.DTO.SeekerResumeBuilder;

namespace BLL.Interfaces
{
    public interface IResumeService
    {
        void Create(SeekerResumeDTO value);

        IEnumerable<SeekerResumeDTO> Find(IEnumerable<JobTypeDTO> types,
                            IEnumerable<DateTime> dateTimes,
                            IEnumerable<SkillSetDTO> skillSets);

        IEnumerable<SeekerResumeDTO> Find(string key);

        SeekerResumeDTO Get(int? Id);

        IEnumerable<SeekerResumeDTO> GetAll();

        void Delete(int? Id);

        void Change(SeekerResumeDTO value);

        void SendResume(int senderId, int recieverId);

        IEnumerable<JobPostDTO> ReviewVacancies(int id);
    }
}