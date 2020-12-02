using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class TagRepository : IRepository<Tag>
    {  
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TagRepository(ApplicationDbContext context, IMapper mapper)  
        {  
            this.context = context;
            this.mapper = mapper;
        }  
        
        public List<Tag> GetAll()  
        {  
            var efTags = context.Tags.Include(t => t.Songs).ToList();
            var tags = mapper.Map<List<Tag>>(efTags);

            return tags;  
        }  
        
        public Tag Get(int id)  
        {  
            var efTags = context.Tags.Include(t => t.Songs).ToList();
            var efTag = efTags.FirstOrDefault(t => t.Id == id);

            if (efTag == null)
                return null;
            
            var tag = mapper.Map<Tag>(efTag);
            tag.Songs = mapper.Map<List<Song>>(context.Songs.Where(t => t.Id == efTag.Id));

            return tag;
        }
    
        public int Save(Tag tag)
        {
            var efTag = mapper.Map<EFTag>(tag);
            
            if (efTag.Id == default)
            {
                context.Entry(efTag).State = EntityState.Added;
            }
            else
            {
                var entry = context.Tags.First(t => t.Id == efTag.Id);
                context.Entry(entry).State = EntityState.Detached;
                context.Entry(efTag).State = EntityState.Modified;
            }
            context.SaveChanges();
            
            return efTag.Id;
        }
    
        public void Delete(int id)
        {
            var efTag = context.Tags.FirstOrDefault(t => t.Id == id);

            if (efTag != null)
            {
                context.Tags.Remove(efTag);
            }

            context.SaveChanges();
        }
    }
}