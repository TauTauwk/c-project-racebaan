using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WpfZandvoort
{
    public static class Visualization
    {

        enum Directions
        {
            North,
            East,
            South,
            West
        }
        #region images
        private const string Finish = ".\\Images\\Sections\\FinishRoad.png";
        private const string Corner = ".\\Images\\Sections\\CornerRoad.png";
        private const string Start = ".\\Images\\Sections\\StartRoad.png.png";
        private const string Straight = ".\\Images\\Sections\\StraightRoad.png";
        #endregion
        //makes a image with the complete visualization
        //is used as the value of the image component
        public static BitmapSource DrawTrack(Track track)
        {
            Bitmap bitmap = DoImage.DrawBitmap(100, 100);

            foreach (Section section in track.Sections)
            {
                if (section.SectionType == SectionTypes.StartE)
                {

                }
            }

            return DoImage.CreateBitmapSourceFromGdiBitmap(bitmap);
        }

    }
}
