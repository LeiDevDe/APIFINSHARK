using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interface;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext context;

        public CommentRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null)
            {
                return null;
            }
            context.Comments.Remove(commentModel);
            await context.SaveChangesAsync();
            return commentModel;
        }

        async Task<List<Comment>> ICommentRepository.GetAllAsync()
        {
            return await context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await context.Comments.FindAsync(id);
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {

            await context.Comments.AddAsync(commentModel);
            await context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> UpdateAsync(int id, CreateCommentRequestDto commentRequestDto)
        {
            var existingComment = await context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = commentRequestDto.Title;
            existingComment.Content = commentRequestDto.Content;

            await context.SaveChangesAsync();
            return existingComment;

        }
    }
}