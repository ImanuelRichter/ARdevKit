using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
//using MediaBrowser.Library.Logging;
 
namespace ARdevKit.Controller.EditorController
{
    class ThumbCreator {
 
        static readonly Guid videoType = new
                  System.Guid("73646976-0000-0010-8000-00AA00389B71");
 
 
        public static Bitmap CreateThumb(string videoFilename, double positionPercent)
        {
            Stream ms = new FileStream("tempthump.bmp",FileMode.Create);
            (new NReco.VideoConverter.FFMpegConverter()).GetVideoThumbnail(videoFilename, ms);
            Bitmap b=(Bitmap)Image.FromStream(ms);
            ms.Close();
            return b;
        }
    }
}
