function update(dataPath, id)
{
	$.getJSON(dataPath, function(point)
	{
		var chart = $('#chart1').highcharts();
		var series = chart.series[0],
            shift = series.data.length > 0;
            chart.series[0].addPoint(point, true, shift);
		setTimeout(function() { update(dataPath, id); }, 1000);
	})
	.fail(function() { console.log("Failed to load data")})
	.done(function() { console.log("Loaded data successfully")});
}