#include "MetaioBridge.h"
#include <metaioSDK\IGeometry.h>

MyMetaioSDK::MyMetaioSDK(int wndWidth, int wndHeight, void* hWnd) : m_pMetaioSDK(0)
{
	// create the SDK
	m_pMetaioSDK = metaio::CreateMetaioSDKWin32();
	m_pMetaioSDK->initializeRenderer(wndWidth, wndHeight, metaio::ESCREEN_ROTATION_0, metaio::ERENDER_SYSTEM_OPENGL_ES_2_0, hWnd);

	m_sensors = metaio::CreateSensorsComponent();
	m_pMetaioSDK->registerSensorsComponent(m_sensors);

	// set the callback to this class
	m_pMetaioSDK->registerCallback(this);

	// activate 1st camera
	m_pMetaioSDK->startCamera(0);

	if (!m_pMetaioSDK->setTrackingConfiguration("..\\res\\trackingconfigurations\\TrackingData_MarkerlessFast.xml"))
	{
		MessageBoxA(NULL, "In constructor doesnt work either", "MetaioBridge", MB_OK);
	}
}

bool MyMetaioSDK::setTrackingConfiguaration(metaio::stlcompat::String path)
{
	//MessageBoxA(NULL, path.c_str(), "MetaioBridge", MB_OK);
	if (!fopen(path.c_str(), "r"))
	{
		MessageBoxA(NULL, "Could not find *.xml", "MetaioBridge", MB_OK);
	}
	return m_pMetaioSDK->setTrackingConfiguration(path, true);
}

void MyMetaioSDK::update()
{
	m_pMetaioSDK->render();
}

const char* MyMetaioSDK::getVersion()
{
	return m_pMetaioSDK->getVersion().c_str();
}

void MyMetaioSDK::onAnimationEnd(metaio::IGeometry* geometry, const metaio::stlcompat::String& animationName)
{
	printf("Animation %s just ended \n", animationName.c_str());
}


void MyMetaioSDK::onTrackingEvent(const metaio::stlcompat::Vector<metaio::TrackingValues>& values)
{
	if (values.size() > 0)
	{
		printf("Tracking state changed:  %i\n", values[0].state);
	}
}


