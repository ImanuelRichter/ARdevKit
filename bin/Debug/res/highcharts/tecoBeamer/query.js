function query(dataPath, plugin)
{
	console.log("Try to load data");
	$.getJSON(dataPath, function(data)
	{
		console.log("Response:");
		console.log("metric: " + data[0].metric);
		console.log("unit: " + data[0].tags.unit);
		console.log("resource_id: " + data[0].tags.resource_id);
		plugin.options.yAxis.title.text = data[0].metric + " in " + data[0].tags.unit;
		plugin.options.series[0].name = data[0].tags.resource_id;
		var values = new Array();
		var i = 0;
		var last = 0;
		$.each(data[0].dps, function(k, v) {
			var key = parseInt(k) + (2 * 60 * 60 * 1000);
			values[i] = [key, v];
			i++;
        });
		plugin.options.series[0].data = values;
		$('#' + plugin.id).highcharts(plugin.options);
	})
	.fail(function() { console.log("Failed to load data for " + plugin.id)})
	.done(function() { console.log("Loaded data for " + plugin.id + " successfully")});
	update("http://cumulus.teco.edu:4242/api/query?start=1m-ago&m=avg:1m-avg:energy%7Bresource_id=000D6F0000D34235%7D&ms=true", plugin);
};
function update(dataPath, plugin)
{
	$.getJSON(dataPath, function(data)
	{
		var chart = $('#' + plugin.id).highcharts();
		$.each(data[0].dps, function(k, v) {
			var key = parseInt(k) + (2 * 60 * 60 * 1000);
			var point = [key, v];
			console.log("added " + point);
			var series = chart.series[0],
            		shift = series.data.length > 24;
        		chart.series[0].addPoint(point, true, shift);
        	});
		if (plugin.visible)
			setTimeout(function() { update(dataPath, plugin); }, 1000 * 60);
	})
	.fail(function() { console.log("Failed to update data for " + plugin.id)})
	.done(function() { console.log("Updated data for " + plugin.id + " successfully")});
};