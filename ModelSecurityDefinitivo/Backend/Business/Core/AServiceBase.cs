using System;
using Entity.Model;

namespace Business.Core;

public abstract class AServiceBase<D , T> : IServiceBase<D, T> where T: BaseModel
{
    public abstract Task<IEnumerable<D>> GetAll();
    public abstract Task<D?> getById(int id);
    public abstract Task<D> Create(D dto);
    public abstract Task<bool> Update(D dto);
    public abstract Task<bool> Reactivate(int id);
}
