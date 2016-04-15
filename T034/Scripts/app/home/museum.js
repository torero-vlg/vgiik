define([], function () {

    return {
        Initialize: function () {

            $('#personModal').on('show.bs.modal', function (event) {
                var button = $(event.relatedTarget); // Button that triggered the modal
                var personid = button.data('personid'); // Extract info from data-* attributes


                $.ajax({
                    url: "/Archive/Person?personId=" + personid,
                    cache: false
                })
                .done(function (html) {
                    $("#modalBody").html(html);
                });;
            });

            $('#bookModal').on('show.bs.modal', function (event) {
                var button = $(event.relatedTarget); // Button that triggered the modal
                var id = button.data('id'); // Extract info from data-* attributes

                $.ajax({
                    url: "/Museum/Publication?id=" + id,
                    cache: false
                })
                    .done(function (html) {
                        $("#modalBody").html(html);
                    });;
            });

        }
    }
});
