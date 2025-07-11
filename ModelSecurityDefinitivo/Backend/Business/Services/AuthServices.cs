using AutoMapper;
using Business.Interface;
using Data.Repository;
using Entity.DTOs.Read;
using Entity.DTOs.Write;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class AuthServices : IAuthServices
{
    private readonly AuthRepository _AuthRepository;
    private readonly UserRepository _userRepository;
    private readonly ILogger<AuthServices> _logger;
    private readonly IMapper _mapper;

    public AuthServices(AuthRepository AuthRepository, ILogger<AuthServices> logger, IMapper mapper, UserRepository userRepository)
    {
        _AuthRepository = AuthRepository;
        _logger = logger;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    async public Task<LoginDTO> Login(LoginDTO loginDTO)
    {
        try
        {
            var exists = await _AuthRepository.Login(loginDTO.Email, loginDTO.Password);

            var user = _mapper.Map<LoginDTO>(exists);
            Console.WriteLine("Mapeo: " + user);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al validar las credenciales");
            throw;
        }
    }

    public async Task<RolUserDTO> getRolUserWithId(int id)
    {
        try
        {
            var result = await _AuthRepository.getRolUserWithId(id);
            var rolUser = _mapper.Map<RolUserDTO>(result);
            return rolUser;
        }
        catch (Exception ex)
        {
            // Loguear o manejar el error adecuadamente
            throw new Exception("Error al obtener el rol del usuario", ex);
        }
    }

    public async Task<bool> AddUserPerson(RegisterDTO registerDTO)
    {
        try
        {
            var person = new Person
            {
                Name = registerDTO.Name,
                LastName = registerDTO.LastName,
                TypeDocument = registerDTO.TypeDocument,
                DocumentNumber = registerDTO.DocumentNumber,
                Phone = registerDTO.Phone,
                Address = registerDTO.Address
            };

            

            var resultPerson = await _AuthRepository.AddPerson(person);

            var user = new User
            {
                PersonId = resultPerson.Id,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                CreatedDate = DateTime.UtcNow,
                Active = registerDTO.Active
            };

            var resultUser = await _AuthRepository.AddUser(user);

            if (resultUser)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Error al registrarte", ex);
        }
    }

}
