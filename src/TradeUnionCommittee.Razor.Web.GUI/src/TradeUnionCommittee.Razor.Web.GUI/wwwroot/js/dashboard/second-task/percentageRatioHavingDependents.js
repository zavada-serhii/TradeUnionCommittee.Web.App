// Set new default font family and font color to mimic Bootstrap's default styling
// Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
// Chart.defaults.global.defaultFontColor = '#292b2c';

// Task 2.3

$('#pieChart-2-3').ready(function() {

    $('#pie-time-2-3').empty();
    $('#pie-container-2-3').empty();
    $('#pie-container-2-3').append('<div class="sbl-circ-dual"></div>');

    $.get("/Dashboard/PercentageRatioHavingDependents", function (result) {

        $('#pie-container-2-3').empty();
        $('#pie-container-2-3').append('<canvas id="pieChart-2-3" width="100" height="48"></canvas>');
        $('#pie-time-2-3').append(`Оновлено щойно - ${result.dateTime}`);
        
        var myChart = new Chart($('#pieChart-2-3'), {
            type: 'pie',
            data: {
                labels: result.chart.labels,
                datasets: [{
                    label: '# of Votes',
                    data: result.chart.data,
                    backgroundColor: palette('tol', result.chart.data.length).map(function (hex) {
                        return '#' + hex;
                    })
                }]
            }
        });
    });
});