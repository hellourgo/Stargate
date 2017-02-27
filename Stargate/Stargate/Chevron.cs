namespace Stargate
{
    public class Chevron
    {
        public int Ordinal { get; }
        public ChevronStatus Status { get; set; }
        public Glyph Glyph { get; set; }

        public Chevron(int ordinal)
        {
            Status = ChevronStatus.Inactive;
            Ordinal = ordinal;
        }
    }
}