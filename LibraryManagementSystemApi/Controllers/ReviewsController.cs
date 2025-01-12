using Application.Commands;
using Application.Commands.ReviewsCommand;
using Application.DTOs;
using Application.Queries;
using Application.Queries.ReviewsQuery;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ISender _sender;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(IReviewService reviewService, ISender sender, ILogger<ReviewsController> logger)
        {
            _reviewService = reviewService;
            _sender = sender;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ReviewsEntity>> GetAllReviews()
        {
            return await _sender.Send(new GetAllReviewsQuery());
        }

        [HttpGet("{id}")]

        public async Task<ReviewsEntity> GetReviewById(int id)
        {
            return await _sender.Send(new GetReviewByUserIdQuery(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromForm] ReviewDtos review)
        {
            try
            {
                
                // Validate and normalize the date format
                if (!DateTime.TryParse(review.ReviewDate, out DateTime parsedDate))
                {
                    return BadRequest("Invalid date format. Please use 'yyyy-MM-dd'.");
                }

                // Convert the normalized date to DateOnly
                var reviewDate = DateOnly.FromDateTime(parsedDate);
                var createdReview = await _sender.Send<ReviewsEntity>(new CreateReviewCommand(

                    review.UserId,
                    review.BookId,
                    review.Rating,
                    reviewDate,
                    review.Comments));

                return Ok(new
                {
                    createdReview.ReviewId,
                    createdReview.UserId,
                    createdReview.BookId,
                    createdReview.Rating,
                    createdReview.ReviewDate,
                    createdReview.Comments
                });
            }
           
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the review");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the review");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromForm] ReviewDtos review)
        {
            try
            {
                // Validate and normalize the date format
                if (!DateTime.TryParse(review.ReviewDate, out DateTime parsedDate))
                {
                    return BadRequest("Invalid date format. Please use 'yyyy-MM-dd'.");
                }

                // Convert the normalized date to DateOnly
                var reviewDate = DateOnly.FromDateTime(parsedDate);
                var updatedReview = await _sender.Send<int>(new UpdateReviewCommand(
                    id,
                    review.UserId,
                    review.BookId,
                    review.Rating,
                    reviewDate,
                    review.Comments));

                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the review");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the review");
            }
        }

        [HttpDelete("{id}")]
        public async Task<int> DeleteReview(int id)
        { 
            return await _sender.Send(new DeleteReviewCommand { Id = id });
        }

    }
}
