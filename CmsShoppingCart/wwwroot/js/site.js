// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    if ($("a.confirmDeletion").length) {
        $("a.confirmDeletion").click(() => {
            if (!confirm("Confirm deletion")) return false;
        });
    }

    if ($("div.alert.notification").length) {
        setTimeout(() => {
            $("div.alert.notification").fadeOut();

        }, 2000);
    }
});

function readURL(input) {
    if (input.files && input.files[0]) {
        let reader = new FileReader();

        reader.onload = function (e) {
            $("img#imgpreview").attr("src", e.target.result).width(200).height(200);
        };
        reader.readAsDataURL(input.files[0]);
    }


};





/*$(document).ready(function () {
    $("#search-button").on('click', function () {
        var searchTerm = $("#search-input").val();

        $.ajax({
            url: '/Products/Search', 
            type: 'GET',
            data: { searchTerm: searchTerm },
            success: function (result) {
                $("#search-results").html(result);
            },
            error: function () {
                // Handle error
            }
        });
    });
});*/

/*document.getElementById('search-button').addEventListener('click', function () {

    const searchTerm = document.getElementById('search-input').value;
});*/


// Perform your search logic here
        // You can manipulate the DOM to display search results