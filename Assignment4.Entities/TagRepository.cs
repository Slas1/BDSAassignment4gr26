using Assignment4.Core;

namespace Assignment4.Entities
{
    public class TagRepository : ITagRepository
    {

        (Response Response, int TagId) Create(TagCreateDTO tag);
        IReadOnlyCollection<TagDTO> ReadAll();
        TagDTO Read(int tagId);
        Response Update(TagUpdateDTO tag);
        Response Delete(int tagId, bool force = false);
    }
}