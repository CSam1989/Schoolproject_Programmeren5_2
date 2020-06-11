using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class EFRepo<TEntity> : EfReadOnlyRepo<TEntity>, IRepository<TEntity>
        where TEntity : class
    {
        #region Ctor DI

        public EFRepo(ApplicationDbContext context) : base(context)
        {
        }

        #endregion

        #region CrUD methods

        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            var dbSet = Context.Set<TEntity>();

            if (Context.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);

            dbSet.Remove(entity);
        }

        #endregion
    }
}