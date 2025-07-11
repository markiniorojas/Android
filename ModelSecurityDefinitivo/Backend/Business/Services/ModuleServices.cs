using AutoMapper;
using Business.Core;
using Business.Interface;
using Business.Strategies;
using Data.Core;
using Data.Repository;
using Entity.DTOs.Read;
using Entity.Enums;
using Entity.Model;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class ModuleServices : ServiceBase<ModuleDTO, Module>, IModuleServices
{
    private readonly ModuleRepository _module;
    private readonly ILogger<ModuleServices> _logger;
    private readonly IMapper _mapper;
    private readonly DeleteStrategyFactory<Module> _deleteFactory;

    public ModuleServices(DataBase<Module> data, ModuleRepository module,ILogger<ModuleServices> logger,IMapper mapper, DeleteStrategyFactory<Module> delete)
        :base(data, logger,mapper)
    {
        _module = module;
        _logger = logger;
        _mapper = mapper;
        _deleteFactory = delete;
    }

    public async Task Delete(int id, DeleteType tipo)
    {
        var strategy = _deleteFactory.Create(tipo);
        await strategy.Delete(id);
    }

}

