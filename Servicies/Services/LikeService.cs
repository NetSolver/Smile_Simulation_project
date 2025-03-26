using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services
{
    public class LikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IMapper _mapper;

        public LikeService(ILikeRepository likeRepository, IMapper mapper)
        {
            _likeRepository = likeRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddLikeAsync(int postId, int userId)
        {
            if (await _likeRepository.IsPostLikedByUserAsync(postId, userId))
                return false;

            var like = new Like { PostId = postId, UserId = userId };
            await _likeRepository.AddAsync(like);
            await _likeRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveLikeAsync(int postId, int userId)
        {
            var like = await _likeRepository.GetByPostAndUserAsync(postId, userId);
            //to remove from database u need to get the object first not only check so getpostanduserasync is used not ispostlikedbyuserasync
            if (like == null)
                return false;

            _likeRepository.Delete(like);
            await _likeRepository.SaveChangesAsync();
            return true;
        }
    }
}
