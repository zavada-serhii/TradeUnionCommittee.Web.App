// For a bubble chart

$('#clusterAnalysisClick').click(() =>
{
    $('#bubble-container').empty();
    $("#bubble-container").css("padding", "1.25rem");
    $('#bubble-container').append('<div class="sbl-circ-dual"></div>');

    var typeId = $("select#clusterAnalysisEvents option:checked").val();

    $.get(`/Dashboard/BubbleData/${typeId}`, (result) =>
    {
        $('#bubble-container').empty();
        $('#bubble-container').append('<canvas id="bubbleChart"></canvas>');

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