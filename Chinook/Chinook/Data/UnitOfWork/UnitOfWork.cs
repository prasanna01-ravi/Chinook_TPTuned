using Microsoft.EntityFrameworkCore.Storage;

namespace Chinook.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ChinookContext _context; 
        public UnitOfWork(ChinookContext context)
        {
            this._context = context;
        }

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
