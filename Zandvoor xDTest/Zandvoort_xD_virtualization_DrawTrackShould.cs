using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Zandvoort_xD;

namespace Zandvoor_xDTest
{
    public class Zandvoort_xD_virtualization_DrawTrackShould
    {
        Track track;
        SectionTypes[] sections;
        [SetUp] 
        public void SetUp() 
        {
            var ln = SectionTypes.LeftN;
            var le = SectionTypes.LeftE;
            var ls = SectionTypes.LeftS;
            var lw = SectionTypes.LeftW;
            var rn = SectionTypes.RightN;
            var re = SectionTypes.RightE;
            var rs = SectionTypes.RightS;
            var rw = SectionTypes.RightW;
            var Se = SectionTypes.StartE;
            var sn = SectionTypes.StraightN;
            var se = SectionTypes.StraightE;
            var ss = SectionTypes.StraightS;
            var sw = SectionTypes.StraightW;
            var Fe = SectionTypes.FinishE;

            sections = new SectionTypes[] {
                Se, ln, le, ls, lw, rn, re, rs, rw, sn, se, ss, sw, Fe
            };
            track = new Track("test", sections);
        }

        [Test]
        public void DrawTrack_track()
        {
            string[] strArr = new string[sections.Count()];
            for(int i = 0; i < sections.Count(); i++) {
                strArr[i]  = Virtualization.GetGraphics(sections[i]);
            }

            Assert.AreEqual(strArr.Length, sections.Count());
        }
    }
}
