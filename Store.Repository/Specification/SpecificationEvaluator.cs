﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;

namespace Store.Repository.Specification
{
    public class SpecificationEvaluator<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        public static IQueryable<TEntity>GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specs)
        {
            var query = inputQuery;

            if (specs.Criteria is not null)
          
                query = query.Where(specs.Criteria);

            if (specs.OrderBy is not null)

                query = query.OrderBy(specs.OrderBy);

            if (specs.OrderByDescending is not null)

                query = query.OrderByDescending(specs.OrderByDescending);

            if (specs.IsPaginated )

                query = query.Skip(specs.Skip).Take(specs.Take);


            query = specs.Includes.Aggregate(query, (current, incliudeExpression) => current.Include(incliudeExpression));
            return query;
        }
    }
}
