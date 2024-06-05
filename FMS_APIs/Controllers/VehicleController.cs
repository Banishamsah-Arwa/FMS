
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using FPro;
using ARWABANISHAMSAH_FMS.SERVICES;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("vehicles")]
    public class VehiclesController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetAllVehicles()
        {
            var vehicleService = new ARWABANISHAMSAH_FMS.SERVICES.VehicleService();
            
            var Gvar = await vehicleService.getAllVehicles();
            return
                Ok(Gvar.DicOfDT);

        }

        [HttpPost]
        public async Task<ActionResult> AddVehicle([FromBody] GVAR Gvar)
        {
            var vehicleService = new ARWABANISHAMSAH_FMS.SERVICES.VehicleService();

             await vehicleService.AddVehicle(Gvar);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteVehicle([FromBody] GVAR Gvar)
        {
            var vehicleService = new ARWABANISHAMSAH_FMS.SERVICES.VehicleService();
            await vehicleService.DeleteVehicle(Gvar);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateVehicle([FromBody] GVAR Gvar)
        {
            var vehicleService = new ARWABANISHAMSAH_FMS.SERVICES.VehicleService();
            await vehicleService.UpdateVehicle( Gvar);
            return Ok();
        }
    }
}
