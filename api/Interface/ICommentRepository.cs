using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Interface
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync();
        public Task<Comment?> GetByIdAsync(int id);
        public Task<Comment?> UpdateAsync(int id, CreateCommentRequestDto commentRequestDto);
        public Task<Comment> CreateAsync(Comment commentModel);
        public Task<Comment?> DeleteAsync(int id);
    }

}