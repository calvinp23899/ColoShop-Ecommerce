$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault(); // turn off href
        var id = $(this).data('id');
        var quantity = 1;
        var Quantity = $('#quantity_value').text()
        if (Quantity != '') {
            quantity = parseInt(Quantity)
        }
        var sl = parseInt($('#quantity_value').text());
        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout_items').html(rs.Count);
                }
            }
        });
    });

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var cofirmMsg = confirm('Bạn có muốn xóa sản phẩm này khỏi giỏ hàng không ?')
        if (cofirmMsg == true) {
            $.ajax({
                url: '/shoppingcart/DeleteCartItem',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $('#checkout_items').html(rs.Count);
                        $('#trow_' + id).remove();
                    }
                }
            });
        }     
    });
});

function ShowCount() {
    $.ajax({
        url: '/shoppingcart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#checkout_items').html(rs.Count);
        }
    });
}