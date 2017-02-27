using System;
using System.Collections.Generic;
using System.Linq;

namespace Stargate
{
    public class GateAddress
    {
        public GateAddress(int glyph1, int glyph2, int glyph3, int glyph4, int glyph5, int glyph6)
            : this(new List<int>
            {
                glyph1,
                glyph2,
                glyph3,
                glyph4,
                glyph5,
                glyph6
            })
        {
        }

        public GateAddress(IReadOnlyList<int> glyphList)
        {
            if (!glyphList.All(g => Enum.IsDefined(typeof(Glyph), g))) return;

            Glyph1 = (Glyph) glyphList[0];
            Glyph2 = (Glyph) glyphList[1];
            Glyph3 = (Glyph) glyphList[2];
            Glyph4 = (Glyph) glyphList[3];
            Glyph5 = (Glyph) glyphList[4];
            Glyph6 = (Glyph) glyphList[5];

            Address = new Dictionary<int, Glyph>
            {
                {1, Glyph1},
                {2, Glyph2},
                {3, Glyph3},
                {4, Glyph4},
                {5, Glyph5},
                {6, Glyph6}
            };
            IsValid = true;
            if (glyphList.Distinct().Count() != 6) IsValid = false;
        }

        public Glyph Glyph1 { get; set; }
        public Glyph Glyph2 { get; set; }
        public Glyph Glyph3 { get; set; }
        public Glyph Glyph4 { get; set; }
        public Glyph Glyph5 { get; set; }
        public Glyph Glyph6 { get; set; }

        public bool IsValid { get; }

        public Dictionary<int, Glyph> Address { get; }
        public string Designation { get; set; }
    }
}