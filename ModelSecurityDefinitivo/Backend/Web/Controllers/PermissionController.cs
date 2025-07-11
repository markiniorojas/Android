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
public class PermissionController : ControllerBase
{
    private readonly PermissionServices _permissionBusiness;
    private readonly ILogger<PermissionController> _logger;


    public PermissionController(PermissionServices permissionServices, ILogger<PermissionController> logger)
    {
        _permissionBusiness = permissionServices;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PermissionDTO>), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var list = await _permissionBusiness.GetAll();
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
            var objectId = await _permissionBusiness.getById(id);
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

    public async Task<IActionResult> Create([FromBody] PermissionDTO dto)
    {
        try
        {
            dto.Name = ValitadionHelpers.NormalizeName(dto.Name);

            var create = await _permissionBusiness.Create(dto);
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
    public async Task<IActionResult> Update([FromBody] PermissionDTO dto)
    {
        try
        {
            if (dto == null || dto.Id <= 0)
            {
                return BadRequest(new { message = "El ID de la Form  debe ser mayor que cero y no nulo" });
            }

            dto.Name = ValitadionHelpers.NormalizeName(dto.Name);

            var update = await _permissionBusiness.Update(dto);
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

        var entity = await _permissionBusiness.getById(id);
        if (entity == null)
            return NotFound($"No se encontró un módulo con ID {id}.");

        await _permissionBusiness.Delete(id, tipo);
        return Ok($"Módulo {id} eliminado ({tipo}).");
    }
}
