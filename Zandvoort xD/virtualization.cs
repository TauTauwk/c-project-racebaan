using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;

namespace Zandvoort_xD
{
    public static class Virtualization
    {
        private static SectionData SecD;

        public static void Initialize(Race race)
        {
            Console.Clear();
            race.DriverChanged += DriverChanged;
        }
        //if a driver changes you want the track to be drawn over
        private static void DriverChanged(Object? sender, DriverChangedEventsArgs e)
        {
            DrawTrack(e.Track);
        }
        #region graphics
        #region finish
        private static string[] _finishE =
        {
            "F-",
            "FL",
            "F ",
            "FR",
            "F-"
        };
        #endregion
        #region Straight
        private static string[] _StraightN =
        {
            "|   |",
            "|L R|",
            "|   |",
            "|   |",
            "|   |"
        };
        private static string[] _StraightE =
        {
            "-----",
            "   L ",
            "     ",
            "   R ",
            "-----"
        };
        private static string[] _StraightS =
        {
            "|   |",
            "|   |",
            "|   |",
            "|R L|",
            "|   |"
        };
        private static string[] _StraightW =
        {
            "-----",
            " R   ",
            "     ",
            " L   ",
            "-----"
        };
        #endregion
        #region Left
        private static string[] _LeftN =
        {
            "/   |",
            " L R|",
            "    /",
            "   / ",
            "--/  "
        };
        private static string[] _LeftE =
        {
            "|   \\",
            "|  L ",
            "\\    ",
            " \\ R ",
            "  \\--"
        };
        private static string[] _LeftS =
        {
            "  /--",
            " /   ",
            "/    ",
            "|R L ",
            "|   /"
        };
        private static string[] _LeftW =
        {
            "--\\  ",
            " R \\",
            "    \\",
            " L  |",
            "\\   |"
        };
        #endregion
        #region Right
        private static string[] _RightN =
        {
            "|   \\",
            "|L R ",
            "\\    ",
            " \\   ",
            "  \\--"
        };
        private static string[] _RightE =
        {
            "  /--",
            " / L ",
            "/    ",
            "|  R ",
            "|   /"
        };
        private static string[] _RightS =
        {
            "--\\  ",
            "   \\ ",
            "    \\",
            " R L|",
            "\\   |"
        };
        private static string[] _RightW =
        {
            "/   |",
            " R  |",
            "    /",
            " L /",
            "--/  "
        };
        #endregion
        #region start
        private static string[] _StartE =
        {
            "--",
            " L",
            "  ",
            "R ",
            "--"
        };
        #endregion
        #endregion
        //drawing all the sections
        public static void DrawTrack(Track track)
        {
            int x = 25;
            int y = 24;
            Console.SetWindowSize(49, 21);
            Console.SetCursorPosition(x,y);

            foreach (Section section in track.Sections) 
            {
                //Console.WriteLine(race.GetSectionData(section).Left);

                #region drawLeft
                if (section.SectionType == SectionTypes.LeftN)
                {
                    foreach (string s in _LeftN)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    y -= 10;
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.LeftE)
                {
                    foreach (string s in _LeftE)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    x += 5;
                    y -= 5;
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.LeftS)
                {
                    foreach (string s in _LeftS)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.LeftW)
                {
                    foreach (string s in _LeftW)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    x -= 5;
                    y -= 5;
                    Console.SetCursorPosition(x, y);
                }
                #endregion
                #region drawRight
                if (section.SectionType == SectionTypes.RightN)
                {
                    foreach (string s in _RightN)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    y -= 10;
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.RightE)
                {
                    foreach (string s in _RightE)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    x += 5;
                    y -= 5;
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.RightS)
                {
                    foreach (string s in _RightS)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.RightW)
                {
                    foreach (string s in _RightW)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    x -= 5;
                    y -= 5;
                    Console.SetCursorPosition(x, y);
                }
                #endregion
                #region drawStart
                if (section.SectionType == SectionTypes.StartE)
                {
                    foreach (string s in _StartE)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }

                    x += 2;
                    y -= 5;
                    Console.SetCursorPosition(x, y);
                }
                #endregion
                #region drawStraight
                if (section.SectionType == SectionTypes.StraightN)
                {
                    foreach (string s in _StraightN)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    y -= 10;
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.StraightE)
                {
                    foreach (string s in _StraightE)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    x += 5;
                    y -= 5;
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.StraightS)
                {
                    foreach (string s in _StraightS)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.StraightW)
                {
                    foreach (string s in _StraightW)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    x -= 5;
                    y -= 5;
                    Console.SetCursorPosition(x, y);
                }
                #endregion
                #region drawFinish
                if (section.SectionType == SectionTypes.FinishE)
                {
                    foreach (string s in _finishE)
                    {
                        y++;
                        DrawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    x += 2;
                    y -= 5;
                    Console.SetCursorPosition(x, y);
                }
                #endregion
            }
        }
        //place the drivers on the placeholders
        public static string DrawPlayers(string str, IParticipant? LeftPlayer, IParticipant? RightPlayer)
        {
            if (LeftPlayer != null)
            {
                if (LeftPlayer.Equipment.IsBroken)
                {
                    str = str.Replace("L", "#");
                }
                else
                {
                    char LeftDriverNumber = LeftPlayer.Name[LeftPlayer.Name.Length - 1];
                    str = str.Replace("L", LeftDriverNumber.ToString());
                }
            }

            if (RightPlayer != null)
            {
                if (RightPlayer.Equipment.IsBroken)
                {
                    str = str.Replace("R", "#");
                }
                else
                {
                    char RightDriverNumber = RightPlayer.Name[RightPlayer.Name.Length - 1];
                    str = str.Replace("R", RightDriverNumber.ToString());
                }
            }

            return str;
        }
        //draw the string of a section
        private static void DrawString(string s, Section section)
        {
            string str = s;
            if ((s.Contains("R") || s.Contains("L")))
            {
                IParticipant Left = null;
                IParticipant Right = null;

                SecD = Data.CurrentRace.GetSectionData(section);

                Left = SecD.Left;
                Right = SecD.Right;

                //check if that section has any players on it
                if (Right != null)
                {
                    str = DrawPlayers(s, Left, Right);
                }
                if (Left != null)
                {
                    str = DrawPlayers(s, Left, Right);
                }
            }
            Console.WriteLine(str);
        }
        //when the event NextRace is triggerd this will be played
        public static void OnNextRace(object? sender, OnNextRaceEventArgs e)
        {
            Initialize(e.Race);
            Data.CurrentRace.DriverChanged += DriverChanged;
            DrawTrack(Data.CurrentRace.track);
        }
        public static string GetGraphics(SectionTypes section)
        {
            switch (section)
            {
                case SectionTypes.LeftN:
                    return _LeftN.ToString();
                    break;
                case SectionTypes.LeftE:
                    return _LeftE.ToString();
                    break;
                case SectionTypes.LeftS:
                    return _LeftS.ToString();
                    break;
                case SectionTypes.LeftW:
                    return _LeftW.ToString();
                    break;
                case SectionTypes.RightN:
                    return _RightN.ToString();
                    break;
                case SectionTypes.RightE:
                    return _RightE.ToString();
                    break;
                case SectionTypes.RightS:
                    return _RightS.ToString();
                    break;
                case SectionTypes.RightW:
                    return _RightW.ToString();
                    break;
                case SectionTypes.StraightN:
                    return _StraightN.ToString();
                    break;
                case SectionTypes.StraightE:
                    return _StraightE.ToString();
                    break;
                case SectionTypes.StraightS:
                    return _StraightS.ToString();
                    break;
                case SectionTypes.StraightW:
                    return _StraightW.ToString();
                    break;
                case SectionTypes.StartE:
                    return _StartE.ToString();
                    break;
                case SectionTypes.FinishE:
                    return _finishE.ToString();
                    break;
                default:
                    return null;
            }
        }
    }
}
