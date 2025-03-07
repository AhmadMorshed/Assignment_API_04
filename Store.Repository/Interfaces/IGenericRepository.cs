﻿using Store.Data.Entities;
using Store.Repository.Specification;
using Store.Repository.Specification.OrderSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interfaces
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey? id);

        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAllAsNoTraAsync();
        Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> specs);

        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs);
        Task<int> GetCountSpecificationAsync(ISpecification<TEntity> specs);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task GetAllWithSpecificationByIdAsync(OrderWithItemSpecification specs);
        Task GetAllWithSpecificationByIdAsync(OrderWithPaymentIntentSpecifaction specs);
    }
}
