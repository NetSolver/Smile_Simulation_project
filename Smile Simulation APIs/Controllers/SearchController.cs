using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Smile_Simulation_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _searchService;

        public SearchController(SearchService searchService)
        {
            _searchService = searchService; 
        }

        [HttpGet("GlobalSearch")]
        public async Task<IActionResult> GlobalSearch(string searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
                return BadRequest("يجب إدخال عبارة البحث."); 

          
            var (results, totalCount) = await _searchService.GlobalSearchAsync(searchQuery, pageNumber, pageSize);

         
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

           
            var response = new
            {
                Results = results,    
                CurrentPage = pageNumber,   
                TotalPages = totalPages,     
                TotalResults = totalCount    
            };

            return Ok(response);  
        }

    }
}
