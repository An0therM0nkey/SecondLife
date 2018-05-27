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
        // GET api/resumes
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/resumes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/resumes
        public void Post([FromBody]string value)
        {
        }

        // PUT api/resumes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/resumes/5
        public void Delete(int id)
        {
        }
    }
}