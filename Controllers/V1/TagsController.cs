using ApiModel.Contracts;
using ApiModel.Contracts.V1.Requests;
using ApiModel.Domain;
using ApiModel.Extensions;
using ApiModel.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiModel.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Poster")]
    public class TagsController : Controller
    {
        private readonly IPostService _postService;
        public TagsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Tags.GetAll)]
        [Authorize(Policy = "TagViewer")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetAllTagsAsync());
        }

        public IActionResult Ping()
        {
            return Ok("pong");
        }

        [HttpPost(ApiRoutes.Tags.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            var newTag = new Tag
            {
                Name = request.TagName,
                CreatorId = HttpContext.GetUserId(),
                CreatedOn = DateTime.UtcNow

            };

            var created = await _postService.CreateTagAsync(newTag);
            if (!created)
            {
                return BadRequest(new { error = "Unable to create tag"});
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Tags.Get.Replace("{tagName}", newTag.Name);
            return Created(locationUri, newTag);
        }

        [HttpDelete(ApiRoutes.Tags.Delete)]
        [Authorize(Roles ="Admin")]
        [Authorize(Policy = "MustWorkForChapsas")]
        public async Task<IActionResult> Delete([FromRoute] string tagName)
        {
            var deleted = await _postService.DeleteTagAync(tagName);
            if (deleted)
                return NoContent();

            return NotFound();
           
        }

        [HttpGet(ApiRoutes.Tags.Get)]
        public async Task<IActionResult> Get([FromRoute] string tagName)
        {
            var tag = await _postService.GetTagByNameAsync(tagName);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        [HttpPut(ApiRoutes.Tags.Update)]    
        public async Task<IActionResult> Update([FromRoute] Guid tagId, [FromBody] UpdateTagsRequest request)
        {   
            var userOwnsTags = await _postService.UserOwnsPostAsync(tagId, HttpContext.GetUserId());   

            if (!userOwnsTags)
                return BadRequest(new { error = "You do not own this tag" });

            var tags = await _postService.GetTagByIdAsync(tagId);
            tags.Name = request.Name;

            var updated = await _postService.UpdateTagsAsync(tags);

            if (updated)

                return Ok(tags);


            return NotFound();
        }
    }
}
