$(document).ready(function () {
    $(".btn-action-edit").click(function () {
        $(this).hide();
        var parentForm = $(this).parent().parent();
        parentForm.find(".table-input.editable").prop('readonly', false);
        parentForm.find(".btn-action-save").removeClass("hidden");
        parentForm.find(".multiselect").removeClass("disabled").prop("disabled", false);
    });

    $(".btn-action-save").click(function () {
        $(".btn-action-edit").show();
        var parentForm = $(this).parent().parent();
        parentForm.find(".table-input.editable").prop('readonly', true);
        parentForm.find(".multiselect").addClass("disabled").prop("disabled", true);
        $(this).addClass("hidden");
    });

    $(".btn-action-delete").click(function () {

        if (confirm("Are you sure you want to delete this item?")) {
            var parentForm = $(this).parent().parent();
            var methodUrl = $(this).attr("methodUrl");
            var methodId = $(this).attr("methodId");

            $.ajax({
                url: methodUrl,
                type: 'POST',
                data: $.param({ id: methodId })
            });

            parentForm.remove();
        }
    });

    $('.multiselect-element').multiselect();
    $(".multiselect-selected-text").on('DOMSubtreeModified', function () {
        var textValue = $(this).text().trim();

        if (textValue.length > 0) {
            var tdParent = $(this).closest("td");
            tdParent.find(".multiselect-hidden-input").val(textValue);
        }
    });
});