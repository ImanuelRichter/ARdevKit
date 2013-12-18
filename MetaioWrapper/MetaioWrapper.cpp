#include "MetaioWrapper.h"
#include <MetaioBridge\MetaioBridge.h>

namespace MetaioWrapper
{
	MyMetaioSDK metaioSDK;

	void MyMetaioWrapper::initializeSDK(int wndWidth, int wndHeight, void* hWnd)
	{
		if (metaioSDK.initializeMetaioSDK(wndWidth, wndHeight, hWnd) == -1)
			MessageBox(NULL, TEXT("loading image failed"), TEXT("Error!"), MB_OK);
		else MessageBox(NULL, TEXT("perfekt :)"), TEXT("Succes!"), MB_OK);
	};

	void MyMetaioWrapper::update()
	{
		metaioSDK.update();
	}

	System::String^ MyMetaioWrapper::getVersion()
	{
		const char* tmp = metaioSDK.getVersion();
		System::String^ version = msclr::interop::marshal_as<System::String^>(tmp);

		return version;
	}
}
