arel.Plugin.Chart6 = 
{
	visible : false,
	id : "chart6",
	coordinateSystemID : 4,
	options : {},
	translation : new arel.Vector3D(-10.0,-139.0,0.0),
	div : document.createElement("div"),
	create : function()
	{
		this.div.setAttribute("id", this.id);
		this.div.style.position = "absolute";
		this.div.style.width = "200px";
		this.div.style.height = "200px";
		document.documentElement.appendChild(this.div);
		$.getScript("Assets/chart6/options.js", function()
		{
			arel.Plugin.Chart6.options = init();
			$('#' + arel.Plugin.Chart6.id).highcharts(arel.Plugin.Chart6.options);
		})
		.fail(function() { console.log("Failed to load options for chart6")})
		.done(function() { console.log("Loaded options for chart6 successfully")});
		$.getScript("Assets/chart6/query.js", function()
		{
			var dataPath = "";
			query(dataPath, arel.Plugin.Chart6);
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
