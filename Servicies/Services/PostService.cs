using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Reposotries;

namespace Application.Services
{

    public class PostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, ILikeRepository likeRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _likeRepository = likeRepository;
            _mapper = mapper;
        }

         public async Task<IReadOnlyList<PostDTO>> GetAllPostsAsync(int currentUserId)
        {
            var posts = await _postRepository.GetAllAsync();
            var postDTOs = _mapper.Map<List<PostDTO>>(posts);

            foreach (var postDTO in postDTOs)
            {
                var post = posts.First(p => p.Id == postDTO.Id);
                postDTO.IsLikedByCurrentUser = post.Likes.Any(like => like.UserId == currentUserId);
                postDTO.LikesCount = post.Likes.Count; 
                postDTO.CommentsCount = post.Comments.Count; 
            }

            return postDTOs;
        }

        public async Task<PostDTO> GetPostByIdAsync(int postId, int currentUserId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null) return null;

            var postDTO = _mapper.Map<PostDTO>(post);
            postDTO.IsLikedByCurrentUser = post.Likes.Any(like => like.UserId == currentUserId);
            postDTO.LikesCount = post.Likes.Count;
            postDTO.CommentsCount = post.Comments.Count;

            return postDTO;
        }


        public async Task<PostDTO> AddPostAsync(Post post)
        {
            post.CreatedAt = DateTime.UtcNow;
            await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync();

            return _mapper.Map<PostDTO>(post);
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null) return false;

            _postRepository.Delete(post);
            await _postRepository.SaveChangesAsync();
            return true;
        }

        public async Task<PostDTO> UpdatePostAsync(Post updatedPost)
        {
            var post = await _postRepository.GetByIdAsync(updatedPost.Id);
            if (post == null) return null;

            post.Content = updatedPost.Content;
            post.ImageUrl = updatedPost.ImageUrl;

            _postRepository.Update(post);
            await _postRepository.SaveChangesAsync();

            var postDTO = _mapper.Map<PostDTO>(post);
            postDTO.IsLikedByCurrentUser = false; //edit

            return postDTO;
        }
    }
}
