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

    public class PersonServices : ServiceBase<PersonDTO, Person>, IPersonServices
    {
        private readonly PersonRepository _persona;
        private readonly ILogger<PersonServices> _logger;
        private readonly IMapper _mapper;
        private readonly DeleteStrategyFactory<Person> _deleteFactory;

        public PersonServices(DataBase<Person> data, PersonRepository persona,ILogger<PersonServices> logger,IMapper mapper,  DeleteStrategyFactory<Person> delete)
            :base(data, logger,mapper)
        {
            _persona = persona;
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
