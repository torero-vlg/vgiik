define([], function () {

    function setCollapseEvent(elementId, url) {
        $(elementId).on('show.bs.collapse', function (e) {

            if ($(e.target).html() === '') {
                $.ajax({
                    url: url,
                    beforeSend: function (xhr) {
                        $(e.target).html('<h3><i class="center fa fa-spinner fa-pulse fa-3x fa-fw"></i></h3>');
                    }
                })
                .success(function (data) {
                    $(e.target).html(data);
                });
            }
        });
    }

    return {
        Initialize: function () {

            setCollapseEvent('#personLink', '/Archive/PersonList');
            setCollapseEvent('#photoLink', '/Archive/PhotoList');
            setCollapseEvent('#departmentLink', '/Archive/DepartmentList');

        }
    }
});
