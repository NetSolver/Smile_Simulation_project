using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly SmileSimulationDbContext _context;

        public LikeRepository(SmileSimulationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> IsPostLikedByUserAsync(int postId, int userId) =>
            await _context.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);
        public async Task<Like?> GetByPostAndUserAsync(int postId, int userId)
        {
            return await _context.Likes
                .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
        }
    }
}
