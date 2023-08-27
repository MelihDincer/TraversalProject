using BusinessLayer.Container;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TraversalCoreProje.CQRS.Handlers.DestinationHandlers;
using TraversalCoreProje.Models;

namespace TraversalCoreProje
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

            services.AddScoped<GetAllDestinationQueryHandler>();
            services.AddScoped<GetDestinationByIDQueryHandler>();
            services.AddScoped<CreateDestinationCommandHandler>();
            services.AddScoped<RemoveDestinationCommandHandler>();
            services.AddScoped<UpdateDestinationCommandHandler>();

            services.AddMediatR(typeof(Startup)); //MediatR projeye dahil edildi.
            
            services.AddDbContext<Context>(); //Proje seviyesinde Authentication uyguladýk.
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<Context>(); //Identity yapýlanmasýný eklemiþ olduk. En sona eklediðimiz AddErrorDescriber ise custom olarak oluþturduðumuz identityvalidatorunu dahil etmek için.

            services.AddHttpClient(); //HttpClient projeye dahil edildi.

            services.ContainerDependencies(); //Business katmanýnda Container klasörü içinde tanýmladýðýmýz Extensions class içerisindeki metoda direkt eriþim saðladýk.

            services.AddAutoMapper(typeof(Startup)); //AutoMapper dahil edildi.

            services.CustomerValidator(); //Extension içerisindeki customervalidator u burada çaðýrdýk. Yukarýdaki containerdependencies gibi.

            services.AddControllersWithViews().AddFluentValidation(); //FluentValidation dahil edildi.

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            
            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources"; //Dil desteðinin eklenmesi kýsmýnda, dil resource dosyalarýný "Resources" adlý klasörde aramasý gerektiðini belirttik.
            });

            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/SignIn/"; //Oturum düþtüðünde/Cookie temizlendiðinde bu sayfaya yönlendir.
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}"); //Sayfa bulunamadýðýnda bu kýsma yönlendir. Parametre alabilir.
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            //Burada, uygulama içerisinde desteklenecek dillerin etiketini/suffix/ön ek leri burada belirttik.
            var suppertedCultures = new[] { "en", "fr", "es", "gr", "tr", "de" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(suppertedCultures[4]).AddSupportedCultures(suppertedCultures).AddSupportedUICultures(suppertedCultures); //Uygulamada ilgili sayfa ayaða kalktýðýnda default olarak hangi dille ayaða kalkacaðý belirtildi. (tr) --- Ayrýca son iki metod ile birlikte, backend ve UI kýsmýna ekleme iþlemi yapýldý.
            app.UseRequestLocalization(localizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Default}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

        }
    }
}
