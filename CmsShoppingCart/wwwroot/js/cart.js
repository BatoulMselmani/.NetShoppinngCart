$(document).ready(function () {
    $("#add-to-cart-button").click(function () {
        var productId = $("#product-id").val();
        var productPrice = parseFloat($("#product-price").val());
        var discountCode = $("#discount-code-input").val().trim();

        // Send an AJAX request to the server to add the product to the cart
        $.ajax({
            type: "POST",
            url: "/Cart/Discount",
            data: {
                productId: productId,
                productPrice: productPrice,
                discountCode: discountCode
            },
            success: function (data) {
                if (data.success) {
                    alert(data.message);

                    // Update the product price on the page
                    var finalPrice = data.finalPrice;
                    $("#product-price").val(finalPrice.toFixed(2));
                } else {
                    alert("Failed to add product to cart.");
                }
            },
            error: function () {
                alert("An error occurred while adding the product to the cart.");
            }
        });
    });
});