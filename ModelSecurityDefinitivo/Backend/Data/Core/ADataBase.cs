using System;
using Entity.Model;

namespace Data.Core;

public abstract class ADataBase<T> : IDataBase<T> where T: BaseModel
{
    public abstract Task<IEnumerable<T>> GetAll();
    public abstract Task<T?> GetById(int id);
    public abstract Task<T> Add(T entity);
    public abstract Task<bool> Update(T entity);
    public abstract Task<bool> DeleteLogical(int id);
    public abstract Task<bool> Delete(int id);
    public abstract Task<bool> Reactivate(int id);
}
