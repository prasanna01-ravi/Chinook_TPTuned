using AutoMapper;
using Chinook.ClientModels;
using Chinook.Data;
using Chinook.Data.UnitOfWork;
using Chinook.Services.Interface;
using System.Linq.Expressions;

namespace Chinook.Services.Implementation
{
    public class ArtistService : BaseService<Artist, Models.Artist>, IArtistService
    {
        public ArtistService(IArtistRepostory repository, ILogger<ArtistService> logger, IMapper mapper, 
            IUnitOfWork uow) : base(repository, logger, mapper, uow)
        {
        }

        public async Task<List<Artist>> GetAdvancedDet(Expression<Func<Models.Artist, bool>> expression)
        {
            Logger.LogInformation($"Get all artists information");
            return Mapper.Map<List<Artist>>(await ((IArtistRepostory)Repository).GetAdvancedDet(expression));
        }
    }
}
