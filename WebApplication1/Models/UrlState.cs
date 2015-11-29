using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Net;

namespace WebApplication1.Models
{
    public class UrlModel
    {
        [Display(Name = "URL")]
        public string Site { get; set; }
        public Guid ID { get; set; }
        public string Content { get; set; }
    }
    public class TaskModel
    {
        [Display(Name = "URL")]
        public string Site { get; set; }
        public Guid ID { get; set; }
        public WebClient client { get; set; }
        public TaskModel(UrlModel model)
        {
            Site = model.Site;
            ID = model.ID;
        }
    }
    public class Downloader
    {
        private static ConcurrentDictionary<Guid, TaskModel> DownloaderManager 
            = new ConcurrentDictionary<Guid, TaskModel>();
       
        public static void CreateNew(UrlModel model)
        {
            model.ID = Guid.NewGuid();
            var taskmodel = new TaskModel(model);
            taskmodel.client = new WebClient();
            DownloaderManager.TryAdd(model.ID, taskmodel);
        }

        public static async Task<string> StartNew(Guid Id)
        {
            TaskModel taskmodel;
            DownloaderManager.TryRemove(Id, out taskmodel);
            var client = taskmodel.client;
            var uri = new Uri(taskmodel.Site);
            taskmodel = null;
            return await client.DownloadStringTaskAsync(uri);
        } 

        public static UrlModel Cancel(UrlModel model)
        {
            TaskModel taskmodel;
            DownloaderManager.TryGetValue(model.ID, out taskmodel);
            if(taskmodel?.client!=null)
                taskmodel.client.CancelAsync();
                model.Content = "Canceled";
            
            return model;
        }

    }
}