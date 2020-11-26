using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> GetAllEntities();
        T GetEntity(int id);
        int SaveEntity(T entity);
        void DeleteEntity(int id);
    }
}