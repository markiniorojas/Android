using Data.Core;
using Data.Interface;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class RolFormPermissionRepository : DataBase<RolFormPermission>, IRolFormPermissionRepository
{
    private readonly ApplicationDbContext _context;
    
    public RolFormPermissionRepository(ApplicationDbContext context)
        : base(context) 
    {
        _context = context;
    }

     public override async Task<IEnumerable<RolFormPermission>> GetAll()
    {
        return await _context.Set<RolFormPermission>()
                        .Where(rfp => !rfp.IsDelete)
                        .Include(rfp => rfp.Rol)
                        .Include(rfp => rfp.Form)
                        .Include(rfp => rfp.Permission)
                        .ToListAsync();   
    }

    public override async Task<RolFormPermission?> GetById(int id)
    {
        return await _context.Set<RolFormPermission>()
                        .Where(rfp => !rfp.IsDelete)
                        .Include(rfp => rfp.Rol)
                        .Include(rfp => rfp.Form)
                        .Include(rfp => rfp.Permission)
                        .FirstOrDefaultAsync(rfp => rfp.Id == id);
    }
}
