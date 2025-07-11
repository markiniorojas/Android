using System;
using Entity.Model;

namespace Business.Core;

public interface IServiceBase<D, T> where T : BaseModel
{
    Task<IEnumerable<D>> GetAll();
    Task<D?> getById(int id);
    Task<D> Create(D dto);
    Task<bool> Update(D dto);
    Task<bool> Reactivate(int id);
}
