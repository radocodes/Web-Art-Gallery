using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WAG.Services.Interfaces;
using System.Security.Claims;
using WAG.ViewModels.Comment;
using Newtonsoft.Json;

namespace WAG.WebApp.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService CommentService;
        private IUserAccountService UserAccountService;

        public CommentController(ICommentService commentService, IUserAccountService userAccountService)
        {
            this.CommentService = commentService;
            this.UserAccountService = userAccountService;
        }

        [HttpPost]
        public IActionResult AddComment(int articleId, string comment)
        {
            var currentUser = this.UserAccountService.GetCurrentUser(HttpContext);
            int commentId = 0;

            if (comment!= null)
            {
                commentId = this.CommentService.AddComment(currentUser.Id, articleId, comment);
            }
            

            var addCommentViewModel = new AddCommentViewModel()
            {
                Comment = comment,
                CommentId = commentId,
                UserFirstName = currentUser.FirstName,
                UserLastName = currentUser.LastName,
                UserName = currentUser.UserName
            };

            string json = JsonConvert.SerializeObject(addCommentViewModel);

            return Json(json);
        }

        [HttpPost]
        public IActionResult DeleteComment(int commentId)
        {
            var currUser = this.UserAccountService.GetCurrentUser(HttpContext);

            var comment = this.CommentService.GetCommentById(commentId);

            var commentOwnerId = comment.WAGUserId;

            var deleteCommentViewModel = new DeleteCommentViewModel();

            if (currUser.Id == commentOwnerId || User.IsInRole("Admin"))
            {
                this.CommentService.DeleteComment(commentId);

                deleteCommentViewModel.CommentId = commentId;
            }

            string json = JsonConvert.SerializeObject(deleteCommentViewModel);

            return Json(json);
        }
    }
}