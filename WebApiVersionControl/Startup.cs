using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApiVersionControl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //api版本控制
            services.AddApiVersioning(option => {
                // 当设置为true时，API将返回响应标头中支持的版本信息。
                option.ReportApiVersions = true;
                //此选项将用于不提供版本的请求。默认情况下, 假定的 API 版本为1.0。
                option.AssumeDefaultVersionWhenUnspecified = true;
                // via http headers
                option.ApiVersionReader = new HeaderApiVersionReader("api-version");
                // 响应标头中提供版本信息 当使用header的情况下不支持
                option.ApiVersionReader = new QueryStringApiVersionReader(parameterNames: "version");
                //此选项用于指定在请求中未指定版本时要使用的默认 API 版本。这将默认版本为1.0。
                option.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
