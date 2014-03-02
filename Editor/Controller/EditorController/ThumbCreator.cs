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
    /// <summary>
    /// Utility class to create a thumbnail from video files. Uses AForge library.
    /// </summary>
    class ThumbCreator
    {
        /// <summary>
        /// Creates the thumb.
        /// </summary>
        /// <param name="videoFilename">The video filename.</param>
        /// <returns></returns>
        public static Bitmap CreateThumb(string videoFilename)
        {
            AForge.Video.FFMPEG.VideoFileReader v = new AForge.Video.FFMPEG.VideoFileReader();
            v.Open(videoFilename);
            return v.ReadVideoFrame();
        }
    }
}
