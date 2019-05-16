using System;
using System.Collections.Generic;
using System.Text;
using WAG.Data.Models;

namespace WAG.Services.Interfaces
{
    public interface ICommentService
    {
        int AddComment(string userId, int articleId, string commentContent);

        void DeleteComment(int commentId);

        List<Comment> GetArticleComments(int articleId);

        Comment GetCommentById(int commentId);
    }
}
