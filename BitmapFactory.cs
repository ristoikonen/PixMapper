using Pixdata;
using System.Drawing;

namespace PixMapper
{
    public interface IBitmapCreator
    {
        bool Create(string embedMsg, string bmpFileName, int w, int h, Color bgColor);
    }

    public interface IBitmapReader
    {
        string Read( string bmpFileName);
    }

    public class BitmapFactory: IBitmapCreator, IBitmapReader
    {
        public bool Create(string embedMsg, string bmpFileName, int w, int h, System.Drawing.Color bgColor)
        {

            if (embedMsg != null && (embedMsg.Length * 4) + 2 < (w * h))
            {
                //var fullPath = Path.GetFullPath(bmpFileName);
                var ex = Path.Exists(bmpFileName);

                BuGeRedCreator cr = new BuGeRedCreator(embedMsg, (int)(bgColor.R), (int)bgColor.G, (int)bgColor.B, (int)bgColor.A, h, w);
                var msgList = cr.CreateMessage();
                var bitmap = cr.CreateBitmap(msgList);
                bitmap.Save(bmpFileName);
            }
            return true;
        }

        public string Read(string bmpFileName)
        {
            string message =  "";

            if (File.Exists(bmpFileName))
            {
                BuGeRedCollection BGRColl = new BuGeRedCollection();
                UsAsciiIMap map = new UsAsciiIMap();

                var bgrList = BGRColl.GetBuGeRedListFromBitmap(new Bitmap(bmpFileName));
                BuGeRed basepix = bgrList[0];
                Color basecol = bgrList[0].ToColor();
                //BuGeRed basepix2 = new BuGeRed(Color.FromArgb(basepix.Alpha, basepix.Red, basepix.Green, basepix.Blue));

                int endmark = bgrList.FindIndex(bgr => bgr.Red == (basecol.R - 2));

                if(endmark>2)
                { 
                    var msglist = bgrList.GetRange(1, endmark - 1);
                    //BuGeRedCollection readcol = new BuGeRedCollection();
                    //readcol.pixelList = msglist;

                    var pairs = Enumerable.Range(0, msglist.Count / 2)
                                              .Select(i => Tuple.Create(msglist[i * 2], msglist[i * 2 + 1]));
                    foreach (var p in pairs)
                    {
                        var bitsstring = p.Item1.Difference(basepix) + p.Item2.Difference(basepix);
                        char ch = map.ConvertToChar(bitsstring);
                        // Console.WriteLine($"{bitsstring} => {ch}");
                        message += ch;
                    }
                }
            }

            return message;
        }
    }
}

