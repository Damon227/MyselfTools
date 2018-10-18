// ***********************************************************************
// Solution         : MyselfTools
// Project          : EFCoreTest
// File             : EntityService.cs
// Updated          : 2018-05-26 13:42
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2017 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTest
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : KCEntity
    {
        private readonly DbContext dbContext;

        public EntityService(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region IEntityService<TEntity> Members

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }
        #endregion
    }
}