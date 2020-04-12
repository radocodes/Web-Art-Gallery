

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

//Winter snowflakes at background
let addSnowFlakes = () => {
    //(function () {
    //    let requestAnimationFrame = window.requestAnimationFrame || window.mozRequestAnimationFrame || window.webkitRequestAnimationFrame || window.msRequestAnimationFrame ||
    //        function (callback) {
    //            window.setTimeout(callback, 1000 / 60);
    //        };
    //    window.requestAnimationFrame = requestAnimationFrame;
    //})();


    let flakes = [],
        canvas = document.getElementById("canvas"),
        ctx = canvas.getContext("2d"),
        flakeCount = 400,
        mX = -100,
        mY = -100

    let body = document.body,
        html = document.documentElement;

    let height = Math.max(body.scrollHeight, body.offsetHeight,
        html.clientHeight, html.scrollHeight, html.offsetHeight);

    canvas.width = window.innerWidth;
    canvas.height = height; /*window.innerHeight;*/

    function snow() {
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        for (let i = 0; i < flakeCount; i++) {
            let flake = flakes[i],
                x = mX,
                y = mY,
                minDist = 150,
                x2 = flake.x,
                y2 = flake.y;

            let dist = Math.sqrt((x2 - x) * (x2 - x) + (y2 - y) * (y2 - y)),
                dx = x2 - x,
                dy = y2 - y;

            if (dist < minDist) {
                let force = minDist / (dist * dist),
                    xcomp = (x - x2) / dist,
                    ycomp = (y - y2) / dist,
                    deltaV = force / 2;

                flake.velX -= deltaV * xcomp;
                flake.velY -= deltaV * ycomp;

            } else {
                flake.velX *= .98;
                if (flake.velY <= flake.speed) {
                    flake.velY = flake.speed
                }
                flake.velX += Math.cos(flake.step += .05) * flake.stepSize;
            }

            ctx.fillStyle = "rgba(255,255,255," + flake.opacity + ")";
            flake.y += flake.velY;
            flake.x += flake.velX;

            if (flake.y >= canvas.height || flake.y <= 0) {
                reset(flake);
            }


            if (flake.x >= canvas.width || flake.x <= 0) {
                reset(flake);
            }

            ctx.beginPath();
            ctx.arc(flake.x, flake.y, flake.size, 0, Math.PI * 2);
            ctx.fill();
        }
        requestAnimationFrame(snow);
    };

    function reset(flake) {
        flake.x = Math.floor(Math.random() * canvas.width);
        flake.y = 0;
        flake.size = (Math.random() * 3) + 2;
        flake.speed = (Math.random() * 1) + 0.5;
        flake.velY = flake.speed;
        flake.velX = 0;
        flake.opacity = (Math.random() * 0.5) + 0.3;
    }

    function init() {
        for (let i = 0; i < flakeCount; i++) {
            let x = Math.floor(Math.random() * canvas.width),
                y = Math.floor(Math.random() * canvas.height),
                size = (Math.random() * 3) + 2,
                speed = (Math.random() * 1) + 0.5,
                opacity = (Math.random() * 0.5) + 0.3;

            flakes.push({
                speed: speed,
                velY: speed,
                velX: 0,
                x: x,
                y: y,
                size: size,
                stepSize: (Math.random()) / 30,
                step: 0,
                opacity: opacity
            });
        }

        snow();
    };

    canvas.addEventListener("mousemove", function (e) {
        mX = e.clientX,
            mY = e.clientY
    });

    window.addEventListener("resize", function () {
        let body = document.body,
            html = document.documentElement;

        let height = Math.max(body.scrollHeight, body.offsetHeight,
            html.clientHeight, html.scrollHeight, html.offsetHeight);

        canvas.width = window.innerWidth;
        canvas.height = height; /*window.innerHeight;*/
    })

    init();
}