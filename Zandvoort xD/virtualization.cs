using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zandvoort_xD
{
    public static class Virtualization
    {
        public static void Initialize()
        {

        }

        #region graphics
        private static string[] _finishHorizontal =
        {
            "F----",
            "F  # ",
            "F    ",
            "F  # ",
            "F----"
        };

        private static string[] _StraightHorizontal =
        {
            "-----",
            "   # ",
            "     ",
            "   # ",
            "-----"
        };

        private static string[] _LeftHorizontal =
        {
            "/   |",
            " # #|",
            "    /",
            "   / ",
            "--/  "
        };

        private static string[] _RightHorizontal =
        {
            "--\\  ",
            "   \\ ",
            "    \\",
            " # #|",
            "\\   |"
        };

        private static string[] _StartGrid =
        {
            "-----",
            "  >#>",
            "     ",
            " > # ",
            "-----"
        };

        #endregion

        private static string[] mirrorHorizontal(string[] _section)
        {
            string[] result = new string[4];
            result[0] = _section[4];
            result[1] = _section[3];
            result[2] = _section[2];
            result[3] = _section[1];
            result[4] = _section[0];
            return result;
        }

        private static string[] mirrorVertical(string[] _section)
        {
            int num = 0;
            char[] [] ch = new char[4][];
            foreach (string s in _section)
            {
                char[] sectionDetails = s.ToCharArray();
                ch[num] = sectionDetails;

                string charStr = new string(sectionDetails);
                num++;
            }
        }
        public static void DrawTrack(Track track)
        {
            int x = 25;
            int y = 24;

            Console.SetWindowSize(49, 49);
            Console.SetCursorPosition(x,y);

            foreach (var section in track.Sections) 
            {
                if (section.SectionType == SectionTypes.LeftCorner)
                {
                    foreach (string s in _LeftHorizontal)
                    {
                        y++;
                        Console.SetCursorPosition(x, y);
                        Console.WriteLine(s);
                    }
                }
                if (section.SectionType == SectionTypes.RightCorner)
                {
                    foreach (string s in _RightHorizontal)
                    {
                        y++;
                        Console.SetCursorPosition(x, y);
                        Console.WriteLine(s);
                    }
                }
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    foreach (string s in _StartGrid)
                    {
                        y++;
                        Console.SetCursorPosition(x, y);
                        Console.WriteLine(s);
                    }
                }
                if (section.SectionType == SectionTypes.Straight)
                {
                    foreach (string s in _StraightHorizontal)
                    {
                        y++;
                        Console.SetCursorPosition(x, y);
                        Console.WriteLine(s);
                    }
                }
                if (section.SectionType == SectionTypes.Finish)
                {
                    foreach (string s in _finishHorizontal)
                    {
                        y++;
                        Console.SetCursorPosition(x, y);
                        Console.WriteLine(s);
                    }
                }

            }
        }
    }
}
