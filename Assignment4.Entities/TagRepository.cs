using System.Collections.Generic;
using Assignment4.Core;
using System.Linq;

namespace Assignment4.Entities
{
    public class TagRepository : ITagRepository
    {
        private readonly KanbanContext _context;

        public TagRepository(KanbanContext context)
        {
            _context = context;
        }

        public (Response Response, int TagId) Create(TagCreateDTO tag){
            var entity = new Tag { Name = tag.Name};

            _context.Tag.Add(entity);

            _context.SaveChanges();

            return (Response.Created, entity.TagID);            
        }

        public IReadOnlyCollection<TagDTO> ReadAll()
        {
            return _context.Tag
                    .Select(t => new TagDTO(t.TagID, t.Name))
                    .ToList().AsReadOnly();
        }

        public TagDTO Read(int tagId)
        {
            var tags = from t in _context.Tag
                         where t.TagID == tagId
                         select new TagDTO(t.TagID, t.Name);
            return tags.FirstOrDefault();
        }
        
        public Response Update(TagUpdateDTO tag)
        {
            var entity = _context.Tag.Find(tag.Id);

            if (entity == null)
            {
                return Response.NotFound;
            }

            entity.TagID = tag.Id;
            entity.Name = tag.Name;

            _context.SaveChanges();

            return Response.Updated;
        }
        public Response Delete(int tagId, bool force = false)
        {
            var entity = _context.Tag.Find(tagId);

            if(entity == null)
            {
                return Response.NotFound;
            }

            if(entity.Tasks[0] != null && force == false)
            {
                return Response.Conflict;
            }

            _context.Tag.Remove(entity);
            _context.SaveChanges();

            return Response.Deleted;
        }
    }
}