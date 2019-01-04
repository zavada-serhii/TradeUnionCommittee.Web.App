var ctxBc = document.getElementById('bubbleChart').getContext('2d');
  var bubbleChart = new Chart(ctxBc, {
    type: 'bubble',
    data: {
      datasets: [{
        label: 'John',
        data: [{
          x: 3,
          y: 7,
          r: 10
        }],
        backgroundColor: "#ff6384",
        hoverBackgroundColor: "#ff6384"
      }, {
        label: 'Peter',
        data: [{
          x: 5,
          y: 7,
          r: 10
        }],
        backgroundColor: "#44e4ee",
        hoverBackgroundColor: "#44e4ee"
      }, {
        label: 'Donald',
        data: [{
          x: 7,
          y: 7,
          r: 10
        }],
        backgroundColor: "#62088A",
        hoverBackgroundColor: "#62088A"
      }]
    }
  })