﻿using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISqlRepository : IDisposable
    {
        void Delete<TEntity>(TEntity entity) where TEntity : class;

        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;

        TEntity GetById<TEntity>(int id) where TEntity : BaseModel<int>;

        void Insert<TEntity>(TEntity entity) where TEntity : class;

        void SaveChanges();
    }
}
