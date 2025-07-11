using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Entity.DTOs.Read;
using Utilities;
using Entity.Enums;
using Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;

[Route("api/[controller]")]
//[Authorize]
[ApiController]
[Produces("application/json")]
public class ModuleController : ControllerBase
{
    private readonly ModuleServices _moduleBusiness;
    private readonly ILogger<ModuleController> _logger;

    public ModuleController(ModuleServices moduleBusiness, ILogger<ModuleController> logger)
    {
        _moduleBusiness = moduleBusiness;
        _logger = logger;
    }


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PersonDTO>), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var modules = await _moduleBusiness.GetAll();
            return Ok(modules);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los Module");
            return StatusCode(500, new { message = ex.Message });
        }
    }


    [HttpGet("{id:int}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var Module = await _moduleBusiness.getById(id);
            return Ok(Module);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validación fallida para el Module con ID:" + id);
            return BadRequest(new { Mesagge = ex.Message });
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation(ex, "Persona no encontrado con ID: {ModuleId}", id);
            return NotFound(new { message = ex.Message });
        }
        catch (ExternalServiceException ex)
        {
            _logger.LogError(ex, "Error al obtener Module con ID: {ModuleId}", id);
            return StatusCode(500, new { message = ex.Message });
        }
    }


    [HttpPost]
    [ProducesResponseType(typeof(ModuleDTO), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]

    public async Task<IActionResult> CreatePerson([FromBody] ModuleDTO ModuleDTO)
    {
        try
        {
            ModuleDTO.Name = ValitadionHelpers.NormalizeName(ModuleDTO.Name);

            var createModule = await _moduleBusiness.Create(ModuleDTO);
            return CreatedAtAction(nameof(GetById), new
            {
                id = createModule.Id
            }, createModule);

        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validación fallida");
            return BadRequest(new { mesagge = ex.Message });
        }
        catch (ExternalServiceException ex)
        {
            _logger.LogError(ex, "Error al crear la persona");
            return StatusCode(500, new { mesagge = ex.Message });
        }
    }


    [HttpPut]
    [ProducesResponseType(typeof(ModuleDTO), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdatePerson([FromBody] ModuleDTO ModuleDTO)
    {
        try
        {
            if (ModuleDTO == null || ModuleDTO.Id <= 0)
            {
                return BadRequest(new { message = "El ID de la Module  debe ser mayor que cero y no nulo" });
            }

            ModuleDTO.Name = ValitadionHelpers.NormalizeName(ModuleDTO.Name);

            var updatePerson = await _moduleBusiness.Update(ModuleDTO);
            return Ok(updatePerson);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validación fallida al actualizar el Module");
            return BadRequest(new { message = ex.Message });
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation(ex, "Module no encontrado con ID: {RolId}", ModuleDTO.Id);
            return NotFound(new { message = ex.Message });
        }
        catch (ExternalServiceException ex)
        {
            _logger.LogError(ex, "Error al actualizar el persona con ID co: {RolId}", ModuleDTO.Id);
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] DeleteType tipo)
    {
        if (id <= 0)
            return BadRequest("El ID proporcionado no es válido.");

        var entity = await _moduleBusiness.getById(id);
        if (entity == null)
            return NotFound($"No se encontró un módulo con ID {id}.");

        await _moduleBusiness.Delete(id, tipo);
        return Ok($"Módulo {id} eliminado ({tipo}).");
    }
}
