define(['lightbox'], function () {

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

            $('#veteranModal').on('show.bs.modal', function (event) {
                var button = $(event.relatedTarget); // Button that triggered the modal
                var id = button.data('veteranid'); // Extract info from data-* attributes

                $.ajax({
                    url: "/Museum/Veteran?id=" + id,
                    beforeSend: function (xhr) {
                        $("#modalBody").html('<h3><i class="center fa fa-spinner fa-pulse fa-3x fa-fw"></i></h3>');
                    },
                    cache: false
                })
                    .done(function (html) {
                        $("#modalBody").html(html);
                    });;
            });

            jQuery('a[rel*=lightbox_s]').lightBox();
        }
    }
});
