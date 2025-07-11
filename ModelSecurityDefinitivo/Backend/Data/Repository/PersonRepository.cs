using Data.Core;
using Entity.Model;
using Entity.Context;
using Data.Interface;

namespace Data.Repository;

public class PersonRepository : DataBase<Person>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext context)
    : base(context) {}

}
