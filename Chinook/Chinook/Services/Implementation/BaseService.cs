using AutoMapper;
using Chinook.ClientModels;
using Chinook.Data;
using Chinook.Data.UnitOfWork;
using Chinook.Services.Interface;
using System.Linq.Expressions;

namespace Chinook.Services.Implementation
{
    public abstract class BaseService<TObject, TEntity> : IBaseService<TObject, TEntity>
       where TObject : class, BaseClientModel
       where TEntity : class
    {
        protected IBaseRepository<TEntity> Repository { get; set; }

        protected ILogger Logger { get; set; }
        protected IMapper Mapper { get; set; }
        protected IUnitOfWork UOW { get; set; }

        protected BaseService(IBaseRepository<TEntity> repository, ILogger logger, IMapper mapper, IUnitOfWork uow)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            UOW = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public virtual async Task<List<TObject>> GetAllAsync()
        {
            return Mapper.Map<List<TObject>>(await Repository.GetAll());
        }

        public virtual async Task<List<TObject>> GetAllByCondtionAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Mapper.Map<List<TObject>>(await Repository.GetAllByCondition(expression));
        }
    }
}
