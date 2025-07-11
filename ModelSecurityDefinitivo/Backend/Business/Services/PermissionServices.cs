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

public class PermissionServices : ServiceBase<PermissionDTO, Permission>, IPermissionServices
{
    private readonly PermissionRepository _permission;
    private readonly ILogger<PermissionServices> _logger;
    private readonly IMapper _mapper;
    private readonly DeleteStrategyFactory<Permission> _deleteFactory;

    public PermissionServices(DataBase<Permission> data, PermissionRepository permission,ILogger<PermissionServices> logger,IMapper mapper, DeleteStrategyFactory<Permission> delete)
        :base(data, logger,mapper)
    {
        _permission = permission;
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
