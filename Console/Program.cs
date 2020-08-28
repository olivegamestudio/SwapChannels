using CommandLine;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SwapChannels
{
    class Program
    {
        [Verb("add", HelpText = "Filename(s) to convert.")]
        class AddOptions
        {
            [Option('f', "filename", Required = true, HelpText = "Filename(s) to convert.")]
            public string Filename { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<AddOptions>(args).MapResult((AddOptions opts) => RunAddAndReturnExitCode(opts), errs => 1);
        }

        private static object RunAddAndReturnExitCode(AddOptions opts)
        {
            string filename = opts.Filename;

            if (Path.GetExtension(filename).ToLowerInvariant() != ".png")
            {
                return null;
            }

            if (!File.Exists(filename))
            {
                return null;
            }

            var bytes = File.ReadAllBytes(filename);
            var ms = new MemoryStream(bytes);
            Bitmap bitmap = (Bitmap)Image.FromStream(ms);


            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    color = Color.FromArgb(color.A, color.B, color.G, color.R);
                    bitmap.SetPixel(x, y, color);
                }
            }

            bitmap.Save(filename, ImageFormat.Png);
            return null;
        }
    }
}
