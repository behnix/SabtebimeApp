using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace App.Core.Middlewares
{
    public class AppOfflineMiddleware
    {
        private bool _fileExist;
        private string _fileContents;
        private readonly RequestDelegate _next;

        public AppOfflineMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            var filePath = env.WebRootPath + "\\App_Offline.htm";
            _fileExist = File.Exists(filePath);
            if (_fileExist)
            {
                _fileContents = File.ReadAllText(filePath);
            }
        }

        public async Task Invoke(HttpContext context)
        {
            if (_fileExist)
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(_fileContents);
                return;
            }
            await _next(context);
        }
    }
}
