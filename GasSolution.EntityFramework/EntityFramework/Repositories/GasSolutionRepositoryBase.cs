using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace GasSolution.EntityFramework.Repositories
{
    public abstract class GasSolutionRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<GasSolutionDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected GasSolutionRepositoryBase(IDbContextProvider<GasSolutionDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class GasSolutionRepositoryBase<TEntity> : GasSolutionRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected GasSolutionRepositoryBase(IDbContextProvider<GasSolutionDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
