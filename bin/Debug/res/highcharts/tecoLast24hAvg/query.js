function query(dataPath, plugin)
{
	console.log("Try to load data");
	$.getJSON(dataPath, function(data)
	{
		console.log("Response:");
		console.log("metric: " + data[0].metric);
		console.log("unit: " + data[0].unit);
		console.log("resource_id: " + data[0].tags.resource_id);
		plugin.options.yAxis.title.text = data[0].metric + " in " + data[0].tags.unit;
		plugin.options.series[0].name = data[0].tags.resource_id;
		plugin.options.series[0].data = data[0].dps;
		$('#' + plugin.id).highcharts(plugin.options);
	})
	.fail(function() { console.log("Failed to load data for " + plugin.id)})
	.done(function() { console.log("Loaded data for " + plugin.id + " successfully")});
	//update(dataPath, plugin);
};
function update(dataPath, plugin)
{
	$.getJSON(dataPath, function(data)
	{
		var chart = $('#' + plugin.id).highcharts();
		var point = data[0].dps;
		var series = chart.series[0],
            shift = series.data.length > 24;
        chart.series[0].addPoint(point, true, shift);
		if (plugin.visible)
			setTimeout(function() { update(dataPath, plugin); }, 1000);
	})
	.fail(function() { console.log("Failed to load data")})
	.done(function() { console.log("Loaded data successfully")});
};