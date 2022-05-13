using System.Text;

namespace FontExport.FontData.Lattice;

public abstract class AbstractLattice
{
    public byte ZoneCodeMin { get; } = 0;

    public byte ZoneCodeMax { get; } = 0;

    public byte PosiCodeMin { get; } = 0;

    public byte PosiCodeMax { get; } = 0;

    public Encoding Encoding { get; }

    public AbstractLattice(Encoding encoding, byte zoneCodeMin, byte zoneCodeMax, byte posiCodeMin, byte posiCodeMax)
    {
        ZoneCodeMin = zoneCodeMin;
        ZoneCodeMax = zoneCodeMax;
        PosiCodeMin = posiCodeMin;
        PosiCodeMax = posiCodeMax;
        Encoding = encoding;
    }

    public abstract string GetSingle(byte zoneCode, byte posiCode);
}

