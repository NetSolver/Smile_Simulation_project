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
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {  
        private readonly SmileSimulationDbContext _context;

        public CommentRepository(SmileSimulationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId) =>
            await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
    }
}
