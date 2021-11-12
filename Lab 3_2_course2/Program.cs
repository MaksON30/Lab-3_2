using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Pipes;
using System.Text.RegularExpressions;

namespace Lab_3_2_course2
{
    class Program
    {
        static void Main(string[] args)
        {
            const string directoryPath = @"C:\Test";
            const string mirrored = "-mirrored";
            const string imageExtension = ".gif";

            Regex regexExtForImage = new Regex("^.*\\.(jpg|JPG|gif|GIF)$", RegexOptions.IgnoreCase);

            var files = Directory.GetFiles(directoryPath);

            foreach (var file in files)
            {
                var filename = file.Substring(0, file.LastIndexOf('.'));

                Console.WriteLine(file);
                try
                {
                    Image img = null;
                    FileStream fs = null;
                    try
                    {
                        fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                        img = Image.FromStream(fs);
                        Console.WriteLine("Image open");

                        Bitmap s = new Bitmap(img);
                        s.RotateFlip(RotateFlipType.Rotate180FlipY);
                        s.Save($"{filename}{mirrored}{imageExtension}");
                    }
                    finally
                    {
                        fs.Close();
                    }
                    Bitmap.FromFile(file);
                }
                catch (Exception e)
                {
                    if (regexExtForImage.IsMatch(file))
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
