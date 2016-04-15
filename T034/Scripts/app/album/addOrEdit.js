define([], function () {

    return {
        Initialize: function () {
         
            var trash = $(".fa-trash-o").click(function () {

                $.ajax({
                    url: "/Album/DeleteFile",
                    data: { filePath: $(this).closest("img").attr('src') }
                })
              .success(function (data) {
                  $(this).closest(".list-group-item").slideUp();
              });

            });;

            //@Url.Action("DeleteFile", "Album", new {filePath = filePath, albumId = Model.Id}, null)

        }
    }
});
