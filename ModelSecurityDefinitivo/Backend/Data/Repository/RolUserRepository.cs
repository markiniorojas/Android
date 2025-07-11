using Data.Core;
using Data.Interface;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class RolUserRepository : DataBase<RolUser>, IRolUserRepository
{
    private readonly ApplicationDbContext _context;

    public RolUserRepository(ApplicationDbContext context)
        : base(context) 
    {
        _context = context;
    }

    public override async Task<IEnumerable<RolUser>> GetAll()
    {
        return await _context.Set<RolUser>()
                        .Where(ru => !ru.IsDelete)
                        .Include(ru => ru.Rol)
                        .Include(ru => ru.User)
                        .ToListAsync();   
    }

    public override async Task<RolUser?> GetById(int id)
    {
        return await _context.Set<RolUser>()
                        .Where(ru => !ru.IsDelete)
                        .Include(ru => ru.Rol)
                        .Include(ru => ru.User)
                        .FirstOrDefaultAsync(ru => ru.Id == id);
    }
}
