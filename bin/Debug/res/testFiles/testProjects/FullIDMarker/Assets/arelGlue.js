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
		COS3Anchor = arel.Object.Model3D.createFromImage("COS3Anchor","Assets/anchor.png");
		COS3Anchor.setVisibility(false);
		COS3Anchor.setCoordinateSystemID(3);
		arel.Scene.addObject(COS3Anchor);
		COS4Anchor = arel.Object.Model3D.createFromImage("COS4Anchor","Assets/anchor.png");
		COS4Anchor.setVisibility(false);
		COS4Anchor.setCoordinateSystemID(4);
		arel.Scene.addObject(COS4Anchor);
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
		
			imageAugmentation1 = arel.Object.Model3D.createFromImage("imageAugmentation1","Assets/cinqueterre-279013_1280.jpg");
			imageAugmentation1.setVisibility(true);
			imageAugmentation1.setCoordinateSystemID(2);
			imageAugmentation1.setScale(new arel.Vector3D(1.0,1.0,1.0));
			imageAugmentation1.setTranslation(new arel.Vector3D(-82.0,86.0,0.0));
			var imageAugmentation1Rotation = new arel.Rotation();
			imageAugmentation1Rotation.setFromEulerAngleDegrees(new arel.Vector3D(0.0,0.0,0.0));
			imageAugmentation1.setRotation(imageAugmentation1Rotation);
			arel.Scene.addObject(imageAugmentation1);
		
			imageAugmentation2 = arel.Object.Model3D.createFromImage("imageAugmentation2","Assets/steelwool-265850_1920.jpg");
			imageAugmentation2.setVisibility(true);
			imageAugmentation2.setCoordinateSystemID(2);
			imageAugmentation2.setScale(new arel.Vector3D(1.0,1.0,1.0));
			imageAugmentation2.setTranslation(new arel.Vector3D(-81.0,12.0,2.0));
			var imageAugmentation2Rotation = new arel.Rotation();
			imageAugmentation2Rotation.setFromEulerAngleDegrees(new arel.Vector3D(0.0,0.0,0.0));
			imageAugmentation2.setRotation(imageAugmentation2Rotation);
			arel.Scene.addObject(imageAugmentation2);
		
			imageAugmentation3 = arel.Object.Model3D.createFromImage("imageAugmentation3","Assets/cartoon-41250_1280.png");
			imageAugmentation3.setVisibility(true);
			imageAugmentation3.setCoordinateSystemID(2);
			imageAugmentation3.setScale(new arel.Vector3D(1.0,1.0,1.0));
			imageAugmentation3.setTranslation(new arel.Vector3D(-37.0,67.0,0.0));
			var imageAugmentation3Rotation = new arel.Rotation();
			imageAugmentation3Rotation.setFromEulerAngleDegrees(new arel.Vector3D(0.0,0.0,0.0));
			imageAugmentation3.setRotation(imageAugmentation3Rotation);
			arel.Scene.addObject(imageAugmentation3);
		
			videoAugmentation1 = arel.Object.Model3D.createFromMovie("videoAugmentation1","Assets/video.alpha.3g2");
			videoAugmentation1.setVisibility(true);
			videoAugmentation1.setCoordinateSystemID(3);
			videoAugmentation1.setScale(new arel.Vector3D(1.0,1.0,1.0));
			videoAugmentation1.setTranslation(new arel.Vector3D(-84.0,82.0,0.0));
			var videoAugmentation1Rotation = new arel.Rotation();
			videoAugmentation1Rotation.setFromEulerAngleDegrees(new arel.Vector3D(0.0,0.0,0.0));
			videoAugmentation1.setRotation(videoAugmentation1Rotation);
			arel.Scene.addObject(videoAugmentation1);
			$.getScript("Events/videoAugmentation1_Event.js", function()
			{
			})
			.fail(function() { console.log("Failed to load events")})
			.done(function() { console.log("Loaded events successfully")});
		
			videoAugmentation2 = arel.Object.Model3D.createFromMovie("videoAugmentation2","Assets/video.3g2");
			videoAugmentation2.setVisibility(true);
			videoAugmentation2.setCoordinateSystemID(3);
			videoAugmentation2.setScale(new arel.Vector3D(1.0,1.0,1.0));
			videoAugmentation2.setTranslation(new arel.Vector3D(85.0,74.0,0.0));
			var videoAugmentation2Rotation = new arel.Rotation();
			videoAugmentation2Rotation.setFromEulerAngleDegrees(new arel.Vector3D(-20.0,10.0,-45.0));
			videoAugmentation2.setRotation(videoAugmentation2Rotation);
			arel.Scene.addObject(videoAugmentation2);
			$.getScript("Events/videoAugmentation2_Event.js", function()
			{
			})
			.fail(function() { console.log("Failed to load events")})
			.done(function() { console.log("Loaded events successfully")});
		
			chart4 = arel.Plugin.Chart4;
			chart4.hide();
		
			chart5 = arel.Plugin.Chart5;
			chart5.hide();
		
			chart6 = arel.Plugin.Chart6;
			chart6.hide();
		
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
			arel.Scene.getObject("imageAugmentation2").setVisibility(true);
			arel.Scene.getObject("imageAugmentation3").setVisibility(true);
			arel.Scene.getObject("videoAugmentation1").setVisibility(true);
			arel.Scene.getObject("videoAugmentation1").startMovieTexture();
			arel.Scene.getObject("videoAugmentation2").setVisibility(true);
			arel.Scene.getObject("videoAugmentation2").startMovieTexture();
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
			if (param[0].getCoordinateSystemID() == chart4.getCoordinateSystemID())
			{
				chart4.create();
				chart4.show();
				arel.Scene.getScreenCoordinatesFrom3DPosition(COS4Anchor.getTranslation(), chart4.getCoordinateSystemID(), function(coord){move(COS4Anchor, chart4, coord);});
			}
			if (param[0].getCoordinateSystemID() == chart5.getCoordinateSystemID())
			{
				chart5.create();
				chart5.show();
				arel.Scene.getScreenCoordinatesFrom3DPosition(COS4Anchor.getTranslation(), chart5.getCoordinateSystemID(), function(coord){move(COS4Anchor, chart5, coord);});
			}
			if (param[0].getCoordinateSystemID() == chart6.getCoordinateSystemID())
			{
				chart6.create();
				chart6.show();
				arel.Scene.getScreenCoordinatesFrom3DPosition(COS4Anchor.getTranslation(), chart6.getCoordinateSystemID(), function(coord){move(COS4Anchor, chart6, coord);});
			}
		}
		else if(type && type == arel.Events.Scene.ONTRACKING && param[0].getState() == arel.Tracking.STATE_NOTTRACKING)
		{
			console.log("Tracking lost");
			chart1.hide();
			chart2.hide();
			chart3.hide();
			arel.Scene.getObject("imageAugmentation1").setVisibility(false);
			arel.Scene.getObject("imageAugmentation2").setVisibility(false);
			arel.Scene.getObject("imageAugmentation3").setVisibility(false);
			arel.Scene.getObject("videoAugmentation1").setVisibility(false);
			arel.Scene.getObject("videoAugmentation1").pauseMovieTexture();
			arel.Scene.getObject("videoAugmentation2").setVisibility(false);
			arel.Scene.getObject("videoAugmentation2").pauseMovieTexture();
			chart4.hide();
			chart5.hide();
			chart6.hide();
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
var imageAugmentation2;
var imageAugmentation3;
var COS3Anchor;
var videoAugmentation1;
var videoAugmentation2;
var COS4Anchor;
var chart4;
var chart5;
var chart6;
