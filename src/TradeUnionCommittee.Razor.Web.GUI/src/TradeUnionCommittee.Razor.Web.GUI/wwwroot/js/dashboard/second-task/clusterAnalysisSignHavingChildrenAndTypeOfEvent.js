// Task 2.2

$('#clusterAnalysisClick-2-2').click(() => {

    $('#bubble-time-2-2').empty();
    $('#bubble-container-2-2').empty();
    $("#bubble-container-2-2").css("padding", "1.25rem");
    $('#bubble-container-2-2').append('<div class="sbl-circ-dual"></div>');

    var typeId = $("select#clusterAnalysisEvents-2-2 option:checked").val();

    $.get(`/Dashboard/ClusterAnalysisSignHavingChildrenAndTypeOfEvent/${typeId}`, (result) => {
        $('#bubble-container-2-2').empty();
        $('#bubble-container-2-2').append('<canvas id="bubbleChart-2-2"></canvas>');
        $('#bubble-time-2-2').append(`Оновлено щойно - ${result.dateTime}`);

        var mybubbleChart = new Chart(document.getElementById('bubbleChart-2-2'),
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