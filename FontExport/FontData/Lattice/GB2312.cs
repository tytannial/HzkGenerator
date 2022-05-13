using System.Text;

namespace FontExport.FontData.Lattice;

public class GB2312 : AbstractLattice
{
    public GB2312() : base(Encoding.GetEncoding("GB2312"), 161, 254, 161, 254)
    {
    }

    public override string GetSingle(byte zoneCode, byte posiCode)
    {
        byte[] codes = new byte[2] { zoneCode, posiCode };
        return Encoding.GetString(codes);
    }
}
