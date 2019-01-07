$('#lineChart').ready(function () {
    $.get("/Dashboard/LineData", function (result) {
        var myRadarChart = new Chart(document.getElementById("lineChart").getContext('2d'), {
            type: 'line',
            data: {
                labels: result.labels,
                datasets: [
                    {
                        label: result.dataSets[0].label,
                        data: result.dataSets[0].data,
                        backgroundColor: [
                            'rgba(105, 0, 132, .2)',
                        ],
                        borderColor: [
                            'rgba(200, 99, 132, .7)',
                        ],
                        borderWidth: 2
                    },
                    {
                        label: result.dataSets[1].label,
                        data: result.dataSets[1].data,
                        backgroundColor: [
                            'rgba(0, 250, 220, .2)',
                        ],
                        borderColor: [
                            'rgba(0, 213, 132, .7)',
                        ],
                        borderWidth: 2
                    },
                    {
                        label: result.dataSets[2].label,
                        data: result.dataSets[2].data,
                        backgroundColor: [
                            'rgba(0, 137, 132, .2)',
                        ],
                        borderColor: [
                            'rgba(0, 10, 130, .7)',
                        ],
                        borderWidth: 2
                    }
                ]
            },
            options: {
                responsive: true
            }
        });
    });
});