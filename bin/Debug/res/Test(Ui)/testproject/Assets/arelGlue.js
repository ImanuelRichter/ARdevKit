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
			imageAugmentation1 = arel.Object.Model3D.createFromImage("imageAugmentation1","Assets/metaioman_target.png");
			imageAugmentation1.setVisibility(true);
			imageAugmentation1.setCoordinateSystemID(1);
			imageAugmentation1.setScale(new arel.Vector3D(1.0,1.0,1.0));
			imageAugmentation1.setTranslation(new arel.Vector3D(131.0,100.0,0.0));
			var imageAugmentation1Rotation = new arel.Rotation();
			imageAugmentation1Rotation.setFromEulerAngleDegrees(new arel.Vector3D(0.0,0.0,0.0));
			imageAugmentation1.setRotation(imageAugmentation1Rotation);
			arel.Scene.addObject(imageAugmentation1);
		
			videoAugmentation1 = arel.Object.Model3D.createFromMovie("videoAugmentation1","Assets/video.alpha.3g2");
			videoAugmentation1.setVisibility(true);
			videoAugmentation1.setCoordinateSystemID(1);
			videoAugmentation1.setScale(new arel.Vector3D(1.0,1.0,1.0));
			videoAugmentation1.setTranslation(new arel.Vector3D(2.0,-93.0,0.0));
			var videoAugmentation1Rotation = new arel.Rotation();
			videoAugmentation1Rotation.setFromEulerAngleDegrees(new arel.Vector3D(0.0,0.0,0.0));
			videoAugmentation1.setRotation(videoAugmentation1Rotation);
			arel.Scene.addObject(videoAugmentation1);
		
			chart1 = arel.Plugin.Chart1;
			chart1.hide();
		
			chart2 = arel.Plugin.Chart2;
			chart2.hide();
		
			imageAugmentation2 = arel.Object.Model3D.createFromImage("imageAugmentation2","Assets/frame.png");
			imageAugmentation2.setVisibility(true);
			imageAugmentation2.setCoordinateSystemID(2);
			imageAugmentation2.setScale(new arel.Vector3D(1.0,1.0,1.0));
			imageAugmentation2.setTranslation(new arel.Vector3D(124.0,-97.0,0.0));
			var imageAugmentation2Rotation = new arel.Rotation();
			imageAugmentation2Rotation.setFromEulerAngleDegrees(new arel.Vector3D(0.0,0.0,0.0));
			imageAugmentation2.setRotation(imageAugmentation2Rotation);
			arel.Scene.addObject(imageAugmentation2);
		
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
			arel.Scene.getObject("imageAugmentation2").setVisibility(true);
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
				arel.Scene.getScreenCoordinatesFrom3DPosition(COS2Anchor.getTranslation(), chart2.getCoordinateSystemID(), function(coord){move(COS2Anchor, chart2, coord);});
			}
		}
		else if(type && type == arel.Events.Scene.ONTRACKING && param[0].getState() == arel.Tracking.STATE_NOTTRACKING)
		{
			console.log("Tracking lost");
			arel.Scene.getObject("imageAugmentation1").setVisibility(false);
			arel.Scene.getObject("videoAugmentation1").setVisibility(false);
			arel.Scene.getObject("videoAugmentation1").pauseMovieTexture();
			chart1.hide();
			chart2.hide();
			arel.Scene.getObject("imageAugmentation2").setVisibility(false);
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
var imageAugmentation1;
var videoAugmentation1;
var chart1;
var COS2Anchor;
var chart2;
var imageAugmentation2;
