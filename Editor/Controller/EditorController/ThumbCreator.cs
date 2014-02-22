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
    class ThumbCreator
    {
        public static Bitmap CreateThumb(string videoFilename, double positionPercent)
        {
            AForge.Video.FFMPEG.VideoFileReader v = new AForge.Video.FFMPEG.VideoFileReader();
            v.Open(videoFilename);
            return v.ReadVideoFrame();
        }
    }
}
