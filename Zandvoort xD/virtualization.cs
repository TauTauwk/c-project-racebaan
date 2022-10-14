﻿using Model;
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
            race.driverChanged += DriverChanged;
        }

        public static void DriverChanged(Object? sender, DriverChangedEventsArgs e)
        {
            DrawTrack(Data.CurrentRace);
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

        //private static string[] mirrorHorizontal(string[] _section)
        //{
        //    string[] result = new string[4];
        //    result[0] = _section[4];
        //    result[1] = _section[3];
        //    result[2] = _section[2];
        //    result[3] = _section[1];
        //    result[4] = _section[0];
        //    return result;
        //}

        //private static string[] TurnClockwise(string[] _section)
        //{
        //    _section.Reverse();
        //    char[] sectionDetails = new char[4];
        //    char[] tussenResult = new char[4];
        //    string[] result = new string[4];
        //    for (int row = 0; row < 4; row++)
        //    {
        //        int num = 0;
        //        foreach (string s in _section)
        //        {
        //            sectionDetails = s.ToCharArray();
        //            tussenResult[row] = sectionDetails[num];
        //            num++;
        //        }
        //        string charStr = new string(tussenResult);
        //        result[num] = charStr;
        //    }
        //    return result;
        //}

        //static string[] RotateString(string[] matrix, int n)
        //{
        //    string[] start = matrix;
        //    string[] result = new string[n];
        //    char[][] sectionDetails = new char[n][];

        //    for (int i = 0; i < n; i++)
        //    {
        //        foreach (string s in matrix)
        //        {
        //            sectionDetails[i] = s.ToCharArray();
        //        }
        //    }

        //    char[][] ret = new char[n][];

        //    for (int i = 0; i < n; ++i)
        //    {
        //        ret[i] = new char[n];

        //        for (int j = 0; j < n; ++j)
        //        {
        //            ret[i][j] = sectionDetails[n - j - 1][i];
        //        }
        //    }

        //    for (int i = 0; i < n; i++)
        //    {
        //        result[i] = new string(ret[i]);
        //    }
        //    return result;
        //}

        public static void DrawTrack(Race race)
        {
            int x = 25;
            int y = 24;
            int nummer = 0;
            Console.SetWindowSize(49, 21);
            Console.SetCursorPosition(x,y);

            foreach (Section section in race.Track.Sections) 
            {
                //Console.WriteLine(race.GetSectionData(section).Left);

                #region drawLeft
                if (section.SectionType == SectionTypes.LeftN)
                {
                    foreach (string s in _LeftN)
                    {
                        y++;
                        drawString(s, section);
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
                        drawString(s, section);
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
                        drawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.LeftW)
                {
                    foreach (string s in _LeftW)
                    {
                        y++;
                        drawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    x -= 5;
                    y -= 5;
                    Console.SetCursorPosition(x, y);
                }
                #endregion
                #region foute boel
                //string[] kindOfSection = new string[4];
                //for (int i = 0; i < bochtL % 4; i++)
                //{
                //    kindOfSection = RotateString(_LeftHorizontal, 4);
                //    if (bochtL % 4 == 0)
                //    {

                //    }
                //    else if (bochtL % 4 == 1)
                //    {

                //    }
                //    else if (bochtL % 4 == 2)
                //    {

                //    }
                //}

                //foreach (string s in kindOfSection)
                //{
                //    Console.SetCursorPosition(x, y);
                //    Console.WriteLine(s);
                //}
                //bochtL++;
                #endregion
                #region drawRight
                if (section.SectionType == SectionTypes.RightN)
                {
                    foreach (string s in _RightN)
                    {
                        y++;
                        drawString(s, section);
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
                        drawString(s, section);
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
                        drawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.RightW)
                {
                    foreach (string s in _RightW)
                    {
                        y++;
                        drawString(s, section);
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
                        string str = s;
                        y++;
                        if ((s.Contains("R") || s.Contains("L")) && nummer < Data.CurrentRace?.Participants?.Count())
                        {
                            IParticipant Left;
                            IParticipant Right;

                            SecD = race.GetSectionData(section);
                            SecD.Left = race.Participants[nummer];
                            SecD.Right = race.Participants[nummer++];

                            Left = SecD.Left;
                            Right = SecD.Right;

                            //Left = Data.CurrentRace.Participants[nummer];
                            //Right = Data.CurrentRace.Participants[nummer];
                            str = DrawPlayers(s, Left, Right);
                        }
                        Console.WriteLine(str);
                        Console.SetCursorPosition(x, y);
                        //y++;
                        //drawString(s, section);
                        //Console.SetCursorPosition(x, y);

                        
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
                        drawString(s, section);
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
                        drawString(s, section);
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
                        drawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    Console.SetCursorPosition(x, y);
                }
                if (section.SectionType == SectionTypes.StraightW)
                {
                    foreach (string s in _StraightW)
                    {
                        y++;
                        drawString(s, section);
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
                        drawString(s, section);
                        Console.SetCursorPosition(x, y);
                    }
                    x += 2;
                    y -= 5;
                    Console.SetCursorPosition(x, y);
                }
                #endregion
            }
        }

        public static string DrawPlayers(string str, IParticipant? LeftPlayer, IParticipant? RightPlayer)
        {
            string s = str;
            if (LeftPlayer != null)
            {
                char LeftDriverNumber = LeftPlayer.Name[LeftPlayer.Name.Length - 1];
                str = str.Replace("L", LeftDriverNumber.ToString());
            }

            if (RightPlayer != null)
            {
                char RightDriverNumber = RightPlayer.Name[RightPlayer.Name.Length - 1];
                str = str.Replace("R", RightDriverNumber.ToString());
            }

            return str;
        }

        public static void drawString(string s, Section section)
        {
            string str = s;
            if ((s.Contains("R") || s.Contains("L")))
            {
                IParticipant Left = null;
                IParticipant Right = null;

                SecD = Data.CurrentRace.GetSectionData(section);

                Left = SecD.Left;
                Right = SecD.Right;

                //Left = Data.CurrentRace.Participants[nummer];
                //Right = Data.CurrentRace.Participants[nummer];
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
    }
}
