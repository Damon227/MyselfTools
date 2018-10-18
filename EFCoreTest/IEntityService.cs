// ***********************************************************************
// Solution         : MyselfTools
// Project          : EFCoreTest
// File             : IEntityService.cs
// Updated          : 2018-05-26 13:42
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2017 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EFCoreTest
{
    public interface IEntityService<TEntity> where TEntity : KCEntity
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> EnableAsync(TEntity entity);

        Task<TEntity> DisableAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<TEntity> QueryAsync(string id);

        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> QueryFirstAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> QueryAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate);
    }
}