using System;
using System.Linq;
using System.Linq.Expressions;
using TrickingLibrary.Models;
using TrickingLibrary.Models.Moderation;

namespace TrickingLibrary.Api.ViewModels
{
    public class ReviewViewModel
    {
        public static readonly Func<Review, object> Create = Projection.Compile();

        public static Expression<Func<Review, object>> Projection =>
            review => new
            {
                review.Id,
                review.ModerationItemId,
                review.Comment,
                review.Status,
            };
    }
}