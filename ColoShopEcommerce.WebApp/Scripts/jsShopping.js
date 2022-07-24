$(document).ready(function () {
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault(); // turn off href
        var id = $(this).data('id');
        var quantity = 1;
        var Quantity = $('#quantity_value').text()
        if (Quantity != '') {
            quantity = parseInt(Quantity)
        }
        var sl = parseInt($('#quantity_value').text());
        alert(id + " " + quantity);
        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout_items').html(rs.Count);
                    alert(rs.msg);
                }
            }
        });
    });
});