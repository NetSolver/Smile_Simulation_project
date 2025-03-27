using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Core.Reposotries
{

    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<Comment?> GetCommentByPostIdAndCommentIdAsync(int postId, int commentId);
    }

}
