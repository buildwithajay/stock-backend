using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
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
            var comment = await _context.comments.FirstOrDefaultAsync(x=>x.Id==id);
            if (comment == null)
            {
                return null;

            }
            _context.comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.comments.Include(c=>c.AppUser).ToListAsync();


        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comments = await _context.comments.Include(c=>c.AppUser).FirstOrDefaultAsync(s=>s.Id==id);
            if (comments == null)
            {
                return null;
            }
            return comments;
        }

       

        public  async Task<Comment?> UpdateAsync(int id, Comment comentModel)
        {
             var comment = await _context.comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }
            comment.Title = comentModel.Title;
            comment.Content = comentModel.Content;
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}