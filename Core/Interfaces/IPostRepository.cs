using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Core.Reposotries
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<int> GetLikesCountAsync(int postId);
        Task<int> GetCommentsCountAsync(int postId);
    }

}

