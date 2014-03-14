arel.Plugin.Chart1 = 
{
	visible : false,
	id : "chart1",
	coordinateSystemID : 1,
	options : {},
	translation : new arel.Vector3D(-136.0,137.0,0.0),
	div : document.createElement("div"),
	create : function()
	{
		this.div.setAttribute("id", this.id);
		this.div.style.position = "absolute";
		this.div.style.width = "200px";
		this.div.style.height = "200px";
		document.documentElement.appendChild(this.div);
		$.getScript("Assets/chart1/options.js", function()
		{
			arel.Plugin.Chart1.options = init();
			$('#' + arel.Plugin.Chart1.id).highcharts(arel.Plugin.Chart1.options);
		})
		.fail(function() { console.log("Failed to load options for chart1")})
		.done(function() { console.log("Loaded options for chart1 successfully")});
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
