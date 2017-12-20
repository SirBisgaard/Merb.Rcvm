using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Merb.Rcvm.FrontEnd.Controllers
{
    [Produces("application/json")]
    [Route("api/RecyclingCenter")]
    public class RecyclingCenterController : Controller
    {
        // GET: api/RecyclingCenter
        [HttpGet]
        public IEnumerable<RecyclingCenter> Get()
        {
            return new RecyclingCenter[]
            {
                new RecyclingCenter { Id ="1", Name = "Center A" },
                new RecyclingCenter { Id ="2", Name = "Center B" },
                new RecyclingCenter { Id ="3", Name = "Center C" },
                new RecyclingCenter { Id ="4", Name = "Center D" },
                new RecyclingCenter { Id ="5", Name = "Center E" },
            };
        }

        // GET: api/RecyclingCenter/5
        [HttpGet("{id}", Name = "Get")]
        public RecyclingCenter Get(int id)
        {
            return new RecyclingCenter { Name = "Center A" };
        }

        // POST: api/RecyclingCenter
        [HttpPost]
        public void Post([FromBody]RecyclingCenter value)
        {
        }

        // PUT: api/RecyclingCenter/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]RecyclingCenter value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class RecyclingCenter
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
