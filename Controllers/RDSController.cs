using AWSService.Interfaces.IServices;
using AWSService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AWSService.Controllers{
    [ApiController]
    [Route("api/RDS")]
    public class RDSController : ControllerBase
    {
        private readonly IRdsService _rdsService;

        public RDSController(IRdsService rdsService)
        {
            _rdsService = rdsService;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees(){
            var result = await _rdsService.GetEmployees();
            return Ok(result);
        }
    }
}
