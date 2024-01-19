//In this javascript file the code is separated in multiple functions because not all of the .cshtml files use exactly the same code,
//it's a little bit different. (for example, we don't need DataTable in FindClosest, therefore the Delete function there is a little
//bit different)

function dataTablesAjax() {
    $(document).ready(function () {
        var table = $("#places_table").DataTable();
        $("#places_table").on("click", ".js_delete", function () {
            var button = $(this);
            bootbox.confirm("Do you want to delete this movie?", function (result) {
                if (result) {
                    $.ajax({
                        url: "/HistoricalCulturalObjects/Delete/" + button.attr("data-place"),
                        method: "GET",
                        success: function () {
                            table.row(button.parents("tr")).remove().draw();
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });
                }
            });
        });

        addToFavorite();
    });
}

function findClosestAjax() {
    $(document).ready(function () {
        $(".js_delete").on("click", function () {
            var button = $(this);
            bootbox.confirm("Do you want to delete this entry?", function (result) {
                if (result) {
                    $.ajax({
                        url: "/HistoricalCulturalObjects/Delete/" + button.attr("data-place"),
                        method: "GET",
                        success: function () {
                            button.closest("tr").remove();
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });
                }
            });
        });

        addToFavorite();
    });
}


function addToFavorite() {
    $("#places_table").on("click", ".favorite-button", function (e) {
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
}