using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImgStoreOnAzure.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string fileName)
        {
            ViewBag.ImgName = TempData["image"];
            return View();
        }

        public ActionResult UploadImages(IEnumerable<HttpPostedFileBase> fileToUpload)
        {
            List<Models.ImageModel> objUploadedImg = new List<Models.ImageModel>();
            foreach (HttpPostedFileBase img in fileToUpload)
            {
                Models.ImageModel objImg = new Models.ImageModel();
                AzureHelper azureHelper = new AzureHelper();
                //Here 111 and 222 is the static value for azure path you will be change according to your requirement.
                objImg.ImageUrl = azureHelper.UploadImage(img, 111, 222);
                objUploadedImg.Add(objImg);
            }
            TempData["image"] = objUploadedImg.ToList();
            return RedirectToAction("Index", new { fileName = "" });
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}