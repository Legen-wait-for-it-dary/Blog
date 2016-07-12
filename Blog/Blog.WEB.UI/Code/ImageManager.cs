using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Hosting;

namespace Blog.WEB.UI.Code
{
    public static class ImageManager
    {
        public static string SaveImage(int mediaFileId, byte [] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            {
                using (var img = Image.FromStream(ms))
                {
                    string extension = "";
                    if (img.RawFormat.Equals(ImageFormat.Bmp)) extension = "bmp";
                    if (img.RawFormat.Equals(ImageFormat.Gif)) extension = "gif";
                    if (img.RawFormat.Equals(ImageFormat.Icon)) extension = "vnd.microsoft.icon";
                    if (img.RawFormat.Equals(ImageFormat.Jpeg)) extension = "jpeg";
                    if (img.RawFormat.Equals(ImageFormat.Png)) extension = "png";
                    if (img.RawFormat.Equals(ImageFormat.Tiff)) extension = "tiff";
                    if (img.RawFormat.Equals(ImageFormat.Wmf)) extension = "wmf";

                    string fileName = mediaFileId + "." + extension;
                    string path = HostingEnvironment.MapPath("~/Images/" + fileName);

                    using (var fs = new BinaryWriter(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)))
                    {
                        fs.Write(buffer);
                        fs.Close();
                    }
                    return fileName;
                }
            }
        }

        public static string GetImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;
            string serverRootDirectory = HostingEnvironment.MapPath(@"/Images/");
            string path = serverRootDirectory + fileName;
            if (serverRootDirectory != null)
            {
                return @"data:image/gif;base64," + Convert.ToBase64String(File.ReadAllBytes(path));
            }
            return null;
        }
    }
}