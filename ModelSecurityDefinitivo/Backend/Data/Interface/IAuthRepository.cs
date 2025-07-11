using Entity.Model;

namespace Data.Interface;

public interface IAuthRepository
{
    Task<User?> Login(string email, string password);
    Task<RolUser> getRolUserWithId(int id);
    Task<User?> GetByEmail(string email);
}
