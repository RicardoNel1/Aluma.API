using DataService.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Aluma.API.RepoWrapper
{
    public interface IRepoBase<T>
    {
        #region Public Methods

        void Create(T entity);

        void Delete(T entity);

        IQueryable<T> FindAll();

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Update(T entity);

        #endregion Public Methods
    }

    public class RepoBase<T> : IRepoBase<T> where T : class
    {
        #region Public Constructors

        public RepoBase(AlumaDBContext repositoryContext)
        {
            DatabaseContext = repositoryContext;
        }

        #endregion Public Constructors

        #region Protected Properties

        protected AlumaDBContext DatabaseContext { get; set; }

        #endregion Protected Properties

        #region Public Methods

        public void Create(T entity)
        {
            entity.GetType().GetProperty("Created").SetValue(entity, DateTime.Now);
            entity.GetType().GetProperty("Modified").SetValue(entity, DateTime.Now);
            DatabaseContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            DatabaseContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return DatabaseContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return DatabaseContext.Set<T>().Where(expression).AsNoTracking();
        }
        public void Update(T entity)
        {
            entity.GetType().GetProperty("Modified").SetValue(entity, DateTime.Now);
            DatabaseContext.Set<T>().Update(entity);
        }

        #endregion Public Methods
    }
}