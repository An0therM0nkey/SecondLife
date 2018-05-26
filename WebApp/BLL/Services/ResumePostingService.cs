using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Models;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ResumePostingService
    {
        IUnitOfWork Database { get; set; }
    }
}
