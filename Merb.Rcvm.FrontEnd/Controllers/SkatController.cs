using System.Linq;
using System.Threading.Tasks;
using Merb.Rcvm.FrontEnd.Domain;
using Merb.Rcvm.FrontEnd.Domain.DataTypes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Merb.Rcvm.FrontEnd.Controllers
{
    [Route("api/[controller]")]
    public class SkatController : Controller
    {
        private readonly HttpServiceClient<Vehicle> _serviceClient;

        public SkatController()
        {
            _serviceClient = new HttpServiceClient<Vehicle>("http://localhost:5030/api/Skat");
        }

        // GET api/values
        [HttpGet]
        public async Task<Vehicle> Get(string registrationNumber, string vin)
        {
            return await _serviceClient.Get($"?registrationNumber={registrationNumber}&vin={vin}");
        }
    }
}
