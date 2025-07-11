using AutoMapper;
using Business.Core;
using Business.Interface;
using Business.Strategies;
using Data.Core;
using Data.Repository;
using Entity.DTOs.Read;
using Entity.DTOs.Write;
using Entity.Enums;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class RolUserServices : ServiceBase<RolUserDTO, RolUser>, IRolUserServices
{
    private readonly RolUserRepository _rolUser;
    private readonly ILogger<RolUserServices> _logger;
    private readonly IMapper _mapper;
    private readonly DeleteStrategyFactory<RolUser> _deleteFactory;

    public RolUserServices(DataBase<RolUser> data, RolUserRepository rolUser,ILogger<RolUserServices> logger,IMapper mapper, DeleteStrategyFactory<RolUser> delete)
        :base(data, logger,mapper)
    {
        _rolUser = rolUser;
        _logger = logger;
        _mapper = mapper;
        _deleteFactory = delete;
    }

    public override async Task<IEnumerable<RolUserDTO>> GetAll()
    {
        try
        {
            var entities = await _rolUser.GetAll();
            return _mapper.Map<IEnumerable<RolUserDTO>>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los registros de {Entity}", typeof(RolUser).Name);
            throw;
        }
    }

    public override async Task<RolUserDTO?> getById(int id)
    {
        try
        {
            var entities = await _rolUser.GetById(id);
            return _mapper.Map<RolUserDTO>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el registro con ID {Id} de {Entity}", id, typeof(RolUser).Name);
            throw;
        }
    }

    public virtual async Task<RolUserWriteDTO> Create(RolUserWriteDTO dto)
    {
        try
        {
            var entity = _mapper.Map<RolUser>(dto);
            var entities = await _rolUser.Add(entity);
            return _mapper.Map<RolUserWriteDTO>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear entidad {Entity}", typeof(RolUser).Name);
            throw;
        }
    }
    
    public virtual async Task<bool> Update(RolUserWriteDTO dto)
    {
        try
        {
            var entity = _mapper.Map<RolUser>(dto);
            var entities = await _rolUser.Update(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar entidad {Entity}", typeof(RolUser).Name);
            throw;
        }
    }

    public async Task Delete(int id, DeleteType tipo)
    {
        var strategy = _deleteFactory.Create(tipo);
        await strategy.Delete(id);
    }

}
