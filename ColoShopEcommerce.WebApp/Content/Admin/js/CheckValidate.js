
$(document).ready(function () {
    //Khi bàn phím được nhấn và thả ra thì sẽ chạy phương thức này
    $("#addProduct").validate({
        rules: {
            imageProducts: "required"
        },
        messages: {
            imageProducts: "Vui Lòng Chọn Ảnh"

        }
    });
});