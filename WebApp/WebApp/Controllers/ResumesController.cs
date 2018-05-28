using BLL.DTO.SeekerResumeBuilder;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers
{
    [Authorize]
    public class ResumesController : ApiController
    {
        IService<SeekerResumeDTO> ResumeService;

        public ResumesController(IService<SeekerResumeDTO> serv)
        {
            ResumeService = serv;
        }

        // GET api/resumes
        public IHttpActionResult Get()
        {
            var res = ResumeService.GetAll();
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        // GET api/resumes/5
        public IHttpActionResult Get(int id)
        {
            var res = ResumeService.Get(id);
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        // POST api/resumes
        public void Post([FromBody]SeekerResumeDTO value)
        {
            if (ModelState.IsValid)
                return Ok(ResumeService.Create(value));
            else
                return BadRequest(ModelState);
        }

        // PUT api/resumes/5
        public IHttpActionResult Put([FromBody]SeekerResumeDTO value)
        {
            if (ModelState.IsValid)
                return Ok(ResumeService.Change(value));
            else
                return BadRequest(ModelState);
        }

        // DELETE api/resumes/5
        public IHttpActionResult Delete(int id)
        {
            bool res = ResumeService.Delete(id);
            if (res)
                return Ok();
            else
                return BadRequest(ModelState);
        }
    }
}