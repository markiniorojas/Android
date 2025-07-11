using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Entity.DTOs.Read;
using Utilities;
using Utilities.Helpers;
using Entity.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;

[Route("api/[controller]")]
//[Authorize]
[ApiController]
[Produces("application/json")]
public class PersonController : ControllerBase
{
    private readonly PersonServices _personBusiness;
    private readonly ILogger<PersonController> _logger;

    public PersonController(PersonServices personBusiness, ILogger<PersonController> logger)
    {
        _personBusiness = personBusiness;
        _logger = logger;
    }


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PersonDTO>), 200)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var Persons = await _personBusiness.GetAll();
            return Ok(Persons);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener los Person");
            return StatusCode(500, new { message = ex.Message });
        }
    }


    [HttpGet("{id:int}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetPersonById(int id)
    {
        try
        {
            var person = await _personBusiness.getById(id);
            return Ok(person);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validación fallida para el person con ID:" + id);
            return BadRequest(new { Mesagge = ex.Message });
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation(ex, "Persona no encontrado con ID: {personId}", id);
            return NotFound(new { message = ex.Message });
        }
        catch (ExternalServiceException ex)
        {
            _logger.LogError(ex, "Error al obtener person con ID: {personId}", id);
            return StatusCode(500, new { message = ex.Message });
        }
    }


    [HttpPost]
    [ProducesResponseType(typeof(PersonDTO), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]

    public async Task<IActionResult> CreatePerson([FromBody] PersonDTO personDTO)
    {
        try
        {

            if (!ValitadionHelpers.IsValidPhoneNumber(personDTO.Phone))
            {
                return BadRequest(new { message = "El número de teléfono proporcionado no es válido." });
            }

            if (!ValitadionHelpers.IsValidDocumentNumber(personDTO.DocumentNumber))
            {
                return BadRequest(new { message = "El número de documento proporcionado no es válido." });
            }

            personDTO.Name = ValitadionHelpers.NormalizeName(personDTO.Name);
            personDTO.LastName = ValitadionHelpers.NormalizeName(personDTO.LastName);

            var createPerson = await _personBusiness.Create(personDTO);
            return CreatedAtAction(nameof(GetPersonById), new { id = createPerson.Id }, createPerson);

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
    [ProducesResponseType(typeof(PersonDTO), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdatePerson([FromBody] PersonDTO personDTO)
    {
        try
        {
            if (personDTO == null || personDTO.Id <= 0)
            {
                return BadRequest(new { message = "El ID de la persona  debe ser mayor que cero y no nulo" });
            }

            var updatePerson = await _personBusiness.Update(personDTO);
            return Ok(updatePerson);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Validación fallida al actualizar el persona");
            return BadRequest(new { message = ex.Message });
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation(ex, "persona no encontrado con ID: {RolId}", personDTO.Id);
            return NotFound(new { message = ex.Message });
        }
        catch (ExternalServiceException ex)
        {
            _logger.LogError(ex, "Error al actualizar el persona con ID co: {RolId}", personDTO.Id);
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] DeleteType tipo)
    {
        if (id <= 0)
            return BadRequest("El ID proporcionado no es válido.");

        var entity = await _personBusiness.getById(id);
        if (entity == null)
            return NotFound($"No se encontró un módulo con ID {id}.");

        await _personBusiness.Delete(id, tipo);
        return Ok($"Módulo {id} eliminado ({tipo}).");
    }

}
