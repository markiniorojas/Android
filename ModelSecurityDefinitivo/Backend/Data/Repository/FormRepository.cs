using Data.Core;
using Data.Interface;
using Entity.Context;
using Entity.Model;

namespace Data.Repository;

public class FormRepository : DataBase<Form>, IFormRepository
{
    public FormRepository(ApplicationDbContext context)
    : base(context) {}

}
