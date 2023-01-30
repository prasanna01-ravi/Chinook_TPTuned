using AutoMapper;
using Chinook.ClientModels;
using Chinook.Data;
using Chinook.Data.UnitOfWork;
using Chinook.Services.Interface;
using System.Linq.Expressions;

namespace Chinook.Services.Implementation
{
    /// <summary>
    /// The BaseService
    /// </summary>
    public abstract class BaseService<TObject, TEntity> : IBaseService<TObject, TEntity>
       where TObject : class, BaseClientModel
       where TEntity : class
    {
        /// <summary>
        /// The Repository
        /// </summary>
        protected IBaseRepository<TEntity> Repository { get; set; }

        /// <summary>
        /// The Logger
        /// </summary>
        protected ILogger Logger { get; set; }

        /// <summary>
        /// The Mapper
        /// </summary>
        protected IMapper Mapper { get; set; }

        /// <summary>
        /// The UOW
        /// </summary>
        protected IUnitOfWork UOW { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService" /> class.
        /// </summary>
        /// <param name="repository">The IArtistRepostory</param>
        /// <param name="logger">The ArtistService Logger</param>
        /// <param name="mapper">The IMapper</param>
        /// <param name="uow">The Unit Of Work</param>
        protected BaseService(IBaseRepository<TEntity> repository, ILogger logger, IMapper mapper, IUnitOfWork uow)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            UOW = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Get all the instances
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TObject>> GetAllAsync()
        {
            return Mapper.Map<List<TObject>>(await Repository.GetAll());
        }

        /// <summary>
        /// Get the advanced details of instances
        /// </summary>
        /// <param name="expression">The expression</param>
        /// <returns></returns>
        public virtual async Task<List<TObject>> GetAllByCondtionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Mapper.Map<List<TObject>>(await Repository.GetAllByCondition(expression));
        }
    }
}
