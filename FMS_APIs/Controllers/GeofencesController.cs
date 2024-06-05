
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using FPro;
using ARWABANISHAMSAH_FMS.SERVICES;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("geofences")]
    public class GeofencesController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult> GetAllGeofences()
        {
            var geofenceService = new ARWABANISHAMSAH_FMS.SERVICES.GeofenceService();

            var Gvar = await geofenceService.getAllGeofences();
            return
                Ok(Gvar.DicOfDT);

        }

        [HttpGet("circle")]
        public async Task<ActionResult> GetCircleGeofences()
        {
            var geofenceService = new ARWABANISHAMSAH_FMS.SERVICES.DetailedGeofenceService();

            var Gvar = await geofenceService.getAllCircularGeofences();
            return
                Ok(Gvar.DicOfDT);

        }

        [HttpGet("polygon")]
        public async Task<ActionResult> GetPolygonGeofences()
        {
            var geofenceService = new ARWABANISHAMSAH_FMS.SERVICES.DetailedGeofenceService();

            var Gvar = await geofenceService.getAllPolygonGeofences();
            return
                Ok(Gvar.DicOfDT);

        }

        [HttpGet("rectangle")]
        public async Task<ActionResult> GetRectangleGeofences()
        {
            var geofenceService = new ARWABANISHAMSAH_FMS.SERVICES.DetailedGeofenceService();

            var Gvar = await geofenceService.getAllRectangleGeofences();
            return
                Ok(Gvar.DicOfDT);

        }


    }
}
