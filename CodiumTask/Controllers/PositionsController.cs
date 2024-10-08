using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Services;
using Shared.DTOs;
using Shared.Entities;

namespace CodiumTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly PositionService _positionService;
        public PositionsController(PositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Position>>> GetAllPositions()
        {
            List<Position> positions = await _positionService.GetPositionsAsync();
            if (positions.Count == 0)
            {
                return NotFound("There are currently no positions");
            }
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetOnePosition(int id)
        {
            Position? position = await _positionService.GetPositionAsync(id);
            if (position == null)
            {
                return NotFound("Position not found!");
            }
            return Ok(position);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPositions([FromBody] PositionUploadDTO uploadPosition)
        {
            if (uploadPosition == null || !uploadPosition.Positions.Any())
            {
                return BadRequest("No positions found in the uploaded file.");
            }
            await _positionService.UploadPositionsAsync(uploadPosition.Positions);
            return Ok("Positions uploaded successfully.");
        }
    }
}
