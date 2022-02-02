using DataService.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Aluma.API.RepoWrapper
{
    public interface IRepoBase<T>
    {
        IQueryable<T> FindAll();

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }

    public class RepoBase<T> : IRepoBase<T> where T : class
    {
        protected AlumaDBContext DatabaseContext { get; set; }

        public RepoBase(AlumaDBContext repositoryContext)
        {
            DatabaseContext = repositoryContext;
        }

        public IQueryable<T> FindAll()
        {
            return DatabaseContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return DatabaseContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            entity.GetType().GetProperty("Created").SetValue(entity, DateTime.Now);
            entity.GetType().GetProperty("Modified").SetValue(entity, DateTime.Now);
            DatabaseContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            entity.GetType().GetProperty("Modified").SetValue(entity, DateTime.Now);
            DatabaseContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            DatabaseContext.Set<T>().Remove(entity);
        }
    }
}