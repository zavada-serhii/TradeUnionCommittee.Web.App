$('#formGroup').ready(function () {

    var now = new Date();
    var nowPlusSevenDays = new Date(now.getTime()).setDate(now.getDate() + 7);

    document.getElementById('startDate').valueAsDate = now;
    document.getElementById('endDate').valueAsDate = new Date(nowPlusSevenDays);
});

$('#getJournalDataClick').click(() => {

    $('#journal-content').empty();
    $('#journal-content').append('<hr> <div class="sbl-circ-dual"></div> <hr>');

    var viewModel = {
        email: $("select#email option:checked").val(),
        startDate: $('#startDate').val(),
        endDate: $('#endDate').val()
    };

    $.post('/Journal/Filter/', viewModel)
        .done(function (result) {

            if (result.length === 0) {
                showModalDialog(['Дані відсутні.']);
                 $('#journal-content').empty();
            }
            else {
                $('#journal-content').empty();
                $('#journal-content').append(`<div class="row main-block">
                                                <table class="table text-left">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                Email
                                                            </th>
                                                            <th>
                                                                Дата та час
                                                            </th>
                                                            <th>
                                                                Операція
                                                            </th>
                                                            <th>
                                                                Дані
                                                            </th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                          ${getHtmlData(result)}
                                                    </tbody>
                                                </table>
                                              </div>`);
            }
        })
        .fail(function (xhr, textStatus, errorThrown) {
            showModalDialog(xhr.responseJSON);
            $('#journal-content').empty();
        });
});

function getHtmlData(data) {

    var result = '';

    data.forEach(function (item) {
        result += `<tr>
                       <td>
                          ${item.emailUser}
                       </td>
                       <td>
                           ${item.date}
                       </td>
                       <td>
                          ${item.operation}
                       </td>
                       <td>
                          ${item.tables}
                       </td>
                   </tr>`;
    });

    return result;
}