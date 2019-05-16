using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WAG.Data;
using WAG.Data.Models;
using WAG.Services.Interfaces;

namespace WAG.Services
{
    public class CommentService : ICommentService
    {
        private WAGDbContext DbContext;

        public CommentService(WAGDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public int AddComment(string userId, int articleId, string commentContent)
        {
            var user = this.DbContext.Users.First(u => u.Id == userId);

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
                comment.WAGUser = new WAGUser()
                {
                    UserName = this.DbContext.Users.First(u => u.Id == comment.WAGUserId).UserName,
                    FirstName = this.DbContext.Users.First(u => u.Id == comment.WAGUserId).FirstName,
                    LastName = this.DbContext.Users.First(u => u.Id == comment.WAGUserId).LastName
                };
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
