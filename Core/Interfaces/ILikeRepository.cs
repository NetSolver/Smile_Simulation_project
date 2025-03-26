using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ILikeRepository : IRepository<Like>
    {
        Task<bool> IsPostLikedByUserAsync(int postId, int userId);
        Task<Like?> GetByPostAndUserAsync(int postId, int userId);

    }
}
