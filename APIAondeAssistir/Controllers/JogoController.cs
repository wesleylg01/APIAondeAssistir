using APIAondeAssistir.Application.Interfaces;
using APIAondeAssistir.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIAondeAssistir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly IJogoService _jogoService;
        public JogoController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var campeonatos = await _jogoService.GetAll();
            if (campeonatos == null)
            {
                return NotFound("Ocorreu um erro ao obter os dados dos Jogos.");
            }

            return Ok(campeonatos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var jogo = await _jogoService.GetById(id);

            if (jogo == null)
            {
                return NotFound("Ocorreu um erro ao obter os dados do Jogo.");
            }
            return Ok(jogo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Jogo jogo)
        {
            if (jogo == null)
            {
                return BadRequest("Dados inválidos, revise e tente novamente.");
            }

            var jogoCreated = await _jogoService.CreateAsync(jogo);
            
            if (!jogoCreated)
            {
                return StatusCode(500, "Ocorreu um erro ao criar o Jogo.");
            }

            return CreatedAtAction(nameof(GetById), new { id = jogo.Codigo }, jogo);
        }

        [HttpPut]
        public async Task<IActionResult> Update (Jogo jogo)
        {
            if (jogo == null)
            {
                return BadRequest("Dados inválidos, revise e tente novamente.");
            }

            var jogoUpdated = await _jogoService.UpdateAsync(jogo);
            if (!jogoUpdated)
            {
                return StatusCode(500, "Ocorreu um erro ao atualizar o Jogo.");
            }

            return Ok(jogoUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var jogo = await _jogoService.DeleteAsync(id);

            if (!jogo)
            {
                return NotFound("Ocorreu um erro ao deletar os dados do Jogo.");
            }
            return NoContent();
        }

        [HttpGet("brasileirao/rodada/{rodada}")]
        public async Task<IActionResult> GetByRodada(int rodada)
        {
            var jogo = await _jogoService.GetByRodada(rodada);

            if (jogo == null)
            {
                return NotFound("Ocorreu um erro ao obter os dados do rodada.");
            }
            return Ok(jogo);
        }

        [HttpGet("time/{time}")]
        public async Task<IActionResult> GetJogosListByTime(int time)
        {
            var jogo = await _jogoService.GetJogosListByTime(time);

            if (jogo == null)
            {
                return NotFound("Ocorreu um erro ao obter os jogos deste time.");
            }
            return Ok(jogo);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetJogoDetails(int id)
        {
            var jogo = await _jogoService.GetJogoDetails(id);

            if (jogo == null)
            {
                return NotFound("Ocorreu um erro ao obter os dados do Jogo.");
            }
            return Ok(jogo);
        }

        [HttpDelete("anteriores")]
        public async Task<IActionResult> DeleteAnteriores()
        {
            var deleted = await _jogoService.DeleteAnterioresAsync();
            if (!deleted)
                return StatusCode(500, "Erro ao deletar jogos anteriores");

            return NoContent();
        }
    }
}
