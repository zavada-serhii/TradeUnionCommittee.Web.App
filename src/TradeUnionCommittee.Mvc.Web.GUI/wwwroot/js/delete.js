$((function () {
    var url;
    var redirectUrl;
    var target;
    var typeModal = {
        DELETE: 1,
        ERROR: 2
    };

    $("body").append(`
            <div class="modal" tabindex="-1" role="dialog" id="deleteModal">
                <div class="modal-dialog" role="document" id="typeModal">
                    <div class="modal-content">
                        <div class="modal-header">
                             <h5 class="modal-title" style="color: red; font-weight: bold; font-size: 20px;" id="headerModal"></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div style="white-space: pre-line" class="modal-body" id="body-modal">
    
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal" id="cancel-delete"></button>
                            <button type="button" class="btn btn-danger" id="confirm-delete">Видалити</button>
                        </div>
                    </div>
                </div>
            </div>
`);

    //Delete Action
    $(".delete").on("click", (e) => {
        e.preventDefault();

        target = e.target;
        var id = $(target).data("id");
        var controller = $(target).data("controller");
        var action = $(target).data("action");
        var bodyMessage = $(target).data("body-message");
        redirectUrl = $(target).data('redirect-url');

        url = `/${controller}/${action}/${id}`;
        switchModalType(typeModal.DELETE);
        changeTextModal(bodyMessage);
        $("#deleteModal").modal("show");
    });

    $("#confirm-delete").on("click", () => {
        $.ajax({
            url: url,
            type: 'DELETE',
            success: function(result) {
                if (result.isValid) {
                    if (redirectUrl) {
                        window.location.href = redirectUrl;
                    } else {
                        document.location.reload(true);
                    }
                } else {
                    showErrors(result.errorsList);
                }
            }
        });
    });

    function showErrors(errorsList) {

        let iterator = 1;
        let errorMessage = "";
        errorsList.forEach(error => {
            errorMessage += `${iterator}. ${error}\n`;
            iterator++;
        });

        switchModalType(typeModal.ERROR);
        changeTextModal(errorMessage);
    }

    function switchModalType (type) {
        switch (type) {
            case typeModal.DELETE:
                $("#typeModal").addClass("modal-dialog-centered");
                $("#headerModal").text("Видалення");
                $("#cancel-delete").text("Скасувати");
                $("#confirm-delete").show();
            break;
            case typeModal.ERROR:
                $("#typeModal").removeClass("modal-dialog-centered");
                $("#headerModal").text("Сталась помилка");
                $("#cancel-delete").text("Закрити");
                $("#confirm-delete").hide();
                break;
        }
    }

    function changeTextModal(text) {
        $("#body-modal").text(text);
    }

}()));