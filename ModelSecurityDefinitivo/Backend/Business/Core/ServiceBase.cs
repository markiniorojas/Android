using System.Buffers.Text;
using AutoMapper;
using Data.Core;
using Entity.Enums;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Core;

public class ServiceBase<D, T> : AServiceBase<D, T> where T : BaseModel where D : class
{
    private readonly DataBase<T> _data;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public ServiceBase(DataBase<T> data, ILogger logger, IMapper mapper)
    {
        _data = data;
        _logger = logger;
        _mapper = mapper;
    }

    public override async Task<IEnumerable<D>> GetAll()
    {
        try
        {
            var entities = await _data.GetAll();
            return _mapper.Map<IEnumerable<D>>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los registros de {Entity}", typeof(T).Name);
            throw;
        }
    }
    public override async Task<D?> getById(int id)
    {
        try
        {
            var entities = await _data.GetById(id);
            return _mapper.Map<D>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el registro con ID {Id} de {Entity}", id, typeof(T).Name);
            throw;
        }
    }

    public override async Task<D> Create(D dto)
    {
        try
        {
            var entity = _mapper.Map<T>(dto);
            var entities = await _data.Add(entity);
            return _mapper.Map<D>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear entidad {Entity}", typeof(T).Name);
            throw;
        }
    }
    
    public override async Task<bool> Update(D dto)
    {
        try
        {
            var entity = _mapper.Map<T>(dto);
            var entities = await _data.Update(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar entidad {Entity}", typeof(T).Name);
            throw;
        }
    }


    /// <summary>
    /// Aqui esta des-eliminando la entidad T, cambiando la propiedad IsDeleted de true a false
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna en caso de que todo este bien true, y si falla lanza una exception</returns>
    public override async Task<bool> Reactivate(int id)
    {
        try
        {
            return await _data.Reactivate(id); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al Des-Elimnar el registro con ID {Id} de {Entity}", id, typeof(T).Name);
            throw;
        }
    }
}
