
$(document).ready(function () {
    $("#btnGenerate").click(function () {
        var id = $(this).data('id');
        $.ajax({
            url: '/in-hoa-don-bill',
            type: 'POST',
            data: { id: id},
            success: function () {
                window.open('https://localhost:44301/Reports/ReportViewer.aspx', '_newtab');
            }
        });
    });
});

