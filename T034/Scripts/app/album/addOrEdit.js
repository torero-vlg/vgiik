define([], function () {

    return {
        Initialize: function () {
         
             $(".fa-trash-o").click(function () {

                var filePath = $(this).parent().data('filepath');

                $.ajax({
                    url: "/Album/DeleteFile",
                    data: { filePath: filePath }
                })
                .success(function (data) {
                    $("[data-filepath='" + data.FilePath + "']").closest(".list-group-item").slideUp();
                });

                return false;
            });;
        }
    }
});
