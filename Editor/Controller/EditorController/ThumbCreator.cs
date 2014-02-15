using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
 
namespace ARdevKit.Controller.EditorController
{
    class ThumbCreator {
        public static Bitmap CreateThumb(string videoFilename, double positionPercent)
        {
            Stream ms = new FileStream("tempthump.bmp",FileMode.Create);
            (new NReco.VideoConverter.FFMpegConverter()).GetVideoThumbnail(videoFilename, ms);
            Bitmap b = (Bitmap)Image.FromStream(ms);
            ms.Close();
            File.Delete("tempthump.bmp");
            return b;
        }
    }
}
