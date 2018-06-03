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
using WebApp.Models;

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
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        [Authorize(Roles = "seeker")]
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
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        [Authorize(Roles = "seeker")]
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
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        public IHttpActionResult Post([FromBody]JobPostDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    VacancyService.Change(value);
                    return Ok();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        // PUT api/vacancies/5
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        public IHttpActionResult Put([FromBody]JobPostDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    VacancyService.Create(value);
                    return Ok();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/vacancies/5
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
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
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        public IHttpActionResult ViewResumes(int id)
        {
            try
            {
                return Ok(VacancyService.ReviewResumes(id));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        [Route("api/vacancies/send")]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        public IHttpActionResult Send(int senderId, int recieverId)
        {
            try
            {
                VacancyService.NotifySeeker(senderId, recieverId);
                return Ok();
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        [Route("api/vacancies/search")]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        public IHttpActionResult Search([FromBody]string key)
        {
            try
            {
                return Ok(VacancyService.Find(key));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        [Route("api/vacancies/filter")]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        public IHttpActionResult Filter([FromBody]SearchRequest searchRequest)
        {
            try
            {
                return Ok(VacancyService.Find(searchRequest.types, searchRequest.dateTimes, searchRequest.skillSets));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }
    }
}
