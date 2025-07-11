using Entity.DTOs.Read;
using Entity.DTOs.Write;

namespace Business.Interface;

public interface IAuthServices
{
    Task<LoginDTO> Login(LoginDTO loginDTO);
    Task<RolUserDTO> getRolUserWithId(int id);
}
