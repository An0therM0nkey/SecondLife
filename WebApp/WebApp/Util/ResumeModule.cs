﻿using BLL.DTO.SeekerResumeBuilder;
using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Util
{
    public class ResumeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IService<SeekerResumeDTO>>().To<ResumeService>();
        }
    }
}