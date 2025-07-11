using System.Runtime.Intrinsics.X86;
using Data.Core;
using Data.Interface;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class FormModuleRepository : DataBase<FormModule>, IFormModuleRepository
{
    private readonly ApplicationDbContext _context;

    public FormModuleRepository(ApplicationDbContext context)
        : base(context) 
    {
        _context = context;
    }

    public override async Task<IEnumerable<FormModule>> GetAll()
    {
        return await _context.Set<FormModule>()
                        .Where(fm => !fm.IsDelete)
                        .Include(fm => fm.Form)
                        .Include(fm => fm.Module)
                        .ToListAsync();   
    }

    public override async Task<FormModule?> GetById(int id)
    {
        return await _context.Set<FormModule>()
                        .Where(fm => !fm.IsDelete)
                        .Include(fm => fm.Form)
                        .Include(fm => fm.Module)
                        .FirstOrDefaultAsync(fm => fm.Id == id);
    }
}
