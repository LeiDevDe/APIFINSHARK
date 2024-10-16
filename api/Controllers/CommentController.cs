using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interface;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
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
            var commentDtos = comments.Select(x => x.ToCommentDto());
            return Ok(commentDtos);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
                return NotFound();
            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentRequestDto createCommentRequestDto)
        {
            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exists");
            }
            var commentModel = createCommentRequestDto.ToCommnetFromCreateDTO(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var commentModel = await _commentRepo.DeleteAsync(id);
            if (commentModel == null)
                return NotFound();

            return NoContent();
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, CreateCommentRequestDto createCommentRequestDto)
        {
            var commentModel = await _commentRepo.UpdateAsync(id, createCommentRequestDto);
            if (commentModel == null)
            {
                return NotFound();
            }
            return Ok(commentModel);
        }

    }
}