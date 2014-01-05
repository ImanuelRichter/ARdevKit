#pragma once
#include "MetaioWrapper.h"
#include <MetaioBridge\MetaioBridge.h>

using namespace msclr::interop;
using namespace std;

namespace MetaioWrapper
{
	MyMetaioSDK* metaioSDK;

	MyMetaioWrapper::MyMetaioWrapper(int wndWidth, int wndHeight, void* hWnd)
	{
		metaioSDK = new MyMetaioSDK(wndWidth, wndHeight, hWnd);
	}

	bool MyMetaioWrapper::setTrackingConfiguration(String^ path)
	{
		const string tmp = marshal_as<string>(path);
		metaio::stlcompat::String str = metaio::stlcompat::String(tmp.c_str());

		return metaioSDK->setTrackingConfiguaration(str);
	}

	void MyMetaioWrapper::update()
	{
		metaioSDK->update();
	}

	/* 
	 * returns the verion of the used metaioSDK
	 */
	String^ MyMetaioWrapper::getVersion()
	{
		std::string tmp = metaioSDK->getVersion();
		String^ version = marshal_as<String^>(tmp);

		return version;
	}
}
