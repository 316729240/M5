﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MWMS.Template;
namespace M5
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.Run(async (context) =>
            {
                await MWMS_Rewrite(context);
            });
        }
        public static  async Task MWMS_Rewrite(HttpContext context)
        {
            UrlMapping u1 = new UrlMapping();
            u1.Path = "2222";
            Uri url = new Uri(context.Request.Scheme+"://"+context.Request.Host+context.Request.Path);
            WebUri u = new WebUri()
            {
                MainMobileUrl="",
            };
            u.AddMapping(new UrlMapping()
            {
                Path = "/acf/",
                PcDomain = "/pc2/",
                MobileDomain = "/m/"
            });
            u.AddMapping(new UrlMapping() {
                    Path="/",
                    PcDomain="/",
                    MobileDomain="/m/"
            });
            u.AddMapping(new UrlMapping()
            {
                Path = "mwms",
                PcDomain = "www.mwms4.com",
                MobileDomain = "m.mwms4.com"
            });
            RequestUrl requestUrl =u.Build(url, context.Request.Headers["User-Agent"]);
            if(requestUrl.IsMobileBrowser && !requestUrl.IsMobileUrl)
            {
                //跳转至手机地址
            } 
           context.Response.WriteAsync("");
            
        }
    }
}
