﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using Amazon.S3;
using Amazon.S3.Model;
//using urNotice.Common.Infrastructure.Common.Config;
//using urNotice.Common.Infrastructure.Model.urNoticeModel.ResponseWrapper;
//using urNotice.Services.ErrorLogger;

namespace AmazonS3UploadAndDownloadAPI.Models
{
    public class S3ImageUploadServices
    {
        //private static readonly ILogger Logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        //private static readonly string _awsAccessKey = AwsConfig._awsAccessKey;
        //private static readonly string _awsSecretKey = AwsConfig._awsSecretKey;
        //private static readonly string _bucketName = AwsConfig._bucketName;
        //private static readonly string _amazonS3PublicUrl = AwsConfig._amazonS3PublicUrl;

        //public static ImgurImageResponse UploadMultipleImagesToImgur(IEnumerable<HttpPostedFileBase> files, string albumid)
        //{


        //    var fileName = Guid.NewGuid().ToString();
        //    var path = albumid + "/" + fileName;
        //    var imgurImage = new ImgurImageResponse();
        //    foreach (HttpPostedFileBase file in files)
        //    {

        //        try
        //        {
        //            path = path + "." + file.FileName.Split('.').Last();
        //            IAmazonS3 client;
        //            Stream inputSteram = ResizeImageFile(file.InputStream, 1024);
        //            using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(_awsAccessKey, _awsSecretKey, Amazon.RegionEndpoint.APSoutheast1))
        //            {
        //                var request = new PutObjectRequest()
        //                {
        //                    BucketName = _bucketName,
        //                    CannedACL = S3CannedACL.PublicRead,//PERMISSION TO FILE PUBLIC ACCESIBLE
        //                    Key = string.Format(path),
        //                    InputStream = inputSteram//SEND THE FILE STREAM
        //                };

        //                client.PutObject(request);
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            Logger.Error("Exception Occured while uploading to Amazon S3 : " + ex, ex);
        //        }

        //        imgurImage.data = new imgurData();

        //        imgurImage.data.link = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //        imgurImage.data.link_s = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //        imgurImage.data.link_m = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //        imgurImage.data.link_l = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //        imgurImage.data.copyText = "";

        //    }
        //    return imgurImage;
        //}

        //public static ImgurImageResponse UploadSingleImageToImgur(HttpPostedFileBase file, string albumid)
        //{


        //    var fileName = Guid.NewGuid().ToString();
        //    var path = albumid + "/" + fileName;
        //    var imgurImage = new ImgurImageResponse();

        //    try
        //    {
        //        path = path + "." + file.FileName.Split('.').Last();
        //        IAmazonS3 client;
        //        Stream inputSteram = null;

        //        if (file.FileName.Split('.')[1] == "gif")
        //        {
        //            inputSteram = file.InputStream;
        //        }
        //        else
        //        {
        //            inputSteram = ResizeImageFile(file.InputStream, 1024);
        //        }

        //        using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(_awsAccessKey, _awsSecretKey, Amazon.RegionEndpoint.APSoutheast1))
        //        {
        //            var request = new PutObjectRequest()
        //            {
        //                BucketName = _bucketName,
        //                CannedACL = S3CannedACL.PublicRead,//PERMISSION TO FILE PUBLIC ACCESIBLE
        //                Key = string.Format(path),
        //                InputStream = inputSteram,//SEND THE FILE STREAM,                            
        //            };


        //            if (file.FileName.Split('.')[1] == "gif")
        //            {
        //                request.ContentType = "image/gif";
        //            }

        //            client.PutObject(request);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Logger.Error("Exception Occured while uploading to Amazon S3 : " + ex, ex);
        //    }

        //    imgurImage.data = new imgurData();

        //    imgurImage.data.link = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //    imgurImage.data.link_s = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //    imgurImage.data.link_m = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //    imgurImage.data.link_l = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //    imgurImage.data.copyText = "";


        //    return imgurImage;
        //}

        //public static byte[] StreamToByteArray(Stream input)
        //{
        //    byte[] buffer = new byte[16 * 1024];
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        int read;
        //        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            ms.Write(buffer, 0, read);
        //        }
        //        return ms.ToArray();
        //    }
        //}

