namespace FontExport
{
    public static class FontTest
    {
        static long GetHzkCharacterPcxPointer(int width, int height, byte section, byte position)
        {
            return width * ((height + 7) >> 3) * (0x5E * (section - 0xA1) + position - 0xA1);
        }

        public static void PrintFont(string file, int width, int height, byte zoneCode, byte PosiCode)
        {
            using var fs = File.OpenRead(file);
            using var br = new BinaryReader(fs);
            var charOffset = GetHzkCharacterPcxPointer(width, height, zoneCode, PosiCode);
            var pos = fs.Seek(charOffset, SeekOrigin.Begin);

            for (int nRow = 0; nRow < height; ++nRow)
            {
                for (int nColumn = 0; nColumn < width; ++nColumn)
                {
                    fs.Seek(pos + nColumn / 8 + (height + 7) / 8 * nRow, SeekOrigin.Begin);
                    byte mask = br.ReadByte();
                    if (((1 << (7 - nColumn % 8)) & mask) > 0)
                    {
                        Console.Write("■");
                    }
                    else
                    {
                        Console.Write("□");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
