#include <metaioSDK\IMetaioSDKWin32.h>

class MyMetaioSDK : metaio::IMetaioSDKCallback
{
public:
	MyMetaioSDK(int wndWidth, int wndHeight, void* hWnd);
	std::string getVersion();
	bool setTrackingConfiguaration(metaio::stlcompat::String path);
	void update();

	// IMetaioSDKCallback BEGIN
	virtual void onAnimationEnd(metaio::IGeometry* geometry, const metaio::stlcompat::String& animationName) override;
	virtual void onTrackingEvent(const metaio::stlcompat::Vector<metaio::TrackingValues>& values) override;
	// IMetaioSDKCallback END

private:
	metaio::IMetaioSDKWin32* m_pMetaioSDK;
	metaio::IGeometry* m_geometry;
	metaio::ISensorsComponent* m_sensors;
	bool m_requestingClose;
};