using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Stargate
{
    public class Gate
    {
        private readonly List<Chevron> Chevrons;

        private readonly LinkedList<Glyph> ring;

        private readonly bool rotateRing;

        public Gate(bool rotateRing = true)
        {
            this.rotateRing = rotateRing;
            var glyphList = Enum.GetValues(typeof(Glyph)).Cast<Glyph>();
            ring = new LinkedList<Glyph>(glyphList);
            Status = GateStatus.Inactive;
            Chevrons = new List<Chevron>
            {
                new Chevron(1),
                new Chevron(2),
                new Chevron(3),
                new Chevron(4),
                new Chevron(5),
                new Chevron(6),
                new Chevron(7)
            };
        }

        public GateStatus Status { get; private set; }
        public Glyph CurrentGlyph => ring.First.Value;

        public void Activate()
        {
            Console.WriteLine("");
            Console.WriteLine("KAWOOOOOOOSH");
            Console.WriteLine("");
            Status = GateStatus.Active;
        }

        public void Deactivate()
        {
            Chevrons.ForEach(c =>
                             {
                                 c.Glyph = Glyph.Origin;
                                 c.Status = ChevronStatus.Inactive;
                             });
            Console.WriteLine("Gate shut down.");
            Status = GateStatus.Inactive;
        }


        public void EnterGlyph(Glyph glyph)
        {
            if (Chevrons.Take(6).All(c => c.Status == ChevronStatus.Encoded) && glyph != Glyph.Origin)
            {
                Console.WriteLine("Last glyph must be the Point of Origin");
                Deactivate();
                return;
            }
            //if (Chevrons.Take(6).Any(c => c.Glyph == glyph)) return;

            Status = GateStatus.Dialing;

            if (rotateRing) RotateRing(glyph);

            var nextChevron = Chevrons.First(c => c.Status != ChevronStatus.Encoded);
            nextChevron.Glyph = glyph;
            nextChevron.Status = ChevronStatus.Encoded;
            if (nextChevron.Ordinal == 7)
            {
                var address = GetGateAddress();
                if (!address.IsValid)
                {
                    Console.WriteLine("Chevron 7 NOT LOCKED");
                    Console.WriteLine("Unable to connect.");
                    Deactivate();
                    return;
                }
                Console.WriteLine("Chevron 7...LOCKED");
                Status = GateStatus.Dialed;
            }
            else
                Console.WriteLine($"Chevron {nextChevron.Ordinal} Encoded: {glyph}");
        }

        private bool DoCounterClockwise(Glyph glyph)
        {
            return ring.TakeWhile(g => g != glyph).Count() < ring.Count / 2;
        }

        private GateAddress GetGateAddress()
        {
            return new GateAddress(Chevrons.Take(6).Select(c => (int) c.Glyph).ToList());
        }

        private void RotateClockwise()
        {
            var last = ring.Last;
            ring.RemoveLast();
            ring.AddFirst(last);
        }

        private void RotateCounterClockwise()
        {
            var first = ring.First;
            ring.RemoveFirst();
            ring.AddLast(first);
        }

        private void RotateRing(Glyph glyph)
        {
            var rotateSound = string.Empty;
            Action rotate;
            if (DoCounterClockwise(glyph))
            {
                rotateSound = "r";
                rotate = RotateCounterClockwise;
            }
            else
            {
                rotateSound = "R";
                rotate = RotateClockwise;
            }

            while (CurrentGlyph != glyph)
            {
                Console.Write(rotateSound);
                rotate.Invoke();
                Thread.Sleep(200);
            }
            Console.WriteLine();
            Console.WriteLine("Click");
            Thread.Sleep(500);
            Console.WriteLine("Click");
            Thread.Sleep(500);
        }
    }
}