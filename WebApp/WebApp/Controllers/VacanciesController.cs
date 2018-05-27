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
        IService<JobPostDTO> VacancyService;

        public VacanciesController(IService<JobPostDTO> serv)
        {
            VacancyService = serv;
        }

        // GET api/vacancies
        public IHttpActionResult Get()
        {
            var res = VacancyService.GetAll();
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        // GET api/vacancies/5
        public IHttpActionResult Get(int id)
        {
            var res = VacancyService.Get(id);
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }   

        // POST api/vacancies
        public void Post([FromBody]JobPostDTO value)
        {
            if (ModelState.IsValid)
                return Ok(VacancyService.Create(value));
            else
                return BadRequest(ModelState);
        }

        // PUT api/vacancies/5
        public IHttpActionResult Put([FromBody]JobPostDTO value)
        {
            //try
            //{
            //    if(ModelState.IsValid)
            //        return Ok(VacancyService.Change(value));
            //}
            //catch (ValidationException ex)
            //{
            //    //Not sure about this exception
            //    return Content(HttpStatusCode.BadRequest, ex);
            //}
            if(ModelState.IsValid)
                return Ok(VacancyService.Change(value));
            else
                return BadRequest(ModelState);
        }

        // DELETE api/vacancies/5
        public IHttpActionResult Delete(int id)
        {
            bool res = VacancyService.Delete(id);
            if(res)
                return Ok();
            else
                return BadRequest(ModelState);
        }
    }
}
