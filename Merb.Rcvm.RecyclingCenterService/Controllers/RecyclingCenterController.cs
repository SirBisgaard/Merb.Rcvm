using System.Collections.Generic;
using System.Threading.Tasks;
using Merb.Rcvm.RecyclingCenterService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Merb.Rcvm.RecyclingCenterService.Controllers
{
    [Route("api/[controller]")]
    public class RecyclingCenterController : Controller
    {
        private RecyclingCenterRepository _repository;

        public RecyclingCenterController()
        {
            _repository = new RecyclingCenterRepository();
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<RecyclingCenter>> Get()
        {
            return await _repository.GetAllRecyclingCenters();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<RecyclingCenter> Get(string id)
        {
            return await _repository.GetRecyclingCenterAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]RecyclingCenter recyclingCenter)
        {
            await _repository.CreateRecyclingCenter(recyclingCenter);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]RecyclingCenter recyclingCenter)
        {
            await _repository.UpdateRecyclingCenter(recyclingCenter);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _repository.DeleteRecyclingCenter(id);
        }
    }
}
