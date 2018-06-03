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
    public class ResumesController : ApiController
    {
        IResumeService ResumeService;

        public ResumesController(IResumeService serv)
        {
            ResumeService = serv;
        }

        // GET api/resumes
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        [Authorize(Roles = "seeker")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(ResumeService.GetAll());
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        // GET api/resumes/5
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        [Authorize(Roles = "seeker")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(ResumeService.Get(id));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        // POST api/resumes
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "seeker")]
        public IHttpActionResult Post([FromBody]SeekerResumeDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResumeService.Change(value);
                    return Ok();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        // PUT api/resumes/5
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "seeker")]
        public IHttpActionResult Put([FromBody]SeekerResumeDTO value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ResumeService.Create(value);
                    return Ok();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/resumes/5
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "seeker")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                ResumeService.Delete(id);
                return Ok();
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        [Route("api/resumes/vacancies")]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "seeker")]
        public IHttpActionResult ViewResumes(int id)
        {
            try
            {
                return Ok(ResumeService.ReviewVacancies(id));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        [Route("api/resumes/send")]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "seeker")]
        public IHttpActionResult Send(int senderId, int recieverId)
        {
            try
            {
                ResumeService.SendResume(senderId, recieverId);
                return Ok();
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        [Route("api/resumes/search")]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        [Authorize(Roles = "seeker")]
        public IHttpActionResult Search([FromBody]string key)
        {
            try
            {
                return Ok(ResumeService.Find(key));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }

        [Route("api/resumes/filter")]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "recruiter")]
        [Authorize(Roles = "seeker")]
        public IHttpActionResult Filter([FromBody]SearchRequest searchRequest)
        {
            try
            {
                return Ok(ResumeService.Find(searchRequest.types, searchRequest.dateTimes, searchRequest.skillSets));
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return BadRequest(ModelState);
        }
    }
}