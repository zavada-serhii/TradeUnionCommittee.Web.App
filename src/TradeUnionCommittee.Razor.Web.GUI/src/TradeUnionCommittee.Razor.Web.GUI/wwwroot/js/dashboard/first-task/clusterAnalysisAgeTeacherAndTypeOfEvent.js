// Task 1.3

$('#clusterAnalysisClick-1-3').click(() => {

    $('#bubble-time-1-3').empty();
    $('#bubble-container-1-3').empty();
    $("#bubble-container-1-3").css("padding", "1.25rem");
    $('#bubble-container-1-3').append('<div class="sbl-circ-dual"></div>');

    var typeId = $("select#clusterAnalysisEvents-1-3 option:checked").val();

    $.get(`/Dashboard/ClusterAnalysisAgeTeacherAndTypeOfEvent/${typeId}`, (result) => {
        $('#bubble-container-1-3').empty();
        $('#bubble-container-1-3').append('<canvas id="bubbleChart-1-3"></canvas>');
        $('#bubble-time-1-3').append(`Оновлено щойно - ${result.dateTime}`);

        var mybubbleChart = new Chart(document.getElementById('bubbleChart-1-3'),
            {
                type: 'bubble',
                data:
                {
                    labels: "Africa",
                    datasets: result.chart
                },
                options:
                {
                    title:
                    {
                        display: true
                    },
                    scales:
                    {
                        yAxes:
                        [{
                            scaleLabel:
                            {
                                display: true,
                                labelString: "Count event"
                            }
                        }],
                        xAxes:
                        [{
                            scaleLabel:
                            {
                                display: true,
                                labelString: "Age"
                            }
                        }]
                    }
                }
            });
    });
});