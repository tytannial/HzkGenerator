using System.Drawing;
using System.Drawing.Text;
using FontExport.FontData.Lattice;

namespace FontExport;

public class HzkFont
{
    public enum SupportedEncoding
    {
        ASCII,
        GB2312,
        GBK,
        UNICODE
    }

    private static readonly byte[] BitMask = new byte[8] { 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };

    /// <summary>
    /// 点阵字体的宽度
    /// </summary>
    public int FontWidth { get; set; }

    /// <summary>
    /// 点阵字体的高度
    /// </summary>
    public int FontHeight { get; set; }

    /// <summary>
    /// 水平偏移
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// 垂直偏移
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// 字体
    /// </summary>
    public Font Font { get; set; }

    /// <summary>
    /// 点阵字体名称
    /// </summary>
    public string FileName { get; set; }

    public void GenerateLattice(SupportedEncoding encoding)
    {
        var encodingLib = encoding switch
        {
            SupportedEncoding.ASCII => new ASCII(),
            SupportedEncoding.GB2312 => new GB2312(),
            SupportedEncoding.GBK => new GBK(),
            SupportedEncoding.UNICODE => new UNICODE(),
            _ => default(AbstractLattice),
        };

        if (encodingLib == null)
        {
            throw new Exception("无效的编码");
        }

        // 输出流
        using var fs = new FileStream(FileName, FileMode.Create);

        for (byte zoneCode = encodingLib.ZoneCodeMin; zoneCode <= encodingLib.ZoneCodeMax; ++zoneCode)
        {
            for (byte posiCode = encodingLib.PosiCodeMin; posiCode <= encodingLib.PosiCodeMax; ++posiCode)
            {
                string str = encodingLib.GetSingle(zoneCode, posiCode);
                var graphicBuffer = PrintFont(str);
                fs.Write(graphicBuffer);
            }
        }
    }

    public byte[] PrintFont(string str, bool print = false)
    {
        //行占用字节数
        var colSize = (FontWidth + 7) >> 3;

        var width = colSize * 8;
        var height = FontWidth;

        using var bmp = new Bitmap(width, height);
        using var g = Graphics.FromImage(bmp);
        g.Clear(Color.White);
        g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
        using var sb = new SolidBrush(Color.Black);

        g.DrawString(str, Font, sb, Font.Size - FontWidth + 1 + X, Y);

        //乘高度得到每个字占用的字节数
        var fontMapBuffer = new byte[colSize * FontWidth];
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (bmp.GetPixel(col, row).ToArgb() == -1)
                {
                    continue;
                }
                fontMapBuffer[row * colSize + col / 8] |= BitMask[col % 8];
            }
        }

        if (!print)
        {
            return fontMapBuffer;
        }

        for (var k = 0; k < FontWidth; k++)
        {
            for (var j = 0; j < colSize; j++)
            {
                for (var i = 0; i < 8; i++)
                {
                    var flag = (fontMapBuffer[k * colSize + j] & BitMask[i]) > 0;
                    Console.Write(flag ? "■" : "□");
                }
            }
            Console.WriteLine();
        }

        return fontMapBuffer;
    }
}
