using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApiModel.Contracts.V1.Requests;
using ApiModel.Contracts.V1.Response;

namespace ApiModel.Sdk
{
    [Headers("Authorization: Bearer")]
    public interface IApiModelApi
    {
        [Get("/api/v1/posts")]
        Task<ApiResponse<List<PostResponse>>> GetAllAsync();

        [Get("/api/v1/posts/{postId}")]
        Task<ApiResponse<PostResponse>> GetAsync(Guid postId);

        [Get("/api/v1/posts")]
        Task<ApiResponse<PostResponse>> CreateAsync([Body] CreatePostRequest createPostRequest);   

        [Put("/api/v1/posts/{postId}")]
        Task<ApiResponse<PostResponse>> UpdateAsync(Guid postId, [Body] UpdatePostRequest updatePostRequest);

        [Delete("/api/v1/posts/{postId}")]
        Task<ApiResponse<PostResponse>> DeleteAsync(Guid postId);

    }
}
