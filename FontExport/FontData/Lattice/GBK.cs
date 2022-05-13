using System.Text;

namespace FontExport.FontData.Lattice;

public class GBK : AbstractLattice
{
    public GBK() : base(Encoding.GetEncoding("gbk"), 129, 254, 64, 254)
    {
    }

    public override string GetSingle(byte zoneCode, byte posiCode)
    {
        byte[] codes = new byte[2] { zoneCode, posiCode };
        return Encoding.GetString(codes);
    }
}
