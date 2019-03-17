
var labels;
var lottoData: Array<LottoTypes.LottoResults>;
let lottoChart: Chart;



window.onload = () => {
    var ctx = (document.getElementById("lottoChart") as any).getContext("2d");
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
        }
    });
}