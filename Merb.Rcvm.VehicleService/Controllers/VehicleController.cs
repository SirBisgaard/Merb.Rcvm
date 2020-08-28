using System.Collections.Generic;
using System.Threading.Tasks;
using Merb.Rcvm.VehicleService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Merb.Rcvm.VehicleService.Controllers
{
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly VehicleRepository _repository;

        public VehicleController()
        {
            _repository = new VehicleRepository();
        }

        // GET api/values
        [HttpGet]
        [HttpGet("RecyclingCenter/{id}")]
        public async Task<IEnumerable<Vehicle>> GetAll(string id)
        {
            return await _repository.GetAllVehicles(id);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Vehicle> Get(string id)
        {
            return await _repository.GetVehicle(id);
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]Vehicle recyclingCenter)
        {
            await _repository.CreateVehicle(recyclingCenter);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Vehicle  recyclingCenter)
        {
            await _repository.UpdateVehicle(recyclingCenter);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _repository.DeleteVehicle(id);
        }
    }
}
