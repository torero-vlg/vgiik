define(['jquery.fileupload'], function () {

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
             });

             var url = '/Upload/UploadFile';
             var folderId = $('#Path').val();

             $('#fileupload').fileupload({
                 url: url,
                 dataType: 'json',
                 paramName: folderId,
                 done: function (e, data) {
                     $.each(data.result, function (index, file) {
                         $('<div class="alert alert-success" role="alert"/>').text(file.name).appendTo('#files');
                     });
                 },
                 fail: function (e, data) {
                     $.each(data.files, function (index, file) {
                         $('<div class="alert alert-danger" role="alert"/>').text(file.name + ' ошибка').appendTo('#files');
                     });
                 },
                 progressall: function (e, data) {
                     var progress = parseInt(data.loaded / data.total * 100, 10);
                     $('#progress .progress-bar').css(
                     'width',
                     progress + '%'
                 );
                 }
             }).prop('disabled', !$.support.fileInput)
             .parent().addClass($.support.fileInput ? undefined : 'disabled');
        }
    }
});
