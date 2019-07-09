$((function () {
    var url;
    var target;

    $("body").append(`
                    <div class="modal" id="deleteModal" tabindex="-1" role="dialog">
                        <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                     <h5 class="modal-title" style="color: red; font-weight: bold; font-size: 20px;">Видалення</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body delete-modal-body">
    
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal" id="cancel-delete">Скасувати</button>
                                    <button type="button" class="btn btn-danger" id="confirm-delete">Видалити</button>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="modal" id="deleteErrorModal" tabindex="-1" role="dialog">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" style="color: red; font-weight: bold; font-size: 20px;">Сталась помилка</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div style="white-space: pre-line" class="modal-body delete-error-modal-body">
                
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">Закрити</button>
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

        url = `/${controller}/${action}/${id}`;
        $(".delete-modal-body").text(bodyMessage);
        $("#deleteModal").modal("show");
    });

    $("#confirm-delete").on("click", () => {
        $.ajax({
            url: url,
            type: 'DELETE',
            success: function (result) {
                if (result.isValid) {
                    document.location.reload(true);
                } else {
                    let iterator = 1;
                    let errorMessage = "";
                    result.errorsList.forEach(error => {
                        errorMessage += `${iterator}. ${error}\n`;
                        iterator++;
                    });
                    $(".delete-error-modal-body").text(errorMessage);
                    $("#deleteErrorModal").modal("show");
                }
            }
        }).always(() => {
            $("#deleteModal").modal("hide");
        });
    });
}()));