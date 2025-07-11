using Data.Core;
using Data.Interface;
using Entity.Context;
using Entity.Model;

namespace Data.Repository;

public class ModuleRepository : DataBase<Module>, IModuleRepository
{

    public ModuleRepository(ApplicationDbContext context)
    : base(context) {}

}
