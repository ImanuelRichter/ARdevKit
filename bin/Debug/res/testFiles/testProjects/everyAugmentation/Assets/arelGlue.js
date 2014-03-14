arel.sceneReady
(
	function()
	{
		console.log("Scene ready");
		COS1Anchor = arel.Object.Model3D.createFromImage("COS1Anchor","Assets/anchor.png");
		COS1Anchor.setVisibility(false);
		COS1Anchor.setCoordinateSystemID(1);
		arel.Scene.addObject(COS1Anchor);
		COS2Anchor = arel.Object.Model3D.createFromImage("COS2Anchor","Assets/anchor.png");
		COS2Anchor.setVisibility(false);
		COS2Anchor.setCoordinateSystemID(2);
		arel.Scene.addObject(COS2Anchor);
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
		
			chart3 = arel.Plugin.Chart3;
			chart3.hide();
		
			imageAugmentation1 = arel.Object.Model3D.createFromImage("imageAugmentation1","Assets/frame.png");
			imageAugmentation1.setVisibility(true);
			imageAugmentation1.setCoordinateSystemID(2);
			imageAugmentation1.setScale(new arel.Vector3D(1.0,1.0,1.0));
			imageAugmentation1.setTranslation(new arel.Vector3D(-2.0,8.0,0.0));
			var imageAugmentation1Rotation = new arel.Rotation();
			imageAugmentation1Rotation.setFromEulerAngleDegrees(new arel.Vector3D(0.0,0.0,0.0));
			imageAugmentation1.setRotation(imageAugmentation1Rotation);
			arel.Scene.addObject(imageAugmentation1);
		
			videoAugmentation1 = arel.Object.Model3D.createFromMovie("videoAugmentation1","Assets/video.alpha.3g2");
			videoAugmentation1.setVisibility(true);
			videoAugmentation1.setCoordinateSystemID(2);
			videoAugmentation1.setScale(new arel.Vector3D(1.0,1.0,1.0));
			videoAugmentation1.setTranslation(new arel.Vector3D(-4.0,8.0,0.0));
			var videoAugmentation1Rotation = new arel.Rotation();
			videoAugmentation1Rotation.setFromEulerAngleDegrees(new arel.Vector3D(0.0,0.0,-90.0));
			videoAugmentation1.setRotation(videoAugmentation1Rotation);
			arel.Scene.addObject(videoAugmentation1);
		
	}
);
function trackingHandler(type, param)
{
	if(param[0] !== undefined)
	{
		if(type && type == arel.Events.Scene.ONTRACKING && param[0].getState() == arel.Tracking.STATE_TRACKING)
		{
			console.log("Tracked coordinateSystemID: " + param[0].getCoordinateSystemID());
			arel.Scene.getObject("imageAugmentation1").setVisibility(true);
			arel.Scene.getObject("videoAugmentation1").setVisibility(true);
			arel.Scene.getObject("videoAugmentation1").startMovieTexture();
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
			if (param[0].getCoordinateSystemID() == chart3.getCoordinateSystemID())
			{
				chart3.create();
				chart3.show();
				arel.Scene.getScreenCoordinatesFrom3DPosition(COS1Anchor.getTranslation(), chart3.getCoordinateSystemID(), function(coord){move(COS1Anchor, chart3, coord);});
			}
		}
		else if(type && type == arel.Events.Scene.ONTRACKING && param[0].getState() == arel.Tracking.STATE_NOTTRACKING)
		{
			console.log("Tracking lost");
			chart1.hide();
			chart2.hide();
			chart3.hide();
			arel.Scene.getObject("imageAugmentation1").setVisibility(false);
			arel.Scene.getObject("videoAugmentation1").setVisibility(false);
			arel.Scene.getObject("videoAugmentation1").pauseMovieTexture();
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
var chart3;
var COS2Anchor;
var imageAugmentation1;
var videoAugmentation1;
