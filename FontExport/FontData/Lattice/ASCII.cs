using System.Text;

namespace FontExport.FontData.Lattice;

public class ASCII : AbstractLattice
{
    public ASCII() : base(Encoding.ASCII, 0, 7, 0, 15)
    {
    }

    public override string GetSingle(byte zoneCode, byte posiCode)
    {
        byte[] codes = new byte[1] { (byte)(zoneCode << 4 | posiCode) };
        return Encoding.GetString(codes);
    }
}