        //public static Stream GifImageFileWithoutCompression(Stream imageFileStream) // Set targetSize to 1024
        //{
        //    byte[] imageFile = StreamToByteArray(imageFileStream);
        //    using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
        //    {
        //        using (Bitmap newImage = new Bitmap(oldImage.Width, oldImage.Height, PixelFormat.Format24bppRgb))
        //        {
        //            MemoryStream m = new MemoryStream();
        //            newImage.Save(m, ImageFormat.Gif);
        //            return new MemoryStream(m.GetBuffer());

        //        }
        //    }
        //}

        //public static Stream ResizeImageFile(Stream imageFileStream, int targetSize) // Set targetSize to 1024
        //{
        //    byte[] imageFile = StreamToByteArray(imageFileStream);
        //    using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
        //    {
        //        Size newSize = CalculateDimensions(oldImage.Size, targetSize);
        //        using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
        //        {
        //            using (Graphics canvas = Graphics.FromImage(newImage))
        //            {
        //                canvas.SmoothingMode = SmoothingMode.AntiAlias;
        //                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //                canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //                canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
        //                MemoryStream m = new MemoryStream();
        //                newImage.Save(m, ImageFormat.Jpeg);
        //                return new MemoryStream(m.GetBuffer());
        //            }
        //        }
        //    }
        //}

        //public static Size CalculateDimensions(Size oldSize, int targetSize)
        //{
        //    Size newSize = new Size();
        //    if (oldSize.Height > oldSize.Width)
        //    {
        //        newSize.Width = (int)(oldSize.Width * ((float)targetSize / (float)oldSize.Height));
        //        newSize.Height = targetSize;
        //    }
        //    else
        //    {
        //        newSize.Width = targetSize;
        //        newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
        //    }
        //    return newSize;
        //}

        //public static ImgurImageResponse UploadSingleImageToS3FromPath(String imagePath, string albumid, String fileName)
        //{


        //    //var fileName = values[1].Replace(" ", "_") + "_image.png";
        //    var path = albumid + "/" + fileName;
        //    var imgurImage = new ImgurImageResponse();

        //    try
        //    {
        //        //path = path + "." + imageType;

        //        byte[] photo = File.ReadAllBytes(imagePath);
        //        IAmazonS3 client;
        //        using (client = Amazon.AWSClientFactory.CreateAmazonS3Client(_awsAccessKey, _awsSecretKey, Amazon.RegionEndpoint.APSoutheast1))
        //        {
        //            var request = new PutObjectRequest()
        //            {
        //                BucketName = _bucketName,
        //                CannedACL = S3CannedACL.PublicRead,//PERMISSION TO FILE PUBLIC ACCESIBLE
        //                Key = string.Format(path),
        //                InputStream = new MemoryStream(photo)//SEND THE FILE STREAM
        //            };

        //            client.PutObject(request);
        //            if (File.Exists(@imagePath))
        //            {
        //                File.Delete(@imagePath);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Logger.Error("Exception Occured while uploading to Amazon S3 : " + ex, ex);
        //    }

        //    imgurImage.data = new imgurData();

        //    imgurImage.data.link = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //    imgurImage.data.link_s = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //    imgurImage.data.link_m = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //    imgurImage.data.link_l = _amazonS3PublicUrl + "/" + _bucketName + "/" + path;
        //    imgurImage.data.copyText = "";


        //    return imgurImage;
        //}
      
        //public static String SaveImageOnServer(String imageUrl, String saveLocation)
        //{
        //    byte[] imageBytes;
        //    HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(imageUrl);
        //    WebResponse imageResponse = imageRequest.GetResponse();

        //    Stream responseStream = imageResponse.GetResponseStream();

        //    using (BinaryReader br = new BinaryReader(responseStream))
        //    {
        //        imageBytes = br.ReadBytes(500000);
        //        br.Close();
        //    }
        //    responseStream.Close();
        //    imageResponse.Close();

        //    FileStream fs = new FileStream(saveLocation, FileMode.Create);
        //    BinaryWriter bw = new BinaryWriter(fs);
        //    try
        //    {
        //        bw.Write(imageBytes);
        //    }
        //    finally
        //    {
        //        fs.Close();
        //        bw.Close();
        //    }

        //    return saveLocation;
        //}
    }
}