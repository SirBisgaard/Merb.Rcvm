using System.Threading.Tasks;
using Merb.Rcvm.SkatService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Merb.Rcvm.SkatService.Controllers
{
    [Route("api/[controller]")]
    public class SkatController : Controller
    {
        private readonly SkatDmrVehicleRepository _repository;

        public SkatController()
        {
            _repository = new SkatDmrVehicleRepository();
        }

        // GET api/values
        [HttpGet]
        public async Task<Vehicle> Get(string registrationNumber, string vin)
        {
            if (!string.IsNullOrEmpty(registrationNumber))
                return await _repository.GetVehicleFromRegistrationNumber(registrationNumber);

            if (!string.IsNullOrEmpty(vin))
                return await _repository.GetVehicleFromVin(vin);

            return new Vehicle();
        }
    }
}
