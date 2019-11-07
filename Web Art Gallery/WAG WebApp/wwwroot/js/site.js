// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//URL parsing from string to anchor-element at article content
let parseUrl = () => {

    let articleContent = $("#blog-details-article-content");
    let expression = /(\b(https?|ftp|file):\/\/[-A-Z0-9+&@@#\/%?=~_|!:,.;]*[-A-Z0-9+&@@#\/%=~_|])/ig;
    let urlMatch = articleContent.text().match(expression);

    if (urlMatch != null) {
        let urlMatchStartIndex = articleContent.text().indexOf(urlMatch);
        let articlePartBeforeUrl = articleContent.text().substring(0, urlMatchStartIndex);
        let urlAsText = articleContent.text().substring(urlMatchStartIndex, (urlMatchStartIndex + urlMatch.length));
        let articlePartAfterUrl = articleContent.text().substring((urlMatchStartIndex + urlMatch.toString().length + 1), (articleContent.text().length));

        articleContent.text("");
        articleContent.append(articlePartBeforeUrl);
        articleContent.append(`<a href ="${urlMatch}">Щракни тук</a >`);
        articleContent.append(articlePartAfterUrl);
    }
}

//Add Comment
let addComment = () => {
    $("#post").on("click", () => {
        let comment = $("#your-comment-input").val();
        $("#your-comment-input").val("");
        $.ajax({
            type: "POST",
            url: "/Comment/AddComment",
            dataType: 'Json',
            data: {
                comment: comment,
                articleId: currArticleId /*@Model.Article.Id*/,
            },
            success: data => {
                let json = JSON.parse(data);
                let userName = "";

                if (json.UserFirstName != null && json.UserLastName != null) {
                    userName = `${json.UserFirstName} ${json.UserLastName}`
                }
                else {
                    userName = json.UserName;
                }
                if (json.Comment != null) {
                    $("#all-comments").append(`<div id="comment-id-${json.CommentId}"><p class="comment" style="display:inline-block">${userName}: ${json.Comment} <button type="button" value="${json.CommentId}" class="close" aria-label="Close"><span aria-hidden="true">&times;</span></button></p></div>`);
                }

            }
        });
    });
}

//Delete Comment
let deleteComment = () => {
    $(".close").click(function () {
        let commentId = $(this).val();
        $.ajax({
            type: "POST",
            url: "/Comment/DeleteComment",
            dataType: 'Json',
            data: {
                commentId: commentId
            },
            success: data => {
                let json = JSON.parse(data);

                if (json != null) {
                    if (json.CommentId != null && json.CommentId != 0) {
                        $(`#comment-id-${json.CommentId}`).hide();
                    }
                }
            }
        });
    });
}