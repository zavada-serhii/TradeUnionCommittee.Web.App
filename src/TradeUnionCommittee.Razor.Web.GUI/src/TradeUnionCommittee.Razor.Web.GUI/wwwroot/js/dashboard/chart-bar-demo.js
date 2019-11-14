// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#292b2c';


$('#myBarChart').ready(function () {

    $('#bar-container').empty();
    $('#bar-container').append('<div class="sbl-circ-dual"></div>');

    $.get("/Dashboard/BarData", function (result) {

        $('#bar-container').empty();
        $('#bar-container').append('<canvas id="myBarChart" width="100%" height="50"></canvas>');

        var myLineChart = new Chart(document.getElementById("myBarChart"), {
            type: 'bar',
            data: {
                labels: result.labels,
                datasets: [{
                    label: "Revenue",
                    backgroundColor: "rgba(2,117,216,1)",
                    borderColor: "rgba(2,117,216,1)",
                    data: result.data,
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
                            maxTicksLimit: result.data.length
                        }
                    }],
                    yAxes: [{
                        ticks: {
                            min: 0,
                            max: Math.max.apply(Math, result.data) + 50,
                            maxTicksLimit: result.data.length
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
