using Microsoft.EntityFrameworkCore;
using ProductApi.Context;
using System.Linq.Expressions;

namespace ProductApi.Services
{
    public class GeneralService<T>:IGeneralService<T> where T : class
    {
        private ServiceContext _serviceContext { get; set; }
        public GeneralService(ServiceContext serviceContext)
        {
           _serviceContext = serviceContext;
        }
        public Task<IQueryable<T>> FindAll() 
        {
            return  Task.FromResult(_serviceContext.Set<T>().AsNoTracking());
        }
        public Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {
            var query = _serviceContext.Set<T>().Where(expression).AsNoTracking();

            if (query.Any())
            {
                return Task.FromResult(query);
            }
            return Task.FromResult(Enumerable.Empty<T>().AsQueryable());
        }
            public Task Create(T entity)
        {
            _serviceContext.Set<T>().Add(entity);
            _serviceContext.SaveChanges();
            return Task.CompletedTask;  
        }
        public Task Update(T entity)
        {
            _serviceContext.Set<T>().Update(entity);
            _serviceContext.SaveChanges();
            return Task.CompletedTask;

        }
        public Task Delete(T entity)
        {
            _serviceContext.Set<T>().Remove(entity);
            _serviceContext.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
