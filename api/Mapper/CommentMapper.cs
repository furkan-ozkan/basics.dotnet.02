using api.Dtos.Comment;
using api.Models;

namespace api.Mapper
{
    public static class CommentMapper
    {
        public static CommentRequestDto ToCommentRequestDto(this Comment commentModel)
        {
            return new CommentRequestDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                CreatedBy = commentModel.AppUser.UserName,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreateDTO(this CommentCreateRequestDto commentCreateRequestDto, int stockId)
        {
            return new Comment
            {
                Title = commentCreateRequestDto.Title,
                Content = commentCreateRequestDto.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdateDTO(this CommentUpdateRequestDto updateCommentDto)
        {
            return new Comment
            {
                Title = updateCommentDto.Title,
                Content = updateCommentDto.Content
            };
        }
    }
}