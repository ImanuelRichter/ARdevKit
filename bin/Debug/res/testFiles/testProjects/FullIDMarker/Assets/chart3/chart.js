arel.Plugin.Chart3 = 
{
	visible : false,
	id : "chart3",
	coordinateSystemID : 1,
	options : {},
	translation : new arel.Vector3D(17.0,-174.0,0.0),
	div : document.createElement("div"),
	create : function()
	{
		this.div.setAttribute("id", this.id);
		this.div.style.position = "absolute";
		this.div.style.width = "200px";
		this.div.style.height = "200px";
		document.documentElement.appendChild(this.div);
		$.getScript("Assets/chart3/options.js", function()
		{
			arel.Plugin.Chart3.options = init();
			$('#' + arel.Plugin.Chart3.id).highcharts(arel.Plugin.Chart3.options);
		})
		.fail(function() { console.log("Failed to load options for chart3")})
		.done(function() { console.log("Loaded options for chart3 successfully")});
		$.getScript("Assets/chart3/query.js", function()
		{
			var dataPath = "Assets/chart3/data.json";
			query(dataPath, arel.Plugin.Chart3);
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
