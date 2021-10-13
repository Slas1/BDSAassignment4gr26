using Assignment4.Core;

namespace Assignment4.Entities
{
    public class TagRepository : ITagRepository
    {
        private readonly IKanbanContext _context;

        public CityRepository(IKanbanContext context)
        {
            _context = context;
        }

        public (Response Response, int TagID) Create(TagCreateDTO tag){
            var entity = new Tag { Name = tag.Name};

            _context.Tags.Add(entity);

            _context.SaveChanges();

            return (Response.Created, entity.TagID);            
        }

        IReadOnlyCollection<TagDTO> ReadAll();


        public TagDTO Read(int tagId)
        {
            var tags = from t in _context.tags
                         where c.TagID == tagId
                         select new TagDTO(c.TagID, c.Name);
            return tags.FirstOrDefault();
        }
        
        public Response Update(TagUpdateDTO tag){

        }

        Response Update(TagUpdateDTO tag);
        Response Delete(int tagId, bool force = false);
    }
}