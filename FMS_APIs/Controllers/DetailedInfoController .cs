
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using FPro;
using ARWABANISHAMSAH_FMS.SERVICES;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("details")]
    public class DetailedInfoController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetAllVehicles()
        {
            var detailedGeofenceService = new ARWABANISHAMSAH_FMS.SERVICES.DetailedInfoService();

            var Gvar = await detailedGeofenceService.getAllVehiclesInformation();
            return
                Ok(Gvar.DicOfDT);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSpicificVehicleInfo(int id)
        {
            var detailedGeofenceService = new ARWABANISHAMSAH_FMS.SERVICES.DetailedInfoService();

            var Gvartwo = await detailedGeofenceService.getDetailedInfo(id);
            return Ok(Gvartwo.DicOfDT);
        }


        [HttpGet("rangetime")]
        public async Task<ActionResult> GetRangeTime([FromBody] GVAR Gvar)
        {
            var detailedGeofenceService = new ARWABANISHAMSAH_FMS.SERVICES.DetailedInfoService();

            var Gvartwo = await detailedGeofenceService.getInfoRangeTime(Gvar);
            return
                Ok(Gvartwo.DicOfDT);

        }

       


    }
}
