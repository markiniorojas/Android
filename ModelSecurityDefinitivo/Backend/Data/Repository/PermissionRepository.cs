using Data.Core;
using Entity.Model;
using Entity.Context;
using Data.Interface;

namespace Data.Repository;

public class PermissionRepository : DataBase<Permission>, IPermissionRepository
{
    public PermissionRepository(ApplicationDbContext context)
    : base(context) {}
}
