using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Reposotries;
namespace Application.Services
{
    public class SearchService
    {
        private readonly IPostRepository _postRepository;
        //private readonly  User _user; // Assuming you have this

        public SearchService(IPostRepository postRepository) //, User userRepository)
        {
            _postRepository = postRepository;
            //_user = userRepository;
        }
        public async Task<(List<dynamic> Results, int TotalCount)> GlobalSearchAsync(string searchQuery, int pageNumber, int pageSize)
        {
            searchQuery = searchQuery.ToLower();

            // البحث في المنشورات  
            var posts = (await _postRepository.GetAllAsync())
                .Where(p => p.Content.ToLower().Contains(searchQuery))
                .Select(p => new { Type = "Post", p.Content });

            // تطبيق Pagination وتحويل إلى dynamic
            var combinedResults = posts.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Cast<dynamic>()  // تحويل إلى dynamic
                .ToList();

            int totalCount = posts.Count(); // حساب عدد النتائج

            return (combinedResults, totalCount);
        }

    }
}