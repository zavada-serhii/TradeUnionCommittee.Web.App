﻿
// Task 1.1

$('#heatmapChart-1-1').ready(function () {

    $('#heat-map-time-1-1').empty();
    $('#heat-map-container-1-1').empty();
    $('#heat-map-container-1-1').append('<div class="sbl-circ-dual"></div>');

    $.get("/Dashboard/CorrelationAnalysisBetweenTeacherAgeAndTypeOfEvent", function (result) {

        $('#heat-map-container-1-1').empty();
        $('#heat-map-container-1-1').append('<div id="heatmapChart-1-1"></div>');
        $('#heat-map-time-1-1').append(`Оновлено щойно - ${result.dateTime}`);

        Highcharts.chart('heatmapChart-1-1', {

            chart: {
                type: 'heatmap',
                marginTop: 40,
                marginBottom: 80,
                plotBorderWidth: 1
            },


            title: {
                text: 'Task 1.1'
            },

            xAxis: {
                categories: ['Age', 'Travel count', 'Wellness count', 'Tour count']
            },

            yAxis: {
                categories: ['Age', 'Travel count', 'Wellness count', 'Tour count'],
                title: null,
                reversed: true
            },

            colorAxis: {
                min: 0,
                minColor: '#FFFFFF',
                maxColor: Highcharts.getOptions().colors[0]
            },

            legend: {
                align: 'right',
                layout: 'vertical',
                margin: 0,
                verticalAlign: 'top',
                y: 25,
                symbolHeight: 280
            },

            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.xAxis.categories[this.point.x] + '</b> sold <br><b>' +
                        this.point.value + '</b> items on <br><b>' + this.series.yAxis.categories[this.point.y] + '</b>';
                }
            },

            series: [{
                name: 'Sales per employee',
                borderWidth: 1,
                data: result.chart,
                dataLabels: {
                    enabled: true,
                    color: '#000000'
                }
            }],

            responsive: {
                rules: [{
                    condition: {
                        maxWidth: 500
                    },
                    chartOptions: {
                        yAxis: {
                            labels: {
                                formatter: function () {
                                    return this.value.charAt(0);
                                }
                            }
                        }
                    }
                }]
            }

        });
    });
});