namespace Chinook.Data.UnitOfWork
{
    /// <summary>
    /// The IUnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commit the data
        /// </summary>
        /// <returns></returns>
        Task Commit();
    }
}
