var labels;
var lottoData;
var lottoChart;
window.onload = function () {
    var ctx = document.getElementById("lottoChart").getContext("2d");
    lottoChart = new Chart(ctx, {
        type: "line",
        data: {
            labels: labels,
            datasets: lottoData
        },
        options: {
            hover: {
                mode: 'index'
            },
            scales: {
                yAxes: [{
                    ticks: {
                        stepSize: 1,
                        min: 0,
                        autoSkip: false
                    }
                }]
            }
        }
    });
};
//# sourceMappingURL=LottoResults.js.map