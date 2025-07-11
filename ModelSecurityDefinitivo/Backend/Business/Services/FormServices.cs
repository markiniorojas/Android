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

public class FormServices : ServiceBase<FormDTO, Form>, IFormServices
{
    private readonly FormRepository _form;
    private readonly ILogger<FormServices> _logger;
    private readonly IMapper _mapper;
    private readonly DeleteStrategyFactory<Form> _deleteFactory;

    public FormServices(DataBase<Form> data, FormRepository form,ILogger<FormServices> logger,IMapper mapper,  DeleteStrategyFactory<Form> delete)
        :base(data, logger,mapper)
    {
        _form = form;
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
