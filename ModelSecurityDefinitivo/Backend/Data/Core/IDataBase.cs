using System;
using Entity.Model;

namespace Data.Core;

public interface IDataBase<T> where T : BaseModel
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> DeleteLogical(int id);
    Task<bool> Delete(int id);
    Task<bool> Reactivate(int id);
}
