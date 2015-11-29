using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1;
using WebApplication1.Models;

namespace WebApplication1.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void StartDownload()
        {
            var c = new UrlModel() { Site = @"http://www.yandex.ru" };
            Downloader.CreateNew(c);
            var task = Downloader.StartNew(c.ID);
            Assert.IsTrue(task.Result.Length > 0);
        }

    }
}
