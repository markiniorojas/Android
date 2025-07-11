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

public class RolServices : ServiceBase<RolDTO, Rol>, IRolServices
{
    private readonly RolRepository _rol;
    private readonly ILogger<RolServices> _logger;
    private readonly IMapper _mapper;
    private readonly DeleteStrategyFactory<Rol> _delete;

    public RolServices(DataBase<Rol> data, RolRepository rol,ILogger<RolServices> logger,IMapper mapper, DeleteStrategyFactory<Rol> delete)
        :base(data, logger,mapper)
    {
        _rol = rol;
        _logger = logger;
        _mapper = mapper;
        _delete = delete;
    }

    public async Task Delete(int id, DeleteType tipo)
    {
        var strategy = _delete.Create(tipo);
        await strategy.Delete(id);
    }
}
