
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using FPro;
using ARWABANISHAMSAH_FMS.SERVICES;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("vehicleInformation")]
    public class VehiclesInformationController : ControllerBase
    {

       

        [HttpPost]
        public async Task<ActionResult> AddVehicleInformation([FromBody] GVAR Gvar)
        {
            var vehicleInformationService = new ARWABANISHAMSAH_FMS.SERVICES.VehicleInformationServices();

            await vehicleInformationService.AddVehicleInformation(Gvar);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteVehicleInformation([FromBody] GVAR Gvar)
        {
            var vehicleInformationService = new ARWABANISHAMSAH_FMS.SERVICES.VehicleInformationServices();
            await vehicleInformationService.DeleteVehicleInformation(Gvar);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateVehicleInformation([FromBody] GVAR Gvar)
        {
            var vehicleInformationService = new ARWABANISHAMSAH_FMS.SERVICES.VehicleInformationServices();
            await vehicleInformationService.UpdateVehicleInformation(Gvar);
            return Ok();
        }

        [HttpPut("updateDriverID")]
        public async Task<ActionResult> UpdateDriverID([FromBody] GVAR Gvar)
        {
            var vehicleInformationService = new ARWABANISHAMSAH_FMS.SERVICES.VehicleInformationServices();
            await vehicleInformationService.UpdateDriverID(Gvar);
            return Ok();
        }
    }
}
