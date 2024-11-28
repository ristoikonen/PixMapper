# PixMapper class library
Embeds ASCII data into uniform color Bitmap by encoding bit values into RGBA pixels

- - -

### Usage
#### Embed text from message.txt into mybitmap.bmp, filled with green color:

  ```
using PixMapper;
...
BitmapFactory f = new BitmapFactory();
f.Create("Lets put this text into image!", @"c:\temp\mybitmap.bmp", 100, 50, System.Drawing.Color.ForestGreen);

  ```

#### Read text from mybitmap.bmp:

  ```
using PixMapper;
...
BitmapFactory f = new BitmapFactory();
string msg = f.Read(bitmap_filename);
Console.WriteLine($"Message read from {bitmap_filename}: {msg}");
  ```

### How does it work?
* Start with original Bitmap of one uniform color. 
* First pixel is the original color, followed by message pixels, ended by end of message pixel.
* Mark end of message by adding two to R value of a pixel
* Get 1's and 0's of ASCII character, as in BIN column of https://www.ascii-code.com/
* Edit bitmaps pixels RGBA values: add 1 or 0.
* One RGBA pixel can hold four bit word => we utilise two pixels for 8 bit ASCII character


- - -

### In code

    // Assign first bit (0 or 1) into blue part of the pixel 

    // bitstring = "01010011" 
    datapixel.Blue = (byte)int.Parse(bitstring.Substring(0, 1));

    // add pixel of 0's and 1s holding ASCII data into original pixel
    originalpixel.Blue += datapixel;

    // overloaded '+' operator used in compound assignment
    // Note that Alpha is deducted
    public static BuGeRed operator + (BuGeRed bgr, BuGeRed addme)
    {

        return new BuGeRed(new byte[] { (byte)((byte)bgr.Blue + addme.Blue), (byte)((byte)bgr.Green + addme.Green), 
                            (byte)((byte)bgr.Red + addme.Red), (byte)((byte)bgr.Alpha - addme.Alpha) });
    }




   