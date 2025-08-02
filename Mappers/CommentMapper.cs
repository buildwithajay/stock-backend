using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDtos ToCommentDtos(this Comment comment)
        {
            return new CommentDtos
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                StockId = comment.StockId
            };
        }
         public static Comment ToCommentFromCreate(this CreateCommentDto comment, int stockId)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
                StockId = stockId
            };
        }
              public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto comment)
        {
            return new Comment
            {
                Title = comment.Title,
                Content = comment.Content,
             
            };
        }
    }
}