using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Reposotries;

namespace Application.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<CommentDTO>> GetAllCommentsByPostIdAsync(int postId)
        {
            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
            return _mapper.Map<List<CommentDTO>>(comments);
        }

        public async Task<CommentDTO> AddCommentAsync(CommentDTO commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            comment.CreatedAt = DateTime.UtcNow;

            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();

            return _mapper.Map<CommentDTO>(comment);
        }
        public async Task<CommentDTO?> UpdateCommentAsync(int postId, int commentId, string newContent){
        
            var comment = await _commentRepository.GetCommentByPostIdAndCommentIdAsync(postId, commentId);

            if (comment == null) return null; 

          
            comment.Content = newContent;
            await _commentRepository.SaveChangesAsync();

    
            return _mapper.Map<CommentDTO>(comment);
        }



        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null) return false;

            _commentRepository.Delete(comment);
            await _commentRepository.SaveChangesAsync();
            return true;
        }
    }
}