using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DownloadSite(UrlModel model)
        {
            Downloader.CreateNew(model);

            return PartialView(model);
        }
        public ActionResult Cancel(UrlModel model)
        {
            return PartialView(Downloader.Cancel(model));
        }
        
        public async Task<string> StartDownload(Guid Id)
        {
            return await Downloader.StartNew(Id);
        }
        
    }
}