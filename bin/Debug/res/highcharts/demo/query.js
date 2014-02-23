var count = 1;
function query(dataPath, id, options)
{
	$.getJSON(dataPath, function(data)
	{
		options.yAxis.title.text = data[0].metric + " in " + data[0].tags.unit;
		options.series[0].name = data[0].tags.resource_id;
		var values = new Array();
		var i = 0;
		var last = 0;
		$.each(data[0].dps, function(k, v) {
			var key = parseInt(k);
			// Show just every hour
			if (key - last > 3600 * 1000)
			{
				values[i] = [key, v];
				i++;
				last = key;
			}
        });
		options.series[0].data = values;
		$('#' + id).highcharts(options);
	})
	.fail(function() { console.log("Failed to load data")})
	.done(function() { console.log("Loaded data successfully")});
	update(dataPath, id);
};
function update(dataPath, id)
{
	$.getJSON(dataPath, function(data)
	{
		var chart = $('#' + id).highcharts();
		var point = [1392742877000 + count * 3600 * 1000, 21.42];
		count++;
		var series = chart.series[0],
            shift = series.data.length > 24;
        chart.series[0].addPoint(point, true, shift);
		setTimeout(function() { update(dataPath, id); }, 1000);
	})
	.fail(function() { console.log("Failed to load data")})
	.done(function() { console.log("Loaded data successfully")});
};