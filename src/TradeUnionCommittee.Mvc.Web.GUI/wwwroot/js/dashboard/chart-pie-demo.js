// Set new default font family and font color to mimic Bootstrap's default styling
// Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
// Chart.defaults.global.defaultFontColor = '#292b2c';

// Pie Chart Example

$('#myPieChart').ready(function() {

    $.get("/Dashboard/PieData", function (result) {
        var myChart = new Chart(document.getElementById("myPieChart"), {
            type: 'pie',
            data: {
                labels: result.labels,
                datasets: [{
                    label: '# of Votes',
                    data: result.data,
                    backgroundColor: palette('tol', result.data.length).map(function (hex) {
                        return '#' + hex;
                    })
                }]
            }
        });
    });
});