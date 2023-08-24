using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SignalRApi.DAL;
using SignalRApi.Hubs;
using SignalRApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApi
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
            services.AddScoped<VisitorService>();
            services.AddSignalR();

            //AddPolicy metodu ile cors policy oluþturuldu. Bu policy aþaðýda Configure içerisinde çaðrýlmasý gerekmektedir.
            services.AddCors(options => options.AddPolicy("CorsPolicy", //Consume üzerinden server ý tüketmemize olanak saðlayan metot.
                builder =>
                {
                    builder.AllowAnyHeader()     //Dýþarýdan herhangi bir baþlýðýn gelmesine izin ver.
                    .AllowAnyMethod()      //Dýþarýdan herhangi bir metodun gelmesine izin ver.
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials();
                }));

            services.AddEntityFrameworkNpgsql().AddDbContext<ApiAppContext>(opt =>
            opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SignalRApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SignalRApi v1"));
            }

            app.UseRouting();
            app.UseCors("CorsPolicy"); // Yukarýda oluþturduðumuz corspolicy burada çaðrýldý.
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<VisitorHub>("/VisitorHub");   //Nereyi tüketeceðimiz burada belirtilir.Syntax tam olarak bu þekilde.
            });
        }
    }
}
