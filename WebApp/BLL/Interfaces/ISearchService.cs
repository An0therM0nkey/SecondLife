using BLL.DTO.JobPostManagement;
using BLL.DTO.SeekerResumeBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISearchService
    {
        IEnumerable<JobPostDTO> FindVacancies(IEnumerable<JobTypeDTO> types,
                                                    IEnumerable<DateTime> dateTimes,
                                                    IEnumerable<SkillSetDTO> skillSets);

        IEnumerable<SeekerResumeDTO> FindResumes(IEnumerable<SkillSetDTO> skillSets,
                                                    IEnumerable<DateTime> dates);
    }
}
