using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Util
{
    public class NinjectSetting : NinjectModule
    {
        public override void Load()
        {
            Bind<IResumeService>().To<ResumeService>();
            Bind<IJobService>().To<JobService>();
        }
    }
}