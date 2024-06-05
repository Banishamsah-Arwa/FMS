
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using FPro;
using ARWABANISHAMSAH_FMS.SERVICES;

namespace InventoryManagementSystem.Controllers
{
    [ApiController]
    [Route("routeHistory")]
    public class RouteHistoryController : ControllerBase
    {


        [HttpPost]
        public async Task<ActionResult> AddRouteHistory([FromBody] GVAR Gvar)
        {
            var routeHistoryService = new ARWABANISHAMSAH_FMS.SERVICES.RouteHisory();

            await routeHistoryService.AddRouteHistory(Gvar);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRouteHistoryByVehicleId(int id)
        {
            var routeHistoryService = new ARWABANISHAMSAH_FMS.SERVICES.RouteHisory();

            var GvarResponse = await routeHistoryService.GetRouteHistoryByVehicleId(id);
            return
                Ok(GvarResponse.DicOfDT);

        }

    }
}
