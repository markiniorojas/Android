using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Entity.DTOs.Write;
using Utilities.Helpers;
using Business.JWT;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Google.Apis.Auth;
using Utilities.Notification.Email;
using Entity.Model;
using System;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly AuthServices _authServices;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;
    private readonly JWTGenerate _jwt;
    private readonly AuthRepository _repository;
    private readonly NotificationEmail _email;

    public AuthController(
        AuthServices authServices,
    ILogger<AuthController> logger,
    JWTGenerate jWT,
    IConfiguration configuration,
    AuthRepository repository,
    NotificationEmail email
    )
    {
        _authServices = authServices;
        _logger = logger;
        _jwt = jWT;
        _configuration = configuration;
        _repository = repository;
        _email = email;
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        try
        {
            if (!ValitadionHelpers.IsValidEmail(dto.Email))
            {
                return BadRequest(new { message = "El correo electrónico proporcionado no es válido." });
            }

            if (!PasswordHelpers.IsValidPassword(dto.Password))
            {
                return BadRequest(new { message = "La contraseña debe tener al menos 8 caracteres, un número y un carácter especial (_ . -)." });
            }

            var result = await _authServices.Login(dto);

            Console.WriteLine("credenciales recibidas: " + result);

            if (result == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            var token = _jwt.GenerateJWT(result);

            return StatusCode(StatusCodes.Status200OK, new
            {
                isSuccess = true,
                token = token
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al procesar el login.");
            return StatusCode(500, new { message = "Error interno del servidor" });
        }
    }

    [HttpPost("google")]
    public async Task<IActionResult> LoginGoogle([FromBody] GoogleDTO tokenDto)
    {
        try
        {
            Console.WriteLine($"ClientId desde config: {_configuration["Google:ClientId"]}");

            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenDto.Token, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _configuration["Google:ClientId"] }
            });



            var user = await _repository.GetByEmail(payload.Email);

            if (user == null)
            {
                return Ok(new
                {
                    isSuccess = false,
                    message = "El usuario no existe"
                });
            }

            var dto = new LoginDTO
            {
                Email = user.Email,
                Password = user.Password
            };

            var token = await _jwt.GenerateJWT(dto);

            return Ok(new { isSuccess = true, token });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error en login con google");
            return BadRequest(new
            {
                isSuccess = false,
                message = ex.Message
            });
        }
    }

    [HttpPost("google/register")]
    public async Task<IActionResult> RegisterWithGoogle([FromBody] RegisterDTO dto)
    {
        try
        {
            if (!ValitadionHelpers.IsValidEmail(dto.Email))
            {
                return BadRequest(new { message = "El correo electrónico proporcionado no es válido." });
            }

            if (!ValitadionHelpers.IsValidPhoneNumber(dto.Phone))
            {
                return BadRequest(new { message = "El número de teléfono proporcionado no es válido." });
            }

            if (!ValitadionHelpers.IsValidDocumentNumber(dto.DocumentNumber))
            {
                return BadRequest(new { message = "El número de documento proporcionado no es válido." });
            }

            dto.Name = ValitadionHelpers.NormalizeName(dto.Name);
            dto.LastName = ValitadionHelpers.NormalizeName(dto.LastName);

            var payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _configuration["Google:ClientId"] }
            });

            var userExist = await _repository.GetByEmail(payload.Email);


            if (userExist != null)
            {
                return Conflict(new { isSuccess = false, message = "El usuario ya existe." });
            }

            var randomPassword = PasswordHelpers.GenerateRandomPassword();

            var AddUserPerson = new RegisterDTO
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = randomPassword,
                CreatedDate = DateTime.UtcNow,
                Active = true,
                Name = dto.Name,
                LastName = dto.LastName,
                TypeDocument = dto.TypeDocument,
                DocumentNumber = dto.DocumentNumber,
                Phone = dto.Phone,
                Address = dto.Address,
            };

            await _authServices.AddUserPerson(AddUserPerson);
            await _email.WelcomenUser(AddUserPerson.Email, randomPassword);

            var token = await _jwt.GenerateJWT(new LoginDTO
            {
                Email = AddUserPerson.Email,
                Password = AddUserPerson.Password
            });

            return Ok(new { isSuccess = true, token });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en registro con Google");
            return StatusCode(500, new { isSuccess = false, message = ex.Message });
        }
    }

}
