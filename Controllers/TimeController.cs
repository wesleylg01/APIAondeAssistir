using APIAondeAssistir.Application.Interfaces;
using APIAondeAssistir.Domain.Entities;
using APIAondeAssistir.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIAondeAssistir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly ITimeService _timeService;

        public TimeController(ITimeService timeService)
        {
            _timeService = timeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var times = await _timeService.GetAllAsync();

            if(times == null)
            {
                return NotFound("Ocorreu um erro ao obter os dados dos Times.");
            }

            return Ok(times);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var time = await _timeService.GetById(id);

            if (time == null)
            {
                return NotFound("Ocorreu um erro ao obter os dados do Time.");
            }

            return Ok(time);
        }

        [HttpPost]
        public async Task<IActionResult> Create ([FromBody] Time time)
        {
            if (time == null)
            {
                return BadRequest("Dados inválidos, revise e tente novamente.");
            }
            var timeCreated = await _timeService.CreateAsync(time);

            if (!timeCreated)
            {
                return StatusCode(500, "Ocorreu um erro ao criar o Time.");
            }
            return CreatedAtAction(nameof(GetById), new { id = time.Codigo }, time);
        }

        [HttpPut]
        public async Task<IActionResult> Update ([FromBody] Time time)
        {
            if (time == null)
            {
                return BadRequest("Dados inválidos, revise os dados e tente novamente.");
            }
            var timeUpdated = await _timeService.UpdateAsync(time);

            if (!timeUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            if (id < 1)
            {
                return BadRequest("O Id informado é inválido");
            }

            var timeDeleted = await _timeService.DeleteAsync(id);

            if (!timeDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
