using System.Text;

namespace FontExport.FontData.Lattice;

public class UNICODE : AbstractLattice
{
    public UNICODE() : base(Encoding.Unicode, 0, byte.MaxValue, 0, byte.MaxValue)
    {
    }

    public override string GetSingle(byte zoneCode, byte posiCode)
    {
        byte[] codes = new byte[2] { posiCode, zoneCode };
        return Encoding.GetString(codes);
    }
}
