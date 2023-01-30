using Microsoft.EntityFrameworkCore.Storage;

namespace Chinook.Data.UnitOfWork
{
    /// <summary>
    /// The UnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The ChinookContext
        /// </summary>
        private ChinookContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="context">The ChinookContext</param>
        public UnitOfWork(ChinookContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Commit the data
        /// </summary>
        /// <returns></returns>
        public async Task Commit()
        {
            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(true);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
