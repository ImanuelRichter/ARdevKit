arel.Plugin.Chart5 = 
{
	visible : false,
	id : "chart5",
	coordinateSystemID : 4,
	options : {},
	translation : new arel.Vector3D(175.0,118.0,0.0),
	div : document.createElement("div"),
	create : function()
	{
		this.div.setAttribute("id", this.id);
		this.div.style.position = "absolute";
		this.div.style.width = "200px";
		this.div.style.height = "200px";
		document.documentElement.appendChild(this.div);
		$.getScript("Assets/chart5/options.js", function()
		{
			arel.Plugin.Chart5.options = init();
			$('#' + arel.Plugin.Chart5.id).highcharts(arel.Plugin.Chart5.options);
		})
		.fail(function() { console.log("Failed to load options for chart5")})
		.done(function() { console.log("Loaded options for chart5 successfully")});
		$.getScript("Assets/chart5/query.js", function()
		{
			var dataPath = "Assets/chart5/data.xml";
			query(dataPath, arel.Plugin.Chart5);
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
