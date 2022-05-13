using System.Drawing;
using System.Text;
using FontExport;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var hzkFont = new HzkFont();
hzkFont.FontWidth = 16;
hzkFont.FontHeight = 16;
hzkFont.X = 0;
hzkFont.Y = 0;
hzkFont.Font = new Font("宋体", 12);
hzkFont.FileName = "Hzk_Medium";

hzkFont.GenerateLattice(HzkFont.SupportedEncoding.GB2312);

FontTest.PrintFont(hzkFont.FileName, hzkFont.FontWidth, hzkFont.FontHeight, 0xBB, 0xD4);
