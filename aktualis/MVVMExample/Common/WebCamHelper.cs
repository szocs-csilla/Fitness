using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace Common
{
    public class WebCamHelper
    {

        //Block Memory Leak
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr handle);
        public static BitmapSource bs;
        public static IntPtr ip;
        public static BitmapSource LoadBitmap(System.Drawing.Bitmap source)
        {

            ip = source.GetHbitmap();

            bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, System.Windows.Int32Rect.Empty,

                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            DeleteObject(ip);

            return bs;

        }
        public static void SaveImageCapture(BitmapSource bitmap)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.QualityLevel = 100;


            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Image"; // Default file name
            dlg.DefaultExt = ".Jpg"; // Default file extension
            dlg.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save Image
                string filename = dlg.FileName;
                FileStream fstream = new FileStream(filename, FileMode.Create);
                encoder.Save(fstream);
                fstream.Close();
            }

        }

        public static byte[] imageToByteArray(BitmapSource imageIn)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.QualityLevel = 100;

            byte[] bit = new byte[0];

            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Frames.Add(BitmapFrame.Create(imageIn));
                encoder.Save(stream);
                bit = stream.ToArray();
                stream.Close();
            }

            return bit;
        }

        public static BitmapImage byteArrayToImage(byte[] byteArrayIn)
        {
            Stream StreamObj = new MemoryStream(byteArrayIn);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = StreamObj;
            image.EndInit();

            return image;
        }
    }
}
