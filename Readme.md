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

 - - - 


 #### [Link to Pixdata projects home](https://github.com/ristoikonen/Pixdata)

   