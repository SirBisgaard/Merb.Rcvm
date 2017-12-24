using System.Collections.Generic;
using System.Threading.Tasks;
using Merb.Rcvm.FrontEnd.Domain;
using Merb.Rcvm.FrontEnd.Domain.DataTypes;
using Microsoft.AspNetCore.Mvc;

namespace Merb.Rcvm.FrontEnd.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/Vehicle")]
    public class VehicleController : Controller
    {
        private readonly HttpServiceClient<Vehicle> _serviceClient;

        public VehicleController()
        {
            _serviceClient = new HttpServiceClient<Vehicle>("http://localhost:5040/api/Vehicle/");
        }

        // GET: api/RecyclingCenter
        [HttpGet]
        [HttpGet("RecyclingCenter/{id}")]
        public async Task<IEnumerable<Vehicle>> GetAll(string id)
        {
            return await _serviceClient.GetCollection($"RecyclingCenter/{id}");
        }

        // GET: api/RecyclingCenter/5
        [HttpGet("{id}")]
        public async Task<Vehicle> Get(string id)
        {
            return await _serviceClient.Get(id);
        }

        // POST: api/RecyclingCenter
        [HttpPost]
        public async Task Post([FromBody]Vehicle value)
        {
            await _serviceClient.Create(value);
        }

        // PUT: api/RecyclingCenter/5
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody]Vehicle value)
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
