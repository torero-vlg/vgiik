define(['lightbox'], function () {

    return {
        Initialize: function () {

            jQuery('a[rel*=lightbox_s]').lightBox();

        }
    }
});
