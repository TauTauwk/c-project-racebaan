using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Section
    {
        public SectionTypes SectionType { get; set; }

        public Section(SectionTypes sectionType)
        {
            SectionType = sectionType;
        }
    }

    public enum SectionTypes
    { 
        StraightN,
        StraightE,
        StraightS,
        StraightW,
        LeftN,
        LeftE,
        LeftS,
        LeftW,
        RightN,
        RightE,
        RightS,
        RightW,
        StartE,
        FinishE,
    }
}
