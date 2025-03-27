using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Reposotries;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly SmileSimulationDbContext _context;
        public PostRepository(SmileSimulationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetLikesCountAsync(int postId) => await _context.Likes.CountAsync(l => l.PostId == postId);
        public async Task<int> GetCommentsCountAsync(int postId) => await _context.Comments.CountAsync(c => c.PostId == postId);
        public async Task<bool> ExistsAsync(int postId)
        {
            return await _context.Posts.AnyAsync(p => p.Id == postId);
        }
    }
}
