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
    public class ResumeService
    {
        IUnitOfWork Database { get; set; }

        public ResumeService(IUnitOfWork uow)
        {
            this.Database = uow;
        }

    }
}
