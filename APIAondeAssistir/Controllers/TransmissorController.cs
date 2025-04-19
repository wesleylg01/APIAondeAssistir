using APIAondeAssistir.Application.Interfaces;
using APIAondeAssistir.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIAondeAssistir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransmissorController : ControllerBase
    {
        private ITransmissorService _transmissorService;
        public TransmissorController(ITransmissorService transmissorService)
        {
            _transmissorService = transmissorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transmissor = await _transmissorService.GetAllAsync();
            if (transmissor == null)
            {
                return NotFound("Ocorreu um erro ao obter os dados dos Transmissores.");
            }
            return Ok(transmissor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transmissor = await _transmissorService.GetById(id);
            if (transmissor == null)
            {
                return NotFound("Ocorreu um erro ao obter os dados do Transmissor.");
            }
            return Ok(transmissor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transmissor transmissor)
        {
            if (transmissor == null)
            {
                return BadRequest("Dados inválidos, revise e tente novamente.");
            }

            var transmissorCreated = await _transmissorService.CreateAsync(transmissor);
            if (!transmissorCreated)
            {
                return StatusCode(500, "Ocorreu um erro ao criar o Transmissor.");
            }

            return CreatedAtAction(nameof(GetById), new { id = transmissor.Codigo }, transmissor);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Transmissor transmissor)
        {
            if (transmissor == null)
            {
                return BadRequest("Dados inválidos, revise e tente novamente.");
            }
            var transmissorUpdated = await _transmissorService.UpdateAsync(transmissor);
            if (!transmissorUpdated)
            {
                return StatusCode(500, "Ocorreu um erro ao atualizar o Transmissor.");
            }

            return Ok(transmissor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transmissorDeleted = await _transmissorService.DeleteAsync(id);
            if (!transmissorDeleted)
            {
                return StatusCode(500, "Ocorreu um erro ao deletar o Transmissor.");
            }
            return NoContent();
        }
    }
}
