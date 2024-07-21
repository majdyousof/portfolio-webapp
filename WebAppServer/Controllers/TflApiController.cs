using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppServer.Services;

namespace WebAppServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TflApiController : ControllerBase
    {
        private readonly ITflService _tflService;
        private readonly ILogger<TflApiController> _logger;

        public TflApiController(ITflService tflService , ILogger<TflApiController> logger)
        {
            _tflService = tflService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves accident data.
        /// </summary>
        /// <returns>Accident data as a string.</returns>
        [HttpGet("GetAccidentData")]
        public async Task<IActionResult> GetAccidentData()
        {
            try
            {
                string data = await _tflService.GetAccidentData();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves bike point data.
        /// </summary>
        /// <returns>Bike point data as a string.</returns>
        [HttpGet("GetBikePoint")]
        public async Task<IActionResult> GetBikePoint()
        {
            try
            {
                string data = await _tflService.GetBikePoint();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
