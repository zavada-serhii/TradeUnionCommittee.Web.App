// For a bubble chart

$('#clusterAnalysisClick').click(() =>
{
    $('#bubble-chart-container').empty();
    $('#bubble-chart-container').append('<canvas id="bubbleChart"></canvas>');

    var typeId = $("select#clusterAnalysisEvents option:checked").val();

    $.get(`/Dashboard/BubbleData/${typeId}`, (result) =>
    {
        var myBubbleChart = new Chart(document.getElementById('bubbleChart'), 
        {
            type: 'bubble',
            data:
            {
                labels: "Africa",
                datasets: result
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