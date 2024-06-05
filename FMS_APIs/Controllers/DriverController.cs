
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using FPro;
using ARWABANISHAMSAH_FMS.SERVICES;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("drivers")]
    public class DriverController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetAllDrivers()
        {
            var driverService = new ARWABANISHAMSAH_FMS.SERVICES.DriverService();

            var Gvar = await driverService.getDrivers();
            return
                Ok(Gvar.DicOfDT);

        }

        [HttpPost]
        public async Task<ActionResult> AddDriver([FromBody] GVAR Gvar)
        {
            var driverService = new ARWABANISHAMSAH_FMS.SERVICES.DriverService();

            await driverService.addDriver(Gvar);
            return Ok();
        }



        [HttpDelete]
        public async Task<ActionResult> DeleteDriver([FromBody] GVAR Gvar)
        {
            var vehicleService = new ARWABANISHAMSAH_FMS.SERVICES.DriverService();
            await vehicleService.deleteDriver(Gvar);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDriver([FromBody] GVAR Gvar)
        {
            var vehicleService = new ARWABANISHAMSAH_FMS.SERVICES.DriverService();
            await vehicleService.UpdateDriver(Gvar);
            return Ok();
        }
    }
}
