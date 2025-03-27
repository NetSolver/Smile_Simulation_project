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
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class LikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IPostRepository _postRepository;
        private readonly SmileSimulationDbContext _context;  // استخدام الـ DbContext للوصول إلى DbSet<User>
        private readonly IMapper _mapper;

        public LikeService(ILikeRepository likeRepository, IPostRepository postRepository, SmileSimulationDbContext context, IMapper mapper)
        {
            _likeRepository = likeRepository;
            _postRepository = postRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> AddLikeAsync(int postId, int userId)
        {
          
            if (!await _postRepository.ExistsAsync(postId))
                return "Post not found";

            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
                return "User not found";

       
            if (await _likeRepository.IsPostLikedByUserAsync(postId, userId))
                return "Already liked";

            var like = new Like { PostId = postId, UserId = userId };
            await _likeRepository.AddAsync(like);
            await _likeRepository.SaveChangesAsync();
            return "Like added successfully";
        }

        public async Task<string> RemoveLikeAsync(int postId, int userId)
        {
            var like = await _likeRepository.GetByPostAndUserAsync(postId, userId);

            if (like == null)
                return "Like not found";

            _likeRepository.Delete(like);
            await _likeRepository.SaveChangesAsync();
            return "Like removed successfully";
        }
    }


}
