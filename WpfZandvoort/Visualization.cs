using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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

        private static Race _race;
        private static Directions direction = Directions.East;
        private static int imageDimentions = 300;
        
        #region images
        private const string Finish = "C:\\Users\\wesse\\source\\repos\\c-project-racebaan\\WpfZandvoort\\Images\\Sections\\FinishRoad.png";
        private const string Corner = "C:\\Users\\wesse\\source\\repos\\c-project-racebaan\\WpfZandvoort\\Images\\Sections\\CornerRoad.png";
        private const string Start = "C:\\Users\\wesse\\source\\repos\\c-project-racebaan\\WpfZandvoort\\Images\\Sections\\StartRoad.png";
        private const string Straight = "C:\\Users\\wesse\\source\\repos\\c-project-racebaan\\WpfZandvoort\\Images\\Sections\\StraightRoad.png";
        #endregion


        public static void Initialize(Race race)
        {
            _race = race;
        }

        //makes a image with the complete visualization
        //is used as the value of the image component
        public static BitmapSource DrawTrack(Track track)
        {
            Bitmap bitmap = DoImage.DrawBitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bitmap);

            foreach (Section section in track.Sections)
            {
                DrawSection(imageDimentions, imageDimentions, section, graphics);
            }

            return DoImage.CreateBitmapSourceFromGdiBitmap(bitmap);
        }

        public static void DrawSection(int x, int y, Section section, Graphics graphics)
        {
            Bitmap bitmap = DoImage.GetImage(ImagePath(section));

            if (section.SectionType.ToString().Contains("Left") || section.SectionType.ToString().Contains("Right"))
            {
                TurnImage(bitmap, section);
            }

            graphics.DrawImage(bitmap, x, y);
        }

        public static void DetermineDrawLocation()
        {

        }

        public static string ImagePath(Section section) 
        {
            if (section.SectionType.ToString().Contains("Finish"))
            {
                return Finish;
            }
            else if (section.SectionType.ToString().Contains("Left") || section.SectionType.ToString().Contains("Right"))
            {
                return Corner;
            }
            else if (section.SectionType.ToString().Contains("Start"))
            {
                return Start;
            }
            else if (section.SectionType.ToString().Contains("Straight"))
            {
                return Straight;
            }
            else
            {
                return "ERROR: unknown section";
            }
        }

        public static void TurnImage(Bitmap img, Section section)
        {
            switch (section.SectionType)
            {
                case SectionTypes.LeftE:
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone); //turn 90 degrees to go east
                    direction -= 1;
                    break;
                case SectionTypes.LeftS:
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone); //turn 180 degrees to go south
                    direction -= 1;
                    break;
                case SectionTypes.LeftW:
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone); //turn 270 degrees to go west
                    direction -= 1;
                    break;
                case SectionTypes.RightN:
                    img.RotateFlip(RotateFlipType.RotateNoneFlipY); //mirrors the image to be a right corner
                    direction += 1;
                    break;
                case SectionTypes.RightE:
                    img.RotateFlip(RotateFlipType.Rotate90FlipY); //mirrors the image to be a right corner and moves it 90 degrees to go east
                    direction += 1;
                    break;
                case SectionTypes.RightS:
                    img.RotateFlip(RotateFlipType.Rotate180FlipY); //mirrors the image to be a right corner and moves it 180 degrees to go south
                    direction += 1;
                    break;
                case SectionTypes.RightW:
                    img.RotateFlip(RotateFlipType.Rotate270FlipY); //mirrors the image to be a right corner and moves it 270 degrees to go west
                    direction += 1;
                    break;
                case SectionTypes.StraightN:
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone); //makes a straight go vertical
                    break;
                case SectionTypes.StraightS:
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone); //makes a straight go vertical
                    break;
                default:
                    break;
            }
        }
    }
}
