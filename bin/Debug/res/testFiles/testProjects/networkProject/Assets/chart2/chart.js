arel.Plugin.Chart2 = 
{
	visible : false,
	id : "chart2",
	coordinateSystemID : 1,
	options : {},
	translation : new arel.Vector3D(153.0,-5.0,0.0),
	div : document.createElement("div"),
	create : function()
	{
		this.div.setAttribute("id", this.id);
		this.div.style.position = "absolute";
		this.div.style.width = "200px";
		this.div.style.height = "200px";
		document.documentElement.appendChild(this.div);
		$.getScript("Assets/chart2/options.js", function()
		{
			arel.Plugin.Chart2.options = init();
			$('#' + arel.Plugin.Chart2.id).highcharts(arel.Plugin.Chart2.options);
		})
		.fail(function() { console.log("Failed to load options for chart2")})
		.done(function() { console.log("Loaded options for chart2 successfully")});
		$.getScript("Assets/chart2/query.js", function()
		{
			var dataPath = "http://cumulus.teco.edu:4242/api/query?start=1392741669001&m=avg:temperature%7bresource_id=Heater_Living%7d&ms=true";
			query(dataPath, arel.Plugin.Chart2);
		})
		.fail(function() { console.log("Failed to load query")})
		.done(function() { console.log("Loaded query successfully")});
	},
	show : function()
	{
		$('#' + this.id).show();
		this.visible = true;
	},
	hide : function()
	{
		$('#' + this.id).hide();
		this.visible = false;
	},
	getCoordinateSystemID : function()
	{
		return this.coordinateSystemID;
	}
};
