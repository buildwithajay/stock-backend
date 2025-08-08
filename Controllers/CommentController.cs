using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Extesnsions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _user;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo, UserManager<AppUser> user)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _user = user;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentdtos = comments.Select(s => s.ToCommentDtos());
            return Ok(commentdtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
             if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetByIdAsync(id);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments.ToCommentDtos());
        }
        [HttpPost]
        [Route("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {
             if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            if (!await _stockRepo.ExistingStockAsync(stockId))
            {
                return BadRequest("stock not found");
            }
            var username = User.GetUsername();
            var appUser = await _user.FindByNameAsync(username);

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            commentModel.AppUserId = appUser.Id;
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDtos());
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCommentRequestDto updateComment)
        {
             if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            var commentModel = await _commentRepo.UpdateAsync(id, updateComment.ToCommentFromUpdate());
            if (commentModel == null)
            {
                return NotFound();
            }
            return Ok(commentModel.ToCommentDtos());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
             if (!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            var comment = await _commentRepo.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound("comment doesnot exist");
            }
            return Ok(comment);
        }
    }
}