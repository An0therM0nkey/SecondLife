using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class JobService
    {
        IUnitOfWork Database { get; set; }

        public JobService(IUnitOfWork uow)
        {
            this.Database = uow;
        }
    }
}
