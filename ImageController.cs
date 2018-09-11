using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using WebAPI.Models;
using System.Web.Hosting;

namespace WebAPI.Controllers
{
    public class ImageController : ApiController
    {
        [HttpPost]
        [Route("api/UploadImage")]
        public HttpResponseMessage UploadImage()
        {
            string imageName = null;
            var httpRequest = HttpContext.Current.Request;
        //Upload image
            var postedFile = httpRequest.Files["Image"];
        //create custom filename
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ","-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
            postedFile.SaveAs(filePath);
            //save to Database
            using (DBModel db = new DBModel())
            {
                Image image = new Image()
                {
                    ImageCaption = httpRequest["ImageCaption"],
                    ImageName = imageName
                };
                db.Images.Add(image);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }
        //[HttpGet]
        //[Route("api/RetrieveImages")]
        //public HttpResponseMessage Getimgmodel()
        //{
        //    //var x="";
        //   // x = (from n in db.imgmodel where n.id == 12 select n.ImageName).FirstOrDefault();

        //    String x = (from n in db.imgmodel select n.ImageName).FirstorDefault();
        //    var res = new HttpResponseMessage(HttpStatusCode.OK);
        //    string filepath;
      
        //        filepath= HostingEnvironment.MapPath("~/Image/" + x);
           

            
        //    MemoryStream memoryStream = new MemoryStream();

            
        //        using (fileStream = new FileStream(filepath, FileMode.Open))

        //        { 
        //            Image image = Image.FromStream(fileStream);
        //            image.Save(memoryStream, ImageFormat.Jpeg);
        //            res.Content = new ByteArrayContent(memoryStream.ToArray());
        //            res.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        //        }
        //return res;

        //}

    }
}
