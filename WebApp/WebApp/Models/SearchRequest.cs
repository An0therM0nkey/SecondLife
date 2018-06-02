using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.DTO.JobPostManagement;
using BLL.DTO.SeekerResumeBuilder;

namespace WebApp.Models
{
    public class SearchRequest
    {
        public IEnumerable<JobTypeDTO> types;
        public IEnumerable<DateTime> dateTimes;
        public IEnumerable<SkillSetDTO> skillSets;
    }
}