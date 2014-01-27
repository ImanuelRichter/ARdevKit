function query(dataPath, id, options)
{
	$.getJSON(dataPath, function(data)
	{
		var seriesOptions =
		{
			data: data
		};
		options.series.push(seriesOptions);
		return $('#' + id).highcharts(options);
	})
	.fail(function() { console.log("Failed to load data")})
	.done(function() { console.log("Loaded data successfully")});
};