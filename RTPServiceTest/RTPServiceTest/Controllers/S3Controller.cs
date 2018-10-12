using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Data;
using RTPServiceTest.Models;

namespace RTPServiceTest.Controllers
{
    public class S3Controller : ApiController
    {

        //GET api/S3
        public async Task<HttpResponseMessage> Get()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                AmazonS3Downloader S3Download = new AmazonS3Downloader();
                List<string> URLS = new List<string>();
                URLS = S3Download.GetS3ObjectsURLs();

                if (URLS.Count() > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, URLS);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Records found");

                }

            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }


        public async Task<HttpResponseMessage> GetEmployeeFiles(int EmployeeID)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                List<string> URLS = new List<string>();
                AmazonS3Downloader S3Download = new AmazonS3Downloader();


                RDSData rds = new RDSData();
                DataTable dt = new DataTable();
                dt = rds.SP_GetFileByEmployee(EmployeeID);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        URLS.Add(dt.Rows[i]["FileName"].ToString());
                    }

                    URLS = S3Download.GetS3ObjectsURLsByEmployeeID(URLS);

                    if (URLS.Count() > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, URLS);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Records found");

                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Records found");
                }

            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }

        // POST api/values
        [AllowAnonymous]
        public async Task<HttpResponseMessage> PostFileByEmployee(int EmpId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                //HttpPostedFileBase postedfile = Request.File
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {



                            // var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension);

                            //postedFile.SaveAs(filePath);


                            AmazonS3Uploader S3Upload = new AmazonS3Uploader();
                            //string msg = S3Upload.UploadFile(filePath, postedFile.FileName);

                            HttpPostedFile imagefile = httpRequest.Files[file];
                            string msg = S3Upload.S3UploadSingleImage(imagefile, postedFile.FileName);

                            if (msg == "1")
                            {
                                RDSData rds = new RDSData();
                                string bucketName = "vimalarockia83/images1";
                                int val = rds.SP_SaveFileDetails(EmpId, postedFile.FileName, bucketName);
                                var message1 = string.Format("Image Updated Successfully.");
                                return Request.CreateResponse(HttpStatusCode.Created, message1);
                            }
                            else
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, msg);
                            }

                        }
                    }


                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", ex.StackTrace.ToString());
                //dict.Add("error", ex.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Post()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                //HttpPostedFileBase postedfile = Request.File
                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Please Upload a file upto 1 mb.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {



                            // var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension);

                            //postedFile.SaveAs(filePath);


                            AmazonS3Uploader S3Upload = new AmazonS3Uploader();
                            //string msg = S3Upload.UploadFile(filePath, postedFile.FileName);

                            HttpPostedFile imagefile = httpRequest.Files[file];
                            string msg = S3Upload.S3UploadSingleImage(imagefile, postedFile.FileName);

                            if (msg == "1")
                            {
                                var message1 = string.Format("Image Updated Successfully.");
                                return Request.CreateResponse(HttpStatusCode.Created, message1);
                            }
                            else
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, msg);
                            }

                        }
                    }


                }
                var res = string.Format("Please Upload a image.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("some Message");
                dict.Add("error", ex.StackTrace.ToString());
                //dict.Add("error", ex.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }
    }
}
