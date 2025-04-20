using APIAondeAssistir.Application.Interfaces;
using APIAondeAssistir.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIAondeAssistir.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CampeonatoController : ControllerBase
    {
        private readonly ICampeonatoService _campeonatoService;
        public CampeonatoController(ICampeonatoService campeonatoService)
        {
            _campeonatoService = campeonatoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var campeonatos = await _campeonatoService.GetAllAsync();
            if (campeonatos == null)
            {
                return NotFound("Ocorreu um erro ao obter os dados dos Campeonatos.");
            }
            return Ok(campeonatos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var campeonato = await _campeonatoService.GetById(id);

            if (campeonato == null)
            {
                return NotFound("Ocorreu um erro ao obter os dados do Campeonato.");
            }
            return Ok(campeonato);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Campeonato campeonato)
        {
            if (campeonato == null)
            {
                return BadRequest("Dados inválidos, revise e tente novamente.");
            }
            
            var campeonatoCreated = await _campeonatoService.CreateAsync(campeonato);
            if (!campeonatoCreated)
            {
                return StatusCode(500, "Ocorreu um erro ao criar o Campeonato.");
            }

            return CreatedAtAction(nameof(GetById), new { id = campeonato.Codigo }, campeonato);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Campeonato campeonato)
        {
            if (campeonato == null)
            {
                return BadRequest("Dados inválidos, revise e tente novamente.");
            }

            var campeonatoUpdated = await _campeonatoService.UpdateAsync(campeonato);

            if (!campeonatoUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _campeonatoService.DeleteAsync(id);
            if (!deleted)
            {
                return StatusCode(500, "Ocorreu um erro ao deletar o Campeonato.");
            }
            return NoContent();
        }
    }
}
