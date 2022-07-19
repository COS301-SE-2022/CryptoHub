﻿using System;
using Domain.Models;
using Infrastructure.DTO.PostDTO;


namespace BusinessLogic.Services.PostService
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetAllPosts();
        Task<List<PostDTO>> GetPostByUserId(int id);
        Task<PostDTO> AddPost(CreatePostDTO createPostDTO); 
        Task<PostDTO> UpdatePost(Post Post);
        Task Delete(int id);
        Task<PostReport> Report(int Postid, int UserId);
    }
}

