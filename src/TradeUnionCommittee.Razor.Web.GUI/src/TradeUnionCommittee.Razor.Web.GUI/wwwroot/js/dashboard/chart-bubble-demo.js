// For a bubble chart

$('#bubbleChart').ready(() =>
{
    $.get("/Dashboard/BubbleData", (result) =>
    {
        var myBubbleChart = new Chart(document.getElementById("bubbleChart"), 
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
                           labelString: "Happiness"
                        }
                    }],
                    xAxes:
                    [{
                        scaleLabel:
                        {
                           display: true,
                           labelString: "GDP (PPP)"
                        }
                    }]
                }
            }
        });
    });
});