using System.Diagnostics;
using System.Drawing.Imaging;

namespace ScreenSnip
{
    public static class ScreenshotUtils
    {
        public static void TakeCutShot(Rectangle rect)
        {
            Image screenshot = ScreenShot();
            Bitmap originalImage = new Bitmap(screenshot, screenshot.Width, screenshot.Height);
            Bitmap snippedImage = new Bitmap(rect.Width, rect.Height);

            Graphics g = Graphics.FromImage(snippedImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.DrawImage(originalImage, 0, 0, rect, GraphicsUnit.Pixel);

            SaveSnippet(snippedImage);
        }

        private static Image ScreenShot()
        {
            Bitmap printscreen = new Bitmap(Screen.width, Screen.height);
            Graphics graphics = Graphics.FromImage(printscreen);
            graphics.CopyFromScreen(Screen.left, Screen.top, 0, 0, printscreen.Size);

            using (MemoryStream s = new MemoryStream())
            {
                printscreen.Save(s, ImageFormat.Bmp);
                return Image.FromStream(s);
            }
        }

        private static void SaveSnippet(Bitmap snippedImage)
        {
            string dirPath = GetImagesDirectoryPath();
            Directory.CreateDirectory(dirPath!);

            string fileImagePath = GetFileImagePath(dirPath,".png");
            snippedImage.Save(fileImagePath, ImageFormat.Png);

            // Open the explorer at the directory path
            Process.Start("explorer.exe", dirPath);
        }

        private static string GetImagesDirectoryPath()
        {
            string dirPath = string.Empty;

            // Check for any command line arguments
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 2) dirPath = args[1];

            if (dirPath.Length == 0 || !Directory.Exists(dirPath))
                dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Screenshots", "");

            return dirPath;
        }
        private static string GetFileImagePath(string dirPath,string imgFormat)
        {
            DateTime currentDate = DateTime.Now;
            string imageName = $"{currentDate.Year}-{currentDate.Month}-{currentDate.Day} {currentDate.Hour}-{currentDate.Minute}-{currentDate.Second}";
            string path = Path.Combine(dirPath, imageName + imgFormat);

            int i = 0;
            while (File.Exists(path))
            {
                path = Path.Combine(dirPath, $"{imageName} ({++i})" + imgFormat);
            }

            return path;
        }
    }
}
