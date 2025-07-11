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

public class FormModuleServices : ServiceBase<FormModuleDTO, FormModule>, IFormModuleServices
{
    private readonly FormModuleRepository _formModule;
    private readonly ILogger<FormModuleServices> _logger;
    private readonly IMapper _mapper;
    private readonly DeleteStrategyFactory<FormModule> _deleteFactory;

    public FormModuleServices(DataBase<FormModule> data, FormModuleRepository formModule,ILogger<FormModuleServices> logger,IMapper mapper, DeleteStrategyFactory<FormModule> delete)
        :base(data, logger,mapper)
    {
        _formModule = formModule;
        _logger = logger;
        _mapper = mapper;
        _deleteFactory = delete;
    }

    public override async Task<IEnumerable<FormModuleDTO>> GetAll()
    {
        try
        {
            var entities = await _formModule.GetAll();
            return _mapper.Map<IEnumerable<FormModuleDTO>>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los registros de {Entity}", typeof(FormModule).Name);
            throw;
        }
    }

    public override async Task<FormModuleDTO?> getById(int id)
    {
        try
        {
            var entities = await _formModule.GetById(id);
            return _mapper.Map<FormModuleDTO>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el registro con ID {Id} de {Entity}", id, typeof(FormModule).Name);
            throw;
        }
    }

    public async Task<FormModuleWriteDTO> Create(FormModuleWriteDTO dto)
    {
        try
        {
            var entity = _mapper.Map<FormModule>(dto);
            var entities = await _formModule.Add(entity);
            return _mapper.Map<FormModuleWriteDTO>(entities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear entidad {Entity}", typeof(FormModule).Name);
            throw;
        }
    }
    
    public async Task<bool> Update(FormModuleWriteDTO dto)
    {
        try
        {
            var entity = _mapper.Map<FormModule>(dto);
            var entities = await _formModule.Update(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar entidad {Entity}", typeof(FormModule).Name);
            throw;
        }
    }

    public async Task Delete(int id, DeleteType tipo)
    {
        var strategy = _deleteFactory.Create(tipo);
        await strategy.Delete(id);
    }

}
