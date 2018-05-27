using BLL.DTO.JobPostManagement;
using BLL.DTO.SeekerResumeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService<T> where T: class 
    {
        bool Create(T value);

        IEnumerable<T> Find(IEnumerable<JobTypeDTO> types,
                            IEnumerable<DateTime> dateTimes,
                            IEnumerable<SkillSetDTO> skillSets);

        T Get(int? Id);

        IEnumerable<T> GetAll();

        bool Delete(int? Id);

        bool Change(T value);
    }
}
