using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        int Save(T entity);
        void Delete(int id);
    }
}