using Xunit;
using WAG.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WAG.Data.Models;
using WAG.Services.Implementation;

namespace WAG.Services.Tests
{
    public class CommentServiceTests
    {
        [Fact]
        public void AddCommentShouldAddsCommentAndReturnCommentIdCorrect()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Comment_Db")
                .Options;

            var dbContext = new WAGDbContext(options);
            var service = new CommentService(dbContext);

            var userId = Guid.NewGuid().ToString();
            var articleId = 5;
            var commentContent = "Test Comment Text";

            // Act
            var commentId = service.AddComment(userId, articleId, commentContent);

            var addedComment = dbContext
                .Comments
                .FirstOrDefault(comment => comment.Id == commentId);

            // Assert
            Assert.NotNull(addedComment);
            Assert.Equal(userId, addedComment.WAGUserId);
            Assert.Equal(articleId, addedComment.ArticleId);
            Assert.Equal(commentContent, addedComment.TextBody);
        }

        [Fact]
        public void DeleteCommentShouldDeleteCommentCorrect()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Comment_Db")
                .Options;

            var dbContext = new WAGDbContext(options);
            var service = new CommentService(dbContext);

            var comment = new Comment()
            {
                WAGUserId = Guid.NewGuid().ToString(),
                ArticleId = 3,
                TextBody = "Delete Test Comment Text"
            };

            dbContext.Comments.Add(comment);
            dbContext.SaveChanges();

            var storedCommentId = dbContext.Comments.LastOrDefault().Id;

            // Act
            service.DeleteComment(storedCommentId);

            // Assert
            Assert.False(dbContext.Comments.Contains<Comment>(comment));
        }

        [Fact]
        public void GetArticleCommentsShouldReturnsCommentsOrEmptyList()
        {
            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Article_Comments_Db")
                .Options;

            var dbContext = new WAGDbContext(options);
            var service = new CommentService(dbContext);

            var numberOfComments = 5;
            var articleId = 3;

            for (int i = 0; i < numberOfComments; i++)
            {
                var comment = new Comment()
                {
                    WAGUserId = Guid.NewGuid().ToString(),
                    ArticleId = articleId,
                    TextBody = "Delete Test Comment Text",
                };

                dbContext.Comments.Add(comment);
            }
            
            dbContext.SaveChanges();

            var articleCommentsList = service.GetArticleComments(articleId);

            Assert.Equal(numberOfComments, articleCommentsList.Count);

            foreach (var comment in articleCommentsList)
            {
                dbContext.Comments.Remove(comment);
            }

            dbContext.SaveChanges();

            articleCommentsList = service.GetArticleComments(articleId);

            Assert.Empty(articleCommentsList);
        }

        [Fact]
        public void GetCommentByIdShouldReturnsCommentByIdCorrect()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<WAGDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Comments_ById_Db")
                .Options;

            var dbContext = new WAGDbContext(options);
            var service = new CommentService(dbContext);

            var commentSeed = new Comment()
            {
                TextBody = "Unique Test Text",
            };

            dbContext.Comments.Add(commentSeed);
            dbContext.SaveChanges();

            var commentId = dbContext.Comments.LastOrDefault().Id;

            // Act
            var comment = service.GetCommentById(commentId);

            // Assert
            Assert.NotNull(comment);
            Assert.Equal(commentSeed.TextBody, comment.TextBody);
        }
    }
}
