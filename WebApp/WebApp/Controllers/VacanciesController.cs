using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Interfaces;
using BLL.Services;
using BLL.Infrastructure;
using BLL.DTO.JobPostManagement;
using BLL.DTO.SeekerResumeBuilder;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace WebApp.Controllers
{
    [Authorize]
    public class VacanciesController : ApiController
    {
        IJobService VacancyService;

        public VacanciesController(IJobService serv)
        {
            VacancyService = serv;
        }

        // GET api/vacancies
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(VacancyService.GetAll());
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        // GET api/vacancies/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(VacancyService.Get(id));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }   

        // POST api/vacancies
        public void Post([FromBody]JobPostDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                    return Ok(VacancyService.Create(value));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        // PUT api/vacancies/5
        public IHttpActionResult Put([FromBody]JobPostDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                    return Ok(VacancyService.Change(value));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/vacancies/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                VacancyService.Delete(id);
                return Ok();
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        [Route("api/vacancies/resumes")]
        public IHttpActionResult ViewResumes(int id)
        {
            try
            {
                return Ok(VacancyService.Review(id));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
