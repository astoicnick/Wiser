console.log("Yup it's checked");
$(document).ready(function (a) {
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
    });
    $('.favoriteButton').each(function (i) {
        var target = $('.favoriteButton')[i];
        var id = $(target).attr('id');
        $.ajax({
            type: 'GET',
            url: `/api/checkfavorite/${id}`
        })
            .done(function (r) {
                if (r == true) {
                    console.log(r);
                    $(target).removeClass();
                    $(target).addClass("fa fa-star scrollIcon");
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
                    $.ajax({
                        type: 'DELETE',
                        url: `/api/rmfavorite/${id}`
                    })
                        .done(function (s) {
                            if (s == true) {
                                $(target).removeClass();
                                $(target).addClass("fa fa-star-o scrollIcon");
                                console.log(id);
                            }
});
                }
                else {
                    console.log(r);
                    $(target).removeClass();
                    $(target).addClass("fa fa-star scrollIcon");
                }
            })
            .fail(function (r) {
                alert("Failed to favorite post");
            });
    });
    $(".delete").click(function (e) {
        var target = $(".delete");
        var id = target.attr('id');
        $.ajax({
            type: 'DELETE',
            url: `/api/remove/${id}`
        })
            .done(function (r) {
                if (r == true) {
                    target.hide();
                }
                else {
                    alert("You are not the owner of this wisdom, henceforth you cannot remove it. Good day.");
                }
            });
    });
});