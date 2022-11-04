using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        private static Directions directions = Directions.East;
        private const int imageDimentions = 448;
        private static int SizeEast = 0;
        private static int SizeNorth = 0;
        private static int SizeWest = 0;
        private static int SizeSouth = 0;

        #region SectionImages
        private const string Finish = @"C:\Users\wesse\source\repos\Zandvoort xD\WpfZandvoort\Images\Sections\FinishRoad.png";
        private const string Corner = @"C:\Users\wesse\source\repos\Zandvoort xD\WpfZandvoort\Images\Sections\CornerRoad.png";
        private const string Start = @"C:\Users\wesse\source\repos\Zandvoort xD\WpfZandvoort\Images\Sections\StartRoad.png";
        private const string Straight = @"C:\Users\wesse\source\repos\Zandvoort xD\WpfZandvoort\Images\Sections\StraightRoad.png";
        #endregion
        #region CarImages
        private const string BlueCar = @"C:\Users\wesse\source\repos\Zandvoort xD\WpfZandvoort\Images\Cars\BlueCar.png";
        private const string GreenCar = @"C:\Users\wesse\source\repos\Zandvoort xD\WpfZandvoort\Images\Cars\GreenCar.png";
        private const string GreyCar = @"C:\Users\wesse\source\repos\Zandvoort xD\WpfZandvoort\Images\Cars\GreyCar.png";
        private const string RedCar = @"C:\Users\wesse\source\repos\Zandvoort xD\WpfZandvoort\Images\Cars\RedCar.png";
        private const string YellowCar = @"C:\Users\wesse\source\repos\Zandvoort xD\WpfZandvoort\Images\Cars\YellowCar.png";
        private const string Failure = @"C:\Users\wesse\source\repos\Zandvoort xD\WpfZandvoort\Images\Cars\Failure.png";
        #endregion

        public static void Initialize(Race race)
        {
            race.DriverChanged += DriverChanged;
        }
        public static BitmapSource DrawTrack(Track track)
        {
            int x = SizeWest - (imageDimentions - 179);
            int y = SizeSouth - imageDimentions;
            TrackSize(track);
            Bitmap Bitmap = DoImage.GetBitmap(SizeEast, SizeNorth);
            Graphics gBitmap = Graphics.FromImage(Bitmap);

            foreach (Section section in track.Sections)
            {
                Bitmap BTSection = DoImage.GetImage(ImagePathSection(section));

                DetermineDirection(section);

                switch (directions)
                {
                    case Directions.North:
                        if (section.SectionType.ToString().Contains("Right"))
                        {
                            BTSection.RotateFlip(RotateFlipType.Rotate270FlipX);
                        }
                        else
                        {
                            BTSection.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        }
                        gBitmap.DrawImage(BTSection, x, y);
                        DrawParticipants(gBitmap, section, Directions.North, x, y);
                        y -= imageDimentions;
                        continue;
                    case Directions.East:
                        if (section.SectionType == SectionTypes.StartE || section.SectionType == SectionTypes.FinishE)
                        {
                            gBitmap.DrawImage(BTSection, x, y);
                            DrawParticipants(gBitmap, section, Directions.East, x, y);
                            x += 179;
                            continue;
                        }
                        else
                        {
                            if (section.SectionType.ToString().Contains("Right"))
                            {
                                BTSection.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            }
                            else
                            {
                                BTSection.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                            }
                            gBitmap.DrawImage(BTSection, x, y);
                            DrawParticipants(gBitmap, section, Directions.East, x, y);
                            x += imageDimentions;
                            continue;
                        }
                    case Directions.South:
                        if (section.SectionType.ToString().Contains("Right"))
                        {
                            BTSection.RotateFlip(RotateFlipType.Rotate90FlipX);
                        }
                        else
                        {
                            BTSection.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        }
                        gBitmap.DrawImage(BTSection, x, y);
                        DrawParticipants(gBitmap, section, Directions.South, x, y);
                        y += imageDimentions;
                        continue;
                    case Directions.West:
                        if (section.SectionType.ToString().Contains("Right"))
                        {
                            BTSection.RotateFlip(RotateFlipType.Rotate180FlipY);
                        }
                        else
                        {
                            BTSection.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        }
                        gBitmap.DrawImage(BTSection, x, y);
                        DrawParticipants(gBitmap, section, Directions.West, x, y);
                        x -= imageDimentions;
                        continue;
                }
                #region bullshit
                //    DetermineDirection(section);
                //    if (section.SectionType.ToString().Contains("Right"))
                //    {
                //        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                //    }
                //    g.DrawImage(DoImage.GetImage(ImagePath(section)), x, y);
                //    #region DrawLeft
                //    if (section.SectionType == SectionTypes.LeftN)
                //    {
                //        y -= imageDimentions;
                //    }
                //    if (section.SectionType == SectionTypes.LeftE)
                //    {
                //        x += imageDimentions;
                //    }
                //    if (section.SectionType == SectionTypes.LeftS)
                //    {
                //        y += imageDimentions;
                //    }
                //    if (section.SectionType == SectionTypes.LeftW)
                //    {
                //        x -= imageDimentions;
                //    }
                //    #endregion
                //    #region DrawRight
                //    if (section.SectionType == SectionTypes.RightN)
                //    {
                //        y -= imageDimentions;
                //    }
                //    if (section.SectionType == SectionTypes.RightE)
                //    {
                //        x += imageDimentions;
                //    }
                //    if (section.SectionType == SectionTypes.RightS)
                //    {
                //        y += imageDimentions;
                //    }
                //    if (section.SectionType == SectionTypes.RightW)
                //    {
                //        x -= imageDimentions;
                //    }
                //    #endregion
                //    #region DrawStart
                //    if (section.SectionType == SectionTypes.StartE)
                //    {
                //        x += 179;
                //    }
                //    #endregion
                //    #region DrawStraight
                //    if (section.SectionType == SectionTypes.StraightN)
                //    {
                //        y -= imageDimentions;
                //    }
                //    if (section.SectionType == SectionTypes.StraightE)
                //    {
                //        x += imageDimentions;
                //    }
                //    if (section.SectionType == SectionTypes.StraightS)
                //    {
                //        y += imageDimentions;
                //    }
                //    if (section.SectionType == SectionTypes.StraightW)
                //    {
                //        x -= imageDimentions;
                //    }
                //    #endregion
                //    #region DrawFinish
                //    if (section.SectionType == SectionTypes.FinishE)
                //    {
                //        x += 179;
                //    }
                #endregion
            }

            return DoImage.CreateBitmapSourceFromGdiBitmap(Bitmap);
        }
        private static void DriverChanged(Object? sender, DriverChangedEventsArgs e)
        {
            DrawTrack(e.Track);
        }
        private static Bitmap VisualizeParticipants(Section section, string side)
        {
            //check if that section has any players on it
            if (side == "Right")
            {
                return DoImage.GetImage(ImagePathCar(Data.CurrentRace.GetSectionData(section).Right));
            }
            if (side == "Left")
            {
                return DoImage.GetImage(ImagePathCar(Data.CurrentRace.GetSectionData(section).Left));
            }
            return null;
        }
        private static void DrawParticipants(Graphics gBitmap, Section section, Directions direction, int x, int y)
        {
            int saveX = x;
            int saveY = y;
            IParticipant Right = Data.CurrentRace.GetSectionData(section).Right;
            IParticipant Left = Data.CurrentRace.GetSectionData(section).Left;

            Bitmap CarImage;

            if (Right != null)
            {
                CarImage = VisualizeParticipants(section, "Right");
                switch (direction)
                {
                    case Directions.North:
                        CarImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        gBitmap.DrawImage(CarImage, x += 260, y += 50);
                        x = saveX;
                        y = saveY;
                        break;
                    case Directions.East:
                        if (section.SectionType == SectionTypes.StartE || section.SectionType == SectionTypes.FinishE)
                        {
                            gBitmap.DrawImage(CarImage, x += 70, y += 260);
                        }
                        else
                        {
                            gBitmap.DrawImage(CarImage, x += 260, y += 260);
                        }
                        x = saveX;
                        y = saveY;
                        break;
                    case Directions.South:
                        CarImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        gBitmap.DrawImage(CarImage, x += 260, y += 260);
                        x = saveX;
                        y = saveY;
                        break;
                    case Directions.West:
                        CarImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        gBitmap.DrawImage(CarImage, x += 50, y += 260);
                        x = saveX;
                        y = saveY;
                        break;
                }
            }
            if (Left != null)
            {
                CarImage = VisualizeParticipants(section, "Left");
                switch (direction)
                {
                    case Directions.North:
                        CarImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        gBitmap.DrawImage(CarImage, x += 115, y += 70);
                        break;
                    case Directions.East:
                        if (section.SectionType == SectionTypes.StartE || section.SectionType == SectionTypes.FinishE)
                        {
                            gBitmap.DrawImage(CarImage, x += 50, y += 115);
                        }
                        else
                        {
                            gBitmap.DrawImage(CarImage, x += 260, y += 115);
                        }
                        break;
                    case Directions.South:
                        CarImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        gBitmap.DrawImage(CarImage, x += 115, y += 260);
                        break;
                    case Directions.West:
                        CarImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        gBitmap.DrawImage(CarImage, x += 70, y += 115);
                        break;
                }
            }
        }
        private static Directions DetermineDirection(Section section)
        {
            switch (section.SectionType)
            {
                case SectionTypes.LeftN:
                    directions = Directions.North;
                    break;
                case SectionTypes.LeftE:
                    directions = Directions.East;
                    break;
                case SectionTypes.LeftS:
                    directions = Directions.South;
                    break;
                case SectionTypes.LeftW:
                    directions = Directions.West;
                    break;
                case SectionTypes.RightN:
                    directions = Directions.North;
                    break;
                case SectionTypes.RightE:
                    directions = Directions.East;
                    break;
                case SectionTypes.RightS:
                    directions = Directions.South;
                    break;
                case SectionTypes.RightW:
                    directions = Directions.West;
                    break;
                case SectionTypes.StraightN:
                    directions = Directions.North;
                    break;
                case SectionTypes.StraightE:
                    directions = Directions.East;
                    break;
                case SectionTypes.StraightS:
                    directions = Directions.South;
                    break;
                case SectionTypes.StraightW:
                    directions = Directions.West;
                    break;
                case SectionTypes.FinishE:
                    directions = Directions.East;
                    break;
                case SectionTypes.StartE:
                    directions = Directions.East;
                    break;
            }
            return directions;
        }
        private static string ImagePathSection(Section section)
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
        private static string ImagePathCar(IParticipant participant)
        {
            if (participant.Equipment.IsBroken)
            {
                return Failure;
            }
            else
            {
                switch (participant.TeamColor)
                {
                    case TeamColors.Red:
                        return RedCar;
                        break;
                    case TeamColors.Green:
                        return GreenCar;
                        break;
                    case TeamColors.Yellow:
                        return YellowCar;
                        break;
                    case TeamColors.Grey:
                        return GreyCar;
                        break;
                    case TeamColors.Blue:
                        return BlueCar;
                        break;
                }
            }
            return Failure;
        }
        private static void TrackSize(Track track)
        {
            int north = 0;
            int east = 0;
            int south = 0;
            int west = 0;

            int x = imageDimentions;

            bool? FirstCornerRight = null;

            Directions direction = Directions.East;
            Track reversedTrack = track;
            reversedTrack.Sections.Reverse();

            foreach (Section section in track.Sections)
            {
                if (section.SectionType.ToString().Contains("Left"))
                {
                    switch (direction)
                    {
                        case Directions.North:
                            //north += x;
                            direction = Directions.East;
                            break;
                        case Directions.East:
                            east += x;
                            direction = Directions.South;
                            break;
                        case Directions.South:
                            //south += x;
                            direction = Directions.West;
                            break;
                        case Directions.West:
                            //west += x;
                            direction = Directions.North;
                            break;
                    }
                }
                else if (section.SectionType.ToString().Contains("Right"))
                {
                    if (FirstCornerRight == null)
                    {
                        FirstCornerRight = true;
                    }
                    switch (direction)
                    {
                        case Directions.North:
                            //north += x;
                            direction = Directions.West;
                            break;
                        case Directions.East:
                            east += x;
                            direction = Directions.North;
                            break;
                        case Directions.South:
                            //south += x;
                            direction = Directions.East;
                            break;
                        case Directions.West:
                            west += x;
                            direction = Directions.South;
                            break;
                    }
                }

                switch (direction)
                {
                    case Directions.North:
                        north += x;
                        break;
                    case Directions.East:
                        if (section.SectionType == SectionTypes.StartE || section.SectionType == SectionTypes.FinishE)
                        {
                            east += 179;
                        }
                        else
                        {
                            east += x;
                        }
                        break;
                    case Directions.South:
                        south += x;
                        break;
                    case Directions.West:
                        west += x;
                        break;
                }
            }
            SizeEast = west + x;
            SizeWest = west/2 + 225;
            if (FirstCornerRight == true)
            {
                SizeNorth = north + south;
                SizeSouth = x + x;
            }
            else
            {
                SizeNorth = north + south;
                SizeSouth = south + north;
            }
            //foreach (Section section in reversedTrack.Sections)
            //{
            //}
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
}

