
// Task 1.2

$('#basicColumnChart-1-2').ready(function () {

    $('#basic-column-time-1-2').empty();
    $('#basic-column-container-1-2').empty();
    $('#basic-column-container-1-2').append('<div class="sbl-circ-dual"></div>');

    $.get("/Dashboard/CheckingSignificanceAgeTeacherAndTypeOfEvent", function (result) {

        $('#basic-column-container-1-2').empty();
        $('#basic-column-container-1-2').append('<div id="basicColumnChart-1-2"></div>');
        $('#basic-column-time-1-2').append(`Оновлено щойно - ${result.dateTime}`);

        Highcharts.chart('basicColumnChart-1-2', {
            chart: {
                type: 'column'
            },
            title: {
                text: 'Task 1.2'
            },
            xAxis: {
                categories: result.chart.categories,
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: ''
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: result.chart.series
        });
    });
});