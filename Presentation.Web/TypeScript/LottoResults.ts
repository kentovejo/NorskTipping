
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
                mode: "index"
            },
        }
    });
    txtBall1.SetText(lottoData[0].data[0]);
    txtBall2.SetText(lottoData[1].data[0]);
    txtBall3.SetText(lottoData[2].data[0]);
    txtBall4.SetText(lottoData[3].data[0]);
    txtBall5.SetText(lottoData[4].data[0]);
    txtBall6.SetText(lottoData[5].data[0]);
    txtBall7.SetText(lottoData[6].data[0]);
}