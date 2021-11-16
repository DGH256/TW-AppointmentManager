// First, checks if it isn't implemented yet.
if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

$(document).ready(function () {
    $(".btn-location-reload").on('click', function () {
        location.reload();
    });

    $(".btn-close-modal").on('click', function () {
        $(this).closest(".modal-dialog-window").modal('hide');
    });
});