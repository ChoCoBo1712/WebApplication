using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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
            var efTag = context.Tags.ToList();
            var tag = mapper.Map<List<Tag>>(efTag);
            foreach (var pair in efTag.Zip(tag, 
                (efTag, tag) => new { EFTag = efTag, Tag = tag }))
            {
                pair.Tag.Songs = mapper.Map<List<Song>>(context.Songs.Where(t => t.Id == pair.EFTag.Id));
            }

            return tag;  
        }  
        
        public Tag Get(int id)  
        {  
            var efTag = context.Tags.FirstOrDefault(t => t.Id == id);

            if (efTag == null)
                return null;
            
            var tag = mapper.Map<Tag>(efTag);
            tag.Songs = mapper.Map<List<Song>>(context.Songs.Where(t => t.Id == efTag.Id));

            return tag;
        }
    
        public int Save(Tag tag)
        {
            var efTag = mapper.Map<Tag>(tag);
            efTag.Songs= mapper.Map<List<Song>>(context.Songs.Where(t => t.Id == tag.Id));
            context.Entry(efTag).State = efTag.Id == default ? EntityState.Added : EntityState.Modified;
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