using System.Collections.Generic;
using System.Threading.Tasks;
using Merb.Rcvm.FrontEnd.Domain;
using Merb.Rcvm.FrontEnd.Domain.DataTypes;
using Microsoft.AspNetCore.Mvc;

namespace Merb.Rcvm.FrontEnd.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/RecyclingCenter")]
    public class RecyclingCenterController : Controller
    {
        private HttpServiceClient<RecyclingCenter> _serviceClient;

        public RecyclingCenterController()
        {
            _serviceClient = new HttpServiceClient<RecyclingCenter>("http://localhost:5050/api/RecyclingCenter/");
        }

        // GET: api/RecyclingCenter
        [HttpGet]
        public async Task<IEnumerable<RecyclingCenter>> Get()
        {
            return await _serviceClient.GetCollection();
        }

        // GET: api/RecyclingCenter/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<RecyclingCenter> Get(string id)
        {
            return await _serviceClient.Get(id);
        }

        // POST: api/RecyclingCenter
        [HttpPost]
        public async Task Post([FromBody]RecyclingCenter value)
        {
            await _serviceClient.Create(value);
        }

        // PUT: api/RecyclingCenter/5
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody]RecyclingCenter value)
        {
            await _serviceClient.Update(value, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _serviceClient.Delete(id);
        }
    }

}
