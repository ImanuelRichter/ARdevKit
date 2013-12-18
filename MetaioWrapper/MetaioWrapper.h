#include <msclr\marshal_cppstd.h>

namespace MetaioWrapper
{
	public ref class MyMetaioWrapper
	{
	public:
		void initializeSDK(int wndWidth, int wndHeight, void* hWnd);
		void update();
		System::String^ getVersion();
	};
}