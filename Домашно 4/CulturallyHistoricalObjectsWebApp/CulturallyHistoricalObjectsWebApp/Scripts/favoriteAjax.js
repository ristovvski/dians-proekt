$(document).ready(function () {

    $(".favorite-button").click(function (e) {

        e.preventDefault();
        var $form = $(this).closest('form');

        $.ajax({
            url: $form.attr('action'),
            type: 'POST',
            data: $form.serialize(),
            success: function () {
                var $button = $form.find('.favorite-button');
                if ($button.text() === "Favorite") {
                    $button.text("Remove");
                    $button.removeClass("yellow-button").addClass("red-button");
                    $form.attr('action', '/Account/RemoveFromFavorites')
                } else {
                    $button.text("Favorite");
                    $button.removeClass("red-button").addClass("yellow-button");
                    $form.attr('action', '/Account/AddToFavorite')
                }

            },
            error: function (jqXHR) {
                // Show alert message with the error message from the JSON response
                var errorMessage = JSON.parse(jqXHR.responseText).message;
                console.log(errorMessage);
            }
        });
    });
});
