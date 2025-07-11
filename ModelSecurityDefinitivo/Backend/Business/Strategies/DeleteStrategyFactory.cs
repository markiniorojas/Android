using Entity.Enums;
using Entity.Model;

namespace Business.Strategies;

public class DeleteStrategyFactory<T> where T : BaseModel
{
    private readonly Data.Core.DataBase<T> _dataBase;

    public DeleteStrategyFactory(Data.Core.DataBase<T> dataBase)
    {
        _dataBase = dataBase;
    }

    public IDeletedStrategy Create(DeleteType tipo) 
        => tipo switch
        {
            DeleteType.Logica => new DeletedLogicalStrategy<T>(_dataBase),
            DeleteType.Permanente => new DeletedPersistentStrategy<T>(_dataBase),
            _ => throw new ArgumentException("Tipo de eliminación no soportado")
        };
}
