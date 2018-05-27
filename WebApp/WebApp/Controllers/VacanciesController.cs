using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers
{
    [Authorize]
    public class VacanciesController : ApiController
    {
        // GET api/vacncies
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/vacancies/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/vacancies
        public void Post([FromBody]string value)
        {
        }

        // PUT api/vacancies/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/vacancies/5
        public void Delete(int id)
        {
        }
    }
}
