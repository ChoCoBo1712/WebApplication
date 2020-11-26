using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {  
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> entities;  
        
        public Repository(ApplicationDbContext context)  
        {  
            this.context = context;  
            entities = context.Set<T>();  
        }  
        
        public IEnumerable<T> GetAll()  
        {  
            return entities.AsEnumerable();  
        }  
        
        public T Get(int id)  
        {  
            return entities.SingleOrDefault(t => t.Id == id); //TODO change to FirstOrDefault if error occurs
        }

        public int Save(T entity)
        {
            context.Entry(entity).State = entity.Id == default ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            
            return entity.Id;
        }

        public void Delete(int id)
        {
            var entity = entities.SingleOrDefault(x => x.Id == id); //TODO change to FirstOrDefault if error occurs
            
            if (entity != null)
            {
                entities.Remove(entity);
            }

            context.SaveChanges();
        }
    }
}