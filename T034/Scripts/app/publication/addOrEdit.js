define(['layout'], function (layout) {

    function addOrEdit() {
        $.ajax({
            url: '/Publication/AddOrEdit',
            method: 'POST',
            data: {
                Id: $('#Id').val()
            }
        })
        .complete(function (data) {
            layout.showResult(data);
        });
    }

    return {
        Initialize: function () {

            $("#btnAddOrEdit").click(function () {
                addOrEdit();
            });

        }
    }
});