using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Stargate;

namespace Dialer
{
    class Program
    {
        static void Main(string[] args)
        {
            var abydos = new GateAddress(27, 7, 15, 32, 12, 30);
            var gate = new Gate();
            foreach (var glyph in abydos.Address.Values)
            {
                gate.EnterGlyph(glyph);
                Thread.Sleep(1000);
            }
            gate.EnterGlyph(Glyph.Origin);
            Thread.Sleep(1000);
            if (gate.Status == GateStatus.Dialed)
                gate.Activate();

            Console.Read();
        }
    }
}
