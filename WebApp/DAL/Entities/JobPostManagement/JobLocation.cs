using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.JobPostManagement
{
    public class JobLocation
    {
        public int Id { get; set; }
        public string StreetAddres { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
    }
}
