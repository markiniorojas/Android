using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Business.Strategies;
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
public class RolController : ControllerBase
{
    private readonly RolServices _rolBusiness;
    private readonly ILogger<RolController> _logger;


    public RolController(RolServices rolServices, ILogger<RolController> logger)
    {
        _rolBusiness = rolServices;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RolDTO>), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = await _rolBusiness.GetAll();
            return Ok(list);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los Rol");
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
            var objectId = await _rolBusiness.getById(id);
            return Ok(objectId);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validación fallida para el Rol con ID:" + id);
            return BadRequest(new { Mesagge = ex.Message });
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation(ex, "Rol no encontrado con ID: {RolId}", id);
            return NotFound(new { message = ex.Message });
        }
        catch (ExternalServiceException ex)
        {
            _logger.LogError(ex, "Error al obtener Rol con ID: {RolId}", id);
            return StatusCode(500, new { message = ex.Message });
        }
    }


    [HttpPost]
    [ProducesResponseType(typeof(FormDTO), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]

    public async Task<IActionResult> Create([FromBody] RolDTO dto)
    {
        try
        {
            dto.Name = ValitadionHelpers.NormalizeName(dto.Name);

            var create = await _rolBusiness.Create(dto);
            return CreatedAtAction(nameof(GetById), new
            {
                id = create.Id
            }, create);

        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validación fallida");
            return BadRequest(new { mesagge = ex.Message });
        }
        catch (ExternalServiceException ex)
        {
            _logger.LogError(ex, "Error al crear la Rol");
            return StatusCode(500, new { mesagge = ex.Message });
        }
    }


    [HttpPut]
    [ProducesResponseType(typeof(RolDTO), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update([FromBody] RolDTO dto)
    {
        try
        {
            if (dto == null || dto.Id <= 0)
            {
                return BadRequest(new { message = "El ID de la Form  debe ser mayor que cero y no nulo" });
            }

            dto.Name = ValitadionHelpers.NormalizeName(dto.Name);

            var update = await _rolBusiness.Update(dto);
            return Ok(update);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validación fallida al actualizar el Rol");
            return BadRequest(new { message = ex.Message });
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation(ex, "Rol no encontrado con ID: {RolId}", dto.Id);
            return NotFound(new { message = ex.Message });
        }
        catch (ExternalServiceException ex)
        {
            _logger.LogError(ex, "Error al actualizar el Rol con ID co: {FormId}", dto.Id);
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] DeleteType tipo)
    {
        if (id <= 0)
            return BadRequest("El ID proporcionado no es válido.");

        var entity = await _rolBusiness.getById(id);
        if (entity == null)
            return NotFound($"No se encontró un módulo con ID {id}.");

        await _rolBusiness.Delete(id, tipo);
        return Ok($"Módulo {id} eliminado ({tipo}).");
    }
    
}
