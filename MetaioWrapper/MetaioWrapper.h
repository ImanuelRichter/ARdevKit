#include <stdlib.h>
#include <string.h>
#include <msclr\marshal.h>
#include <msclr\marshal_cppstd.h>

using namespace System;

namespace MetaioWrapper
{
	public ref class MyMetaioWrapper
	{
	public:
		MyMetaioWrapper(int wndWidth, int wndHeight, void* hWnd);
		void update();
		bool setTrackingConfiguration(String^);
		String^ getVersion();
	};
}