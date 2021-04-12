using System;
using System.Collections.Generic;
using System.Linq;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Interfaces;

namespace WAG.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly WAGDbContext DbContext;

        public CommentService(WAGDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public int AddComment(string userId, int articleId, string commentContent)
        {
            var user = this.DbContext.Users.FirstOrDefault(u => u.Id == userId);

            var comment = new Comment()
            {
                WAGUserId = userId,
                ArticleId = articleId,
                TextBody = commentContent,
                Date = DateTime.UtcNow,
                WAGUser = user
            };

            this.DbContext.Comments.Add(comment);
            this.DbContext.SaveChanges();

            var commentId = this.DbContext.Comments.First(c => c == comment).Id;

            return commentId;
        }

        public void DeleteComment(int commentId)
        {
            var comment = this.DbContext.Comments.FirstOrDefault(c => c.Id == commentId);

            if (comment != null)
            {
                this.DbContext.Comments.Remove(comment);
                this.DbContext.SaveChanges();
            }
        }

        public List<Comment> GetArticleComments(int articleId)
        {
            var comments = this.DbContext.Comments.Where(c => c.ArticleId == articleId).ToList();

            foreach (var comment in comments)
            {
                var commentAuthor = this.DbContext.Users.FirstOrDefault(u => u.Id == comment.WAGUserId);

                if (commentAuthor != null)
                {
                    comment.WAGUser = new WAGUser()
                    {
                        UserName = commentAuthor.UserName,
                        FirstName = commentAuthor.FirstName,
                        LastName = commentAuthor.LastName
                    };
                }
            }

            return comments;
        }

        public Comment GetCommentById(int commentId)
        {
            var comment = this.DbContext.Comments.FirstOrDefault(c => c.Id == commentId);

            return comment;
        }
    }
}
