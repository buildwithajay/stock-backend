using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentdtos = comments.Select(s => s.ToCommentDtos());
            return Ok(commentdtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comments = await _commentRepo.GetByIdAsync(id);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments.ToCommentDtos());
        }
        [HttpPost]
        [Route("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if (!await _stockRepo.ExistingStockAsync(stockId))
            {
                return BadRequest("stock not found");
            }
            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDtos());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentRequestDto updateComment)
        {
            var commentModel = await _commentRepo.UpdateAsync(id, updateComment.ToCommentFromUpdate());
            if (commentModel == null)
            {
                return NotFound();
            }
            return Ok(commentModel.ToCommentDtos());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepo.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound("comment doesnot exist");
            }
            return Ok(comment);
        }
    }
}