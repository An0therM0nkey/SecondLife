using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.JobPostManagement
{
    public class JobLocationDTO
    {
        public int Id { get; set; }
        public string StreetAddres { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
    }
}
