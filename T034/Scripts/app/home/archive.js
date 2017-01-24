﻿define([], function () {

    function setCollapseEvent(elementId, url) {
        $(elementId).on('show.bs.collapse', function (e) {

            if ($(e.target).html() === '') {
                $.ajax({
                    url: url
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
