console.log("Yup it's checked");
$(document).ready(function (a) {
        //$(".scrollContainer").click(function () {
        //    console.log("yea");
        //    var id = $(".scrollModal").attr('id');
        //    var target = $(`.scrollModal#${id}`);
        //    console.log(target);
        //    target.removeClass();
        //    target.addClass("scrollModal modal fade show");
        //    target.css("display", "block");
        //});
    $('.upvote').each(function (i) {
        id = i;
        $.ajax({
            type: 'GET',
            url: `/api/checkupvote/${id}`
        })
            .done(function (r) {
                if (r == true) {
                    $(`#${i}.upvote`).addClass("fa fa-thumbs-up scrollIcon");
                }
                if (r == false) {

                }
            })
    })
    $('.favoriteButton').each(function (i) {
        id = i;
        $.ajax({
            type: 'GET',
            url: `/api/checkfavorite/${id}`
        })
            .done(function (r) {
                if (r == true) {
                    target = $(`#${i}.favoriteButton`);
                    target.removeClass();
                    target.addClass("fa fa-heart scrollIcon");
                }
            });
    });
    $(".upvote").click(function (e) {
        var target = $(e.target);
        var id = target.attr('id');
        $.ajax({
            type: 'PUT',
            url: `/api/upvote/${id}`
        })
            .done(function (r) {
                if (r == false) {

                    console.log(r);
                    target.removeClass();
                    target.addClass("fa fa-thumbs-up scrollIcon");
                }
                else {

                    console.log(r);
                    target.removeClass();
                    target.addClass("fa fa-thumbs-up scrollIcon");
                }
            })
            .fail(function (r) {
                alert("Failed to upvote post");
            });
    });
    $(".favoriteButton").click(function (e) {
        var target = $(e.target);
        var id = target.attr('id');
        $.ajax({
            type: 'POST',
            url: `/api/favorite/${id}`
        })
            .done(function (r) {
                if (r == false) {
                    console.log(r);
                    target.removeClass();
                    target.addClass("fa fa-heart scrollIcon");
                }
                else {
                    console.log(r);
                    target.removeClass();
                    target.addClass("fa fa-heart scrollIcon");
                }
            })
            .fail(function (r) {
                alert("Failed to favorite post");
            });
    });
});