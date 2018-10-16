using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Amazon.S3.IO;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace RTPServiceTest.Controllers
{
    
    public class HomeController : Controller
    {
        private const string bucketName = "rtpplaces";
        private const string objectKey = "vellankani_shrine.jpg";
        // Specify your bucket region (an example region is shown).
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;
        private static IAmazonS3 s3Client;
        
        public ActionResult Index()
        {
            //s3Client = new AmazonS3Client(bucketRegion);
            //string urlString = GeneratePreSignedURL();

            return View();
        }

        static string GeneratePreSignedURL()
        {
            string urlString = "";
            try
            {
                GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = objectKey,
                    Expires = DateTime.Now.AddMinutes(5)
                };
                urlString = s3Client.GetPreSignedURL(request1);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            return urlString;
        }
    }
}
