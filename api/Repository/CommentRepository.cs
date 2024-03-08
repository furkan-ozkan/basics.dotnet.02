using api.Data;
using api.Dtos.Comment;
using api.Interface;
using api.Mapper;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _context.comments.FirstOrDefaultAsync(x => x.Id == id);

            if (commentModel == null)
                return null;

            _context.comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.comments.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.comments.Include(a => a.AppUser).FirstOrDefaultAsync(x => x.StockId == id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var existingComment = await _context.comments.FirstOrDefaultAsync(x => x.Id == id);

            if (existingComment == null)
                return null;

            existingComment.Title = comment.Title;
            existingComment.Content = comment.Content;

            await _context.SaveChangesAsync();
            return existingComment;
        }
    }
}