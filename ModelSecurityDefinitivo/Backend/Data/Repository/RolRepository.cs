using Data.Core;
using Data.Interface;
using Entity.Context;
using Entity.Model;

namespace Data.Repository;

public class RolRepository : DataBase<Rol>, IRolRepository
{
    public RolRepository(ApplicationDbContext context)
    : base(context) {}
}
