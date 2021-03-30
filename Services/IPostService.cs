using ApiModel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiModel.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync();   

        Task<bool> CreatePostAsync(Post post);  

        Task<Post> GetPostByIdAsync(Guid postId);

        Task<bool> UpdatePostAsync(Post postToUpdate);  

        Task<bool> DeletePostAsync(Guid postId);
        Task<bool> UserOwnsPostAsync(Guid postId, string getUserId);
        Task<List<Tag>> GetAllTagsAsync();
        Task<bool> CreateTagAsync(Tag newTag);
        Task<bool> DeleteTagAync(string tagName);
        Task<Tag> GetTagByNameAsync(string tagName);
    }
}
