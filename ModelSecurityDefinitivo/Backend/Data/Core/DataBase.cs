using Microsoft.EntityFrameworkCore;
using Entity.Context;
using System.Linq.Expressions;
using Entity.Model;

namespace Data.Core;

public class DataBase<T> : ADataBase<T> where T: BaseModel
{
    private readonly ApplicationDbContext _context;
    public DataBase(ApplicationDbContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Obtiene todos los registros de la entidad T desde la base de datos.
    /// </summary>
    /// <returns>Una lista de todos los registros encontrados.</returns>
    public override async Task<IEnumerable<T>> GetAll()
    {
        var query = _context.Set<T>().AsQueryable();

        // Verifica si la entidad tiene una propiedad IsDeleted de tipo bool
        var prop = typeof(T).GetProperty("IsDelete");
        if (prop != null && prop.PropertyType == typeof(bool))
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.Property(parameter, "IsDelete");
            var condition = Expression.Equal(propertyAccess, Expression.Constant(false));
            var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);

            query = query.Where(lambda);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Busca un registro por su identificador único.
    /// </summary>
    /// <param name="id">El identificador del registro a buscar.</param>
    /// <returns>El registro encontrado o null si no existe.</returns>
    public override async Task<T?> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    /// Agrega una nueva entidad a la base de datos.
    /// </summary>
    /// <param name="entity">La entidad que se desea agregar.</param>
    /// <returns>La entidad agregada con sus valores actualizados, si aplica.</returns>
    public override async Task<T> Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Actualiza los datos de una entidad existente.
    /// </summary>
    /// <param name="entity">La entidad con los datos actualizados.</param>
    /// <returns>True si la operación fue exitosa.</returns>
    public override async Task<bool> Update(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return true; 
    }

    /// Elimina físicamente un registro de la base de datos según su identificador.
    /// </summary>
    /// <param name="id">El identificador de la entidad a eliminar.</param>
    /// <returns>True si se eliminó correctamente; Fal se encontró.</returns>
    public override async Task<bool> Delete(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null) return false;
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Realiza una eliminación lógica de la entidad, marcando la propiedad IsDeleted como true.
    /// </summary>
    /// <param name="id">El identificador de la entidad a eliminar lógicamente.</param>
    /// <returns>True si se actualizó correctamente; False si no se encontró o no tiene la propiedad IsDeleted.</returns>
    public override async Task<bool> DeleteLogical(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null) return false;

        var prop = entity.GetType().GetProperty("IsDeleted");
        if (prop != null)
        {
            prop.SetValue(entity, true);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Realizamos un dese-eliminador logico de la entidad, marcando la propiedad IsDeleted como false
    /// </summary>
    /// <param name="id">El identificador de la entidad a dese-eliminar lógicamente.</param>
    /// <returns>True si se actualizó correctamente; False si no se encontró o no tiene la propiedad IsDeleted.</returns>
    public override async Task<bool> Reactivate(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null) return false;

        var prop = entity.GetType().GetProperty("IsDeleted");
        if (prop != null)
        {
            prop.SetValue(entity, false);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

}
