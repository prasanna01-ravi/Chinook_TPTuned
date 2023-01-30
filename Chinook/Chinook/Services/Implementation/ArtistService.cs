using AutoMapper;
using Chinook.ClientModels;
using Chinook.Data;
using Chinook.Data.UnitOfWork;
using Chinook.Services.Interface;
using System.Linq.Expressions;

namespace Chinook.Services.Implementation
{
    /// <summary>
    /// The ArtistService
    /// </summary>
    public class ArtistService : BaseService<Artist, Models.Artist>, IArtistService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistService" /> class.
        /// </summary>
        /// <param name="repository">The IArtistRepostory</param>
        /// <param name="logger">The ArtistService Logger</param>
        /// <param name="mapper">The IMapper</param>
        /// <param name="uow">The Unit Of Work</param>
        public ArtistService(IArtistRepostory repository, ILogger<ArtistService> logger, IMapper mapper, 
            IUnitOfWork uow) : base(repository, logger, mapper, uow)
        {
        }

        /// <summary>
        /// Get the advanced details of Authors
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns></returns>
        public async Task<List<Artist>> GetAdvancedDet(Expression<Func<Models.Artist, bool>> expression)
        {
            Logger.LogInformation($"Get all artists information");
            return Mapper.Map<List<Artist>>(await ((IArtistRepostory)Repository).GetAdvancedDet(expression));
        }
    }
}
