arel.sceneReady
(
	function()
	{
		console.log("Scene ready");
		COS1Anchor = arel.Object.Model3D.createFromImage("COS1Anchor","Assets/anchor.png");
		COS1Anchor.setVisibility(false);
		COS1Anchor.setCoordinateSystemID(1);
		arel.Scene.addObject(COS1Anchor);
		arel.Events.setListener
		(
			arel.Scene, function(type, param)
			{
				trackingHandler(type, param);
			}
		);
			chart1 = arel.Plugin.Chart1;
			chart1.hide();
		
			chart2 = arel.Plugin.Chart2;
			chart2.hide();
		
	}
);
function trackingHandler(type, param)
{
	if(param[0] !== undefined)
	{
		if(type && type == arel.Events.Scene.ONTRACKING && param[0].getState() == arel.Tracking.STATE_TRACKING)
		{
			console.log("Tracked coordinateSystemID: " + param[0].getCoordinateSystemID());
			if (param[0].getCoordinateSystemID() == chart1.getCoordinateSystemID())
			{
				chart1.create();
				chart1.show();
				arel.Scene.getScreenCoordinatesFrom3DPosition(COS1Anchor.getTranslation(), chart1.getCoordinateSystemID(), function(coord){move(COS1Anchor, chart1, coord);});
			}
			if (param[0].getCoordinateSystemID() == chart2.getCoordinateSystemID())
			{
				chart2.create();
				chart2.show();
				arel.Scene.getScreenCoordinatesFrom3DPosition(COS1Anchor.getTranslation(), chart2.getCoordinateSystemID(), function(coord){move(COS1Anchor, chart2, coord);});
			}
		}
		else if(type && type == arel.Events.Scene.ONTRACKING && param[0].getState() == arel.Tracking.STATE_NOTTRACKING)
		{
			console.log("Tracking lost");
			chart1.hide();
			chart2.hide();
		}
	}
};
function move(anchor, object, coord)
{
	var oldLeft = object.div.style.left;
	var oldTop = object.div.style.top;
	var newLeft = (coord.getX() - parseInt(object.div.style.width) / 2) + object.translation.getX();
	var newTop = (coord.getY() - parseInt(object.div.style.height) / 2) - object.translation.getY();
	object.div.style.left = newLeft + 'px';
	object.div.style.top = newTop + 'px';
	if (object.div.style.left != oldLeft || object.div.style.top != oldTop)
	{
		console.log("Moved " + object.id + " from (" + oldLeft + ", " + oldTop + ") to (" + object.div.style.left + ", " + object.div.style.top + ")");
	}
	if (object.visible)
	{
		setTimeout(function() { arel.Scene.getScreenCoordinatesFrom3DPosition(anchor.getTranslation(), object.getCoordinateSystemID(), function(coord){move(anchor, object, coord);}); }, 100);
	}
};
var COS1Anchor;
var chart1;
var chart2;
