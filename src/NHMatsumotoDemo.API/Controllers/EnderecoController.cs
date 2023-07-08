using NHMatsumotoDemo.Services;
using Microsoft.AspNetCore.Mvc;
using NHMatsumotoDemo.Domain.Dtos;
using AutoMapper;
using NHMatsumotoDemo.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace NHMatsumotoDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly ILogger<EnderecoController> _logger;
        private readonly IEnderecoServices _enderecoServices;
        private readonly IMapper _mapper;

        public EnderecoController(ILogger<EnderecoController> logger, IEnderecoServices service, IMapper mapper)
        {
            _logger = logger;
            _enderecoServices = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(EnderecoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _enderecoServices.GetById(id);

            if(result is not null)
            {
                return Ok(result);
            }

            _logger.LogInformation("Tentativa de consulta de endereço inexistente");

            return NotFound();
        }


        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(EnderecoDTO dto)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            var endereco = _mapper.Map<Endereco>(dto);

            var result = await _enderecoServices.Create(endereco);

            if(result is 0)
            {
                _logger.LogError("Erro ao tentar cadastrar novo endereco");
                return BadRequest("Houve um erro ao processar sua solicitação");
            }

            return Created("/", result);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Put(int id, EnderecoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();

                return BadRequest(errors);
            }

            try
            {
                var request = await _enderecoServices.GetById(id);

                if (request is null)
                {
                    return BadRequest("Não foi possíel concluír a operação");
                }

                var endereco = _mapper.Map<Endereco>(dto);

                await _enderecoServices.Put(id, endereco);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o projeto");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
