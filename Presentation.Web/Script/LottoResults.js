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
        options: {}
    });
};
//# sourceMappingURL=LottoResults.js.map