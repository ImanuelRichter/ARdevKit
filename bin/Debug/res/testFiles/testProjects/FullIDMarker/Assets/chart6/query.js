function query(dataPath, plugin)
{
	//Get a point from the server
	$.getJSON(dataPath, function(point)
	{
		var chart = $('#' + plugin.id).highcharts();
		var series = chart.series[0],
            shift = series.data.length > 24;
		//Add the point to the series and shift if the number of points is greater then 24
        chart.series[0].addPoint(point, true, shift);
		//Request data every second	
		setTimeout(function() { query(dataPath, plugin); }, 1000);
	})
	.fail(function() { console.log("Failed to update data")})
	.done(function() { console.log("Updated data successfully")});
}