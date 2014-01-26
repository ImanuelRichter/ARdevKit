function query(dataPath, id, options)
{
	$.get(dataPath, function(xml)
	{
		var $xml = $(xml);
		$xml.find('categories item').each(function(i, category)
		{
			options.xAxis.categories.push($(category).text());
		});
		$xml.find('series').each(function(i, series)
		{
			var seriesOptions =
			{
				name: $(series).find('name').text(),
				color: {
					linearGradient: { x1: $(series).find('x1').text(), x2: $(series).find('x2').text(), y1: $(series).find('y1').text(), y1: $(series).find('y2').text() },
					stops: [
						[0, $(series).find('stops zero').text()],
						[1, $(series).find('stops one').text()]
					]
				},
				data: []
			};
			var points = $(series).find('points').text().split(',');
			$.each(points, function(i, point) {
				seriesOptions.data.push(parseInt(point));
			});
			options.series.push(seriesOptions);
		});
		return $('#' + id).highcharts(options);
	})
	.fail(function() { console.log("Failed to load data")})
	.done(function() { console.log("Loaded data successfully")});
};