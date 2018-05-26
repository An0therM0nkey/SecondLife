using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.SeekerResumeBuilder
{
    public class EducationDetailDTO
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string CertificateDegreeName { get; set; }
        public string Major { get; set; }
        public string InstituteUniversityName { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
