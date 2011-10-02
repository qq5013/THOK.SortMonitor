using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace THOK.AS.Sorting.Util
{
    class GraphicsUtil
    {
        public static Bitmap CreateBitmap(string channelName,string text)
        {
            Image image = new Bitmap(16, 16);
            Graphics g = Graphics.FromImage(image);

            Font font = new Font("ËÎÌו", 10);

            g.TranslateTransform(0, 16);
            g.RotateTransform(270);

            g.Clear(Color.Black);

            g.DrawString(text, font, new SolidBrush(Color.Green), 0,0);

            return (Bitmap)image;
        }
    }
}
