using System;
using AdventOfCode.Business.Managers;
using Microsoft.AspNetCore.Mvc;

namespace AdventOfCode.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdventController : ControllerBase
    {
        private readonly IAdventOfCodeManager _adventOfCodeManager;

        public AdventController(
            IAdventOfCodeManager adventOfCodeManager)
        {
            _adventOfCodeManager = adventOfCodeManager ?? throw new ArgumentNullException(nameof(adventOfCodeManager));
        }

        [HttpPost("trebuchet")]
        public ActionResult<int> CalculateCalibrationAsync()
        {
            var result = _adventOfCodeManager.HandleCalibrationCount();

            return Ok(result.Calibration);
        }
    }
}
