using System;
using Amazon.S3;
using Amazon.S3.Model;
using System.Web;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace RTPServiceTest.Models
{
    public class AmazonS3Uploader
    {
        private string bucketName = "vimalarockia83/images1";
        //private string keyName = "img1";
        //private string filePath = "C:\\Users\\yourUserName\\Desktop\\myImageToUpload.jpg";

        public string UploadFile(string filepath, string keyName)
        {
            string returnmsg = "";
            var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    FilePath = filepath,
                    ContentType = "text/plain"
                };

                PutObjectResponse response = client.PutObject(putRequest);
                returnmsg = "1";
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    returnmsg = "Check the provided AWS Credentials.";
                }
                else
                {
                    returnmsg = "Error occurred: " + amazonS3Exception.InnerException + amazonS3Exception.StackTrace + amazonS3Exception.Message;
                }
            }
            return returnmsg;
        }

        public string S3UploadSingleImage(HttpPostedFile file, string KeyName)
        {
            string returnmsg = "";
            var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            var path = KeyName;
            try
            {
                //path = path + "." + file.FileName.Split('.').Last();
                Stream inputSteram = null;

                if (file.FileName.Split('.')[1] == "gif")
                {
                    inputSteram = file.InputStream;
                }
                else
                {
                    inputSteram = ResizeImageFile(file.InputStream, 1024);
                }
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    CannedACL = S3CannedACL.PublicRead,//PERMISSION TO FILE PUBLIC ACCESIBLE
                    Key = string.Format(path),
                    InputStream = inputSteram,//SEND THE FILE STREAM,   
                };

                PutObjectResponse response = client.PutObject(putRequest);
                returnmsg = "1";
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    returnmsg = "Check the provided AWS Credentials.";
                }
                else
                {
                    returnmsg = "Error occurred: " + amazonS3Exception.InnerException + amazonS3Exception.StackTrace + amazonS3Exception.Message;
                }
            }
            return returnmsg;
        }

        public static Stream ResizeImageFile(Stream imageFileStream, int targetSize) // Set targetSize to 1024
        {
            byte[] imageFile = StreamToByteArray(imageFileStream);
            using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
            {
                Size newSize = CalculateDimensions(oldImage.Size, targetSize);
                using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
                {
                    using (Graphics canvas = Graphics.FromImage(newImage))
                    {
                        canvas.SmoothingMode = SmoothingMode.AntiAlias;
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
                        MemoryStream m = new MemoryStream();
                        newImage.Save(m, ImageFormat.Jpeg);
                        return new MemoryStream(m.GetBuffer());
                    }
                }
            }
        }

        public static byte[] StreamToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static Stream GifImageFileWithoutCompression(Stream imageFileStream) // Set targetSize to 1024
        {
            byte[] imageFile = StreamToByteArray(imageFileStream);
            using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
            {
                using (Bitmap newImage = new Bitmap(oldImage.Width, oldImage.Height, PixelFormat.Format24bppRgb))
                {
                    MemoryStream m = new MemoryStream();
                    newImage.Save(m, ImageFormat.Gif);
                    return new MemoryStream(m.GetBuffer());

                }
            }
        }

        public static Size CalculateDimensions(Size oldSize, int targetSize)
        {
            Size newSize = new Size();
            if (oldSize.Height > oldSize.Width)
            {
                newSize.Width = (int)(oldSize.Width * ((float)targetSize / (float)oldSize.Height));
                newSize.Height = targetSize;
            }
            else
            {
                newSize.Width = targetSize;
                newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
            }
            return newSize;
        }

    }
}