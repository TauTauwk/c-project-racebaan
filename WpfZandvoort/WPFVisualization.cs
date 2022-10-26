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
    public static class WPFVisualization
    {

        enum Directions
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }

        private static Race _race;
        private static Directions directions = Directions.East;
        private static (int width, int height) imageDimentions = (300, 300);
        private static int XLength = 0;
        private static int YLength = 0;

        #region images
        private const string Finish = @"C:\Users\wesse\source\repos\c-project-racebaan\WpfZandvoort\Images\Sections\FinishRoad.png";
        private const string Corner = @"C:\Users\wesse\source\repos\c-project-racebaan\WpfZandvoort\Images\Sections\CornerRoad.png";
        private const string Start = @"C:\Users\wesse\source\repos\c-project-racebaan\WpfZandvoort\Images\Sections\StartRoad.png";
        private const string Straight = @"C:\Users\wesse\source\repos\c-project-racebaan\WpfZandvoort\Images\Sections\StraightRoad.png";
        #endregion


        public static void Initialize(Race race)
        {
            _race = race;
        }

        public static BitmapSource DrawTrack(Track track)
        {
            TrackSize(track);
            Bitmap bitmap = DoHim.DrawBitmap(XLength, YLength); //die 300 moet aangepast worden door de totale grootte van je track
            return DoHim.CreateBitmapSourceFromGdiBitmap(bitmap);
        }

        public static void TrackSize(Track track)
        {
            int width = 0;
            int height = 0;

            int x = 300;

            Directions direction = Directions.East;

            foreach (Section section in track.Sections)
            {
                if (section.SectionType.ToString().Contains("Left"))
                {
                    switch (direction)
                    {
                        case Directions.North:
                            height += x;
                            direction = Directions.East;
                            break;
                        case Directions.East:
                            width += x;
                            direction = Directions.South;
                            break;
                        case Directions.South:
                            height += x;
                            direction = Directions.West;
                            break;
                        case Directions.West:
                            width += x;
                            direction = Directions.North;
                            break;
                    }
                }
                else if (section.SectionType.ToString().Contains("Right"))
                {
                    switch (direction)
                    {
                        case Directions.North:
                            height += x;
                            direction = Directions.West;
                            break;
                        case Directions.East:
                            width += x;
                            direction = Directions.North;
                            break;
                        case Directions.South:
                            height += x;
                            direction = Directions.East;
                            break;
                        case Directions.West:
                            width += x;
                            direction = Directions.South;
                            break;
                    }

                }

                if (direction == Directions.East || direction == Directions.West)
                {
                    if (section.SectionType == SectionTypes.StartE || section.SectionType == SectionTypes.FinishE)
                    {
                        width += 120;
                    }
                    else
                    {
                        width += x;
                    }
                }
                if (direction == Directions.North || direction == Directions.South)
                {
                    height += x;
                }
            }
            XLength = width;
            YLength = height;
        }

        #region triedButFailed
        ////makes a image with the complete visualization
        ////is used as the value of the image component
        //public static BitmapSource DrawTrack(Track track)
        //{
        //    (int width, int height) = DetermineTrackSize(track);

        //    Bitmap bitmap = DoImage.DrawBitmap(100, 100);
        //    Graphics graphics = Graphics.FromImage(bitmap);

        //    int x = 666, y = 66;
        //    Directions currentDirection = Directions.East;

        //    foreach (Section section in track.Sections)
        //    {
        //        DrawSection(x, y, currentDirection, section, graphics);
        //        currentDirection = DetermineDirection((SectionTypes)section.SectionType, currentDirection);
        //        DetermineNewCoordinates(ref x, ref y, currentDirection);
        //    }

        //    return DoImage.CreateBitmapSourceFromGdiBitmap(bitmap);
        //}

        //private static void DrawSection(int x, int y, Directions direction, Section section, Graphics graphics)
        //{
        //    Bitmap bitmap = DoImage.GetImage(ImagePath(section));

        //    if (section.SectionType.ToString().Contains("Left") || section.SectionType.ToString().Contains("Right"))
        //    {
        //        TurnImage(bitmap, section);
        //    }

        //    graphics.DrawImage(bitmap, x, y, imageDimentions, imageDimentions);
        //}

        //public static void DetermineDrawLocation()
        //{

        //}

        //public static (int width, int height) DetermineTrackSize(Track track)
        //{
        //    List<int> positionX = new List<int>();
        //    List<int> positionY = new List<int>();

        //    int x = 600;
        //    int y = 200;

        //    Directions currentDirection = Directions.East;

        //    foreach (var section in track.Sections)
        //    {
        //        positionX.Add(x);
        //        positionY.Add(y);

        //        currentDirection = DetermineDirection((SectionTypes)section.SectionType, currentDirection);

        //        DetermineNewCoordinates(ref x, ref y, currentDirection);
        //    }

        //    int width = positionX.Max();
        //    int height = positionY.Max();

        //    return (width + x, height + y);
        //}

        //private static void DetermineNewCoordinates(ref int x, ref int y, Directions currentDirection)
        //{
        //    switch (currentDirection)
        //    {
        //        case Directions.North:
        //            y -= imageDimentions;
        //            break;
        //        case Directions.East:
        //            x += imageDimentions;
        //            break;
        //        case Directions.South:
        //            y += imageDimentions;
        //            break;
        //        case Directions.West:
        //            x-= imageDimentions;
        //            break;
        //        default:
        //            throw new Exception("Unsupported irections");
        //    }
        //}

        //private static Directions DetermineDirection(SectionTypes sectionType, Directions currentDirection) 
        //{
        //    Directions newDirection = currentDirection;

        //    switch (sectionType)
        //    {
        //        case SectionTypes.RightN:
        //            newDirection = Directions.North;
        //            break;
        //        case SectionTypes.RightE:
        //            newDirection = Directions.East;
        //            break;
        //        case SectionTypes.RightS:
        //            newDirection = Directions.South;
        //            break;
        //        case SectionTypes.RightW:
        //            newDirection = Directions.West;
        //            break;
        //        case SectionTypes.LeftN:
        //            newDirection = Directions.North;
        //            break;
        //        case SectionTypes.LeftE:
        //            newDirection = Directions.East;
        //            break;
        //        case SectionTypes.LeftS:
        //            newDirection = Directions.South;
        //            break;
        //        case SectionTypes.LeftW:
        //            newDirection = Directions.West;
        //            break;
        //    }
        //    return newDirection;
        //}

        //public static string ImagePath(Section section) 
        //{
        //    if (section.SectionType.ToString().Contains("Finish"))
        //    {
        //        return Finish;
        //    }
        //    else if (section.SectionType.ToString().Contains("Left") || section.SectionType.ToString().Contains("Right"))
        //    {
        //        return Corner;
        //    }
        //    else if (section.SectionType.ToString().Contains("Start"))
        //    {
        //        return Start;
        //    }
        //    else if (section.SectionType.ToString().Contains("Straight"))
        //    {
        //        return Straight;
        //    }
        //    else
        //    {
        //        return "ERROR: unknown section";
        //    }
        //}

        //public static void TurnImage(Bitmap img, Section section)
        //{
        //    switch (section.SectionType)
        //    {
        //        case SectionTypes.LeftE:
        //            img.RotateFlip(RotateFlipType.Rotate90FlipNone); //turn 90 degrees to go east
        //            break;
        //        case SectionTypes.LeftS:
        //            img.RotateFlip(RotateFlipType.Rotate180FlipNone); //turn 180 degrees to go south
        //            break;
        //        case SectionTypes.LeftW:
        //            img.RotateFlip(RotateFlipType.Rotate270FlipNone); //turn 270 degrees to go west
        //            break;
        //        case SectionTypes.RightN:
        //            img.RotateFlip(RotateFlipType.RotateNoneFlipY); //mirrors the image to be a right corner
        //            break;
        //        case SectionTypes.RightE:
        //            img.RotateFlip(RotateFlipType.Rotate90FlipY); //mirrors the image to be a right corner and moves it 90 degrees to go east
        //            break;
        //        case SectionTypes.RightS:
        //            img.RotateFlip(RotateFlipType.Rotate180FlipY); //mirrors the image to be a right corner and moves it 180 degrees to go south
        //            break;
        //        case SectionTypes.RightW:
        //            img.RotateFlip(RotateFlipType.Rotate270FlipY); //mirrors the image to be a right corner and moves it 270 degrees to go west
        //            break;
        //        case SectionTypes.StraightN:
        //            img.RotateFlip(RotateFlipType.Rotate90FlipNone); //makes a straight go vertical
        //            break;
        //        case SectionTypes.StraightS:
        //            img.RotateFlip(RotateFlipType.Rotate90FlipNone); //makes a straight go vertical
        //            break;
        //        default:
        //            break;
        //    }
        //}
        #endregion
    }
}
