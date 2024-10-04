using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repository; 

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> CompleteAsync()
        =>await _context.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (_repository == null)
            
                _repository = new Hashtable();
            var entityKey=typeof(TEntity).Name; //Product

            if (!_repository.ContainsKey(entityKey))
            {
                var repositoryType = typeof(GenericRepository<,>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TKey)), _context);
                _repository.Add(entityKey, repositoryInstance);
            }
            return (IGenericRepository < TEntity, TKey >) _repository[entityKey];
        }
    }
}
