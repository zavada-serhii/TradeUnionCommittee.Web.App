// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';

// Task 2.1

$('#task-click-2-1').click(() => {

    $('#bar-time-2-1').empty();
    $('#bar-container-2-1').empty();
    $("#bar-container-2-1").css("padding", "1.25rem");
    $('#bar-container-2-1').append('<div class="sbl-circ-dual"></div>');

    var typeId = $("select#task-events-2-1 option:checked").val();

    $.get(`/Dashboard/MultiCorrelationBetweenTypeOfEventAndDependents/${typeId}`, function (result) {

        $('#bar-container-2-1').empty();
        $('#bar-container-2-1').append('<canvas id="bar-chart-2-1" width="100%" height="50"></canvas>');
        $('#bar-time-2-1').append(`Оновлено щойно - ${result.dateTime}`);

        var myLineChart = new Chart(document.getElementById("bar-chart-2-1"), {
            type: 'bar',
            data: {
                labels: result.chart.labels,
                datasets: [{
                    label: "Revenue",
                    backgroundColor: "rgba(2,117,216,1)",
                    borderColor: "rgba(2,117,216,1)",
                    data: result.chart.data,
                }],
            },
            options: {
                scales: {
                    xAxes: [{
                        time: {
                            unit: 'month'
                        },
                        gridLines: {
                            display: false
                        },
                        ticks: {
                            maxTicksLimit: result.chart.data.length
                        }
                    }],
                    yAxes: [{
                        ticks: {
                            min: 0,
                            max: Math.max.apply(Math, result.chart.data) + 0.25,
                            maxTicksLimit: result.chart.data.length
                        },
                        gridLines: {
                            display: true
                        }
                    }],
                },
                legend: {
                    display: false
                }
            }
        });
    });
});
