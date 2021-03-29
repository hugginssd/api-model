using ApiModel.Contracts;
using ApiModel.Contracts.V1.Requests;
using ApiModel.Domain;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiModel.IntegrationTests
{
    public class  PostsControllerTests : IntegrationTest
    {
       
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnsEmptyResponse()
        {
            //Arrange
            await AuthenticateAsync();

            //Act

            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);



            //Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();

        }



        [Fact]
        public async Task Get_ReturnsPost_WhenPostExistsInTheDatabase()
        {
            //Arrange 
            await AuthenticateAsync();
            var createdPost = await CreatePostAsync(new CreatePostRequest { Name = "Test post" });


            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.Get.Replace("{postId}", createdPost.Id.ToString()));



            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<Post>();
            returnedPost.Id.Should().Be(createdPost.Id);
            returnedPost.Name.Should().Be("Test post");
        }
    }
}
