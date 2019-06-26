// For a bubble chart

var myBubbleChart = new Chart(document.getElementById("bubbleChart"), {
    type: 'bubble',
    data: {
        labels: "Africa",
        datasets: [
            {
                label: ["China"],
                backgroundColor: "rgba(255,221,50,0.2)",
                borderColor: "rgba(255,221,50,1)",
                data: [{
                    x: 6,
                    y: 4.994,
                    r: 5
                }]
            },
            {
                label: ["Denmark"],
                backgroundColor: "rgba(60,186,159,0.2)",
                borderColor: "rgba(60,186,159,1)",
                data: [{
                    x: 7,
                    y: 5.994,
                    r: 5
                }]
            }, {
                label: ["Germany"],
                backgroundColor: "rgba(0,0,0,0.2)",
                borderColor: "#000",
                data: [{
                    x: 8,
                    y: 6.994,
                    r: 5
                }]
            }, {
                label: ["Japan"],
                backgroundColor: "rgba(193,46,12,0.2)",
                borderColor: "rgba(193,46,12,1)",
                data: [{
                    x: 9,
                    y: 7.994,
                    r: 5
                }]
            }
        ]
    },
    options: {
        title: {
            display: true,
        }, scales: {
            yAxes: [{
                scaleLabel: {
                    display: true,
                    labelString: "Happiness"
                }
            }],
            xAxes: [{
                scaleLabel: {
                    display: true,
                    labelString: "GDP (PPP)"
                }
            }]
        }
    }
});