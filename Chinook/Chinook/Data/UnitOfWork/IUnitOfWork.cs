namespace Chinook.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
