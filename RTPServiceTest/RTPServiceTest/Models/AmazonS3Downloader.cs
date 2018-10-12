using System;
using Amazon.S3;
using Amazon.S3.Model;
using System.Collections.Generic;
using Amazon.S3.IO;
using Amazon;

namespace RTPServiceTest.Models
{
    public class AmazonS3Downloader
    {
        private string bucketName = "vimalarockia83";
        static IAmazonS3 client;

        public List<String> GetS3ObjectsURLs()
        {
            List<string> S3URLs = new List<string>();

            //var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            using (client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
            {
                try
                {
                    ListObjectsV2Request request = new ListObjectsV2Request
                    {
                        BucketName = bucketName,
                        MaxKeys = 10
                    };
                    ListObjectsV2Response response;
                    do
                    {
                        response = client.ListObjectsV2(request);

                        // Process response.
                        foreach (S3Object entry in response.S3Objects)
                        {
                            GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest();
                            request1.BucketName = bucketName;
                            //request1.Key = file.Name;
                            request1.Key = entry.Key;
                            request1.Expires = DateTime.Now.AddHours(1);
                            request1.Protocol = Protocol.HTTP;
                            S3URLs.Add(client.GetPreSignedURL(request1));
                        }
                        Console.WriteLine("Next Continuation Token: {0}", response.NextContinuationToken);
                        request.ContinuationToken = response.NextContinuationToken;
                    } while (response.IsTruncated == true);

                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                        ||
                        amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    {
                        //returnmsg = "Check the provided AWS Credentials.";
                    }
                    else
                    {
                        //returnmsg = "Error occurred: " + amazonS3Exception.Message;
                    }
                }
            }
            return S3URLs;
        }

        public List<String> GetS3ObjectsURLsByEmployeeID(List<string> FileKeys)
        {
            List<string> S3URLs = new List<string>();

            //var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1);

            using (client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
            {
                try
                {
                    //ListObjectsV2Request request = new ListObjectsV2Request
                    //{
                    //    BucketName = bucketName,
                    //    MaxKeys = 10
                    //};
                    //ListObjectsV2Response response;
                    //do
                    //{
                    //    response = client.ListObjectsV2(request);

                    // Process response.
                    // foreach (S3Object entry in response.S3Objects)
                    foreach (string Key in FileKeys)
                    {
                        GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest();
                        request1.BucketName = bucketName;
                        //request1.Key = file.Name;
                        request1.Key = Key;
                        request1.Expires = DateTime.Now.AddHours(1);
                        request1.Protocol = Protocol.HTTP;
                        S3URLs.Add(client.GetPreSignedURL(request1));
                    }
                    //Console.WriteLine("Next Continuation Token: {0}", response.NextContinuationToken);
                    //request.ContinuationToken = response.NextContinuationToken;
                    //} while (response.IsTruncated == true);

                }
                catch (AmazonS3Exception amazonS3Exception)
                {
                    if (amazonS3Exception.ErrorCode != null &&
                        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                        ||
                        amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                    {
                        //returnmsg = "Check the provided AWS Credentials.";
                    }
                    else
                    {
                        //returnmsg = "Error occurred: " + amazonS3Exception.Message;
                    }
                }
            }
            return S3URLs;
        }
    }
}