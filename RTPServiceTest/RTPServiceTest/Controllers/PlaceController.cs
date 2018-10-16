using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using RTPServiceTest.Models;
using System.Data;

namespace RTPServiceTest.Controllers
{
    public class PlaceController : ApiController
    {
        AmazonS3Downloader S3Download = new AmazonS3Downloader();
        RDSData rtpcls = new RDSData();
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string CategoryName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string URLS="";
            try
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                DataTable dt = new DataTable();
                dt = rtpcls.GetPlacesFromCategory(CategoryName);

                if (dt.Rows.Count > 0)
                {
                    dt.Columns.Add("ImgURL", typeof(String));

                    foreach (DataRow row in dt.Rows)
                    {
                        URLS = S3Download.GetS3ObjectsURLsByFileName("rtpplaces", row["PlaceImage"].ToString());
                        //need to set value to NewColumn column
                        row["ImgURL"] = URLS;   // or set it to some other value
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, dt);
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
    }
}
