define([], function () {

    return {
        Initialize: function () {

            $('#photoLink').on('show.bs.collapse', function (e) {

                if ($(e.target).html() === '') {
                    $.ajax({
                        url: "/Archive/PhotoList"
                    })
                    .success(function (data) {
                        $(e.target).html(data);
                    });
                }
            });
        }
    }
});
