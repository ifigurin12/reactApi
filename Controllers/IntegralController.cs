using IntegralAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IntegralAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntegralController : Controller
    {
        [HttpGet("calculateIntegral")]
        public async Task<ActionResult<double>> CalculateIntegral(int SplitNumbers, double UpLim, double LowLim)
        {
            try
            {
                SimsonMethod simsonMethod = new SimsonMethod(SplitNumbers, UpLim, LowLim);
                simsonMethod.Calculate();

                if (double.IsInfinity(simsonMethod.res) || double.IsNaN(simsonMethod.res))
                {
                    return BadRequest("Result of integration is not a valid number.");
                }

                return Ok(simsonMethod.res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
