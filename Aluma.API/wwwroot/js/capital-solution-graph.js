google.charts.load("current", { packages: ["corechart"] });
google.charts.setOnLoadCallback(drawCapitalSolutionChart);
function drawCapitalSolutionChart() {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Capital');
    data.addColumn('number', 'Amount');

    // add annotation column role
    data.addColumn({ type: 'string', role: 'annotation' });

    data.addRows([
        ['Capitalized Income shortfall', [Capitalized-Income-shortfall], '[Capitalized-Income-shortfall]'],
        ['Lump sum Needs', [Lump-sum-Needs], '[Lump-sum-Needs]'],
        ['Available Lump sums', [Available-Lump-sums], '[Available-Lump-sums]'],
        ['Total Lump sum Shortfall', [Total-Lump-sum-Shortfall], '[Total-Lump-sum-Shortfall]']
    ]);

    var options = {
        title: 'Capital solution'
    };

    var chart = new google.visualization.LineChart(document.getElementById('capitalSolutionChart'));
    chart.draw(data, options);
}