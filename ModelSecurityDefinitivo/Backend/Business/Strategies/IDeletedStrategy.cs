namespace Business.Strategies;

public interface IDeletedStrategy
{
    Task Delete(int id);
}
