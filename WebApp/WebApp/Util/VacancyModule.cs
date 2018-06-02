using BLL.DTO.JobPostManagement;
using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Util
{
    public class VacancyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IJobService>().To<JobService>();
        }
    }
}