
// Task 1.2

$('#basicColumnChart').ready(function () {

    $('#basic-column-time').empty();
    $('#basic-column-container').empty();
    $('#basic-column-container').append('<div class="sbl-circ-dual"></div>');

    $.get("/Dashboard/CheckingSignificanceAgeTeacherAndTypeOfEvent", function (result) {

        $('#basic-column-container').empty();
        $('#basic-column-container').append('<div id="basicColumnChart"></div>');
        $('#basic-column-time').append(`Оновлено щойно - ${result.dateTime}`);

        Highcharts.chart('basicColumnChart', {
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