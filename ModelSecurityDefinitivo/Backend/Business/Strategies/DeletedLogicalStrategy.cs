using Data.Core;
using Entity.Model;

namespace Business.Strategies;

public class DeletedLogicalStrategy<T> : IDeletedStrategy where T : BaseModel
{
    private readonly DataBase<T> _dataBase;

    public DeletedLogicalStrategy(DataBase<T> dataBase)
    {
        _dataBase = dataBase;
    }

    public async Task Delete (int id)
    {
        await _dataBase.DeleteLogical(id);
    }

}
