using CHU_PolicyPlatform_1.Data;
using CHU_PolicyPlatform_1.Repositories;
using CHU_PolicyPlatform_1.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CHU_PolicyPlatform_1
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
            services.AddControllersWithViews();





            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.LoginPath = new PathString("/User/UserLogin");
                });


            services.AddScoped<ProposalService>();
            services.AddScoped<ProposalRepository>();

            services.AddScoped<ReGerent>();
            services.AddScoped<SeGerent>();

            services.AddScoped<ReUserS>();
            services.AddScoped<SeUserS>();

            services.AddDbContext<ProposeContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ProposeContext")));

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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "/",
                //    defaults: new { controller = "Home", action = "Index" });
                
                //Home
                endpoints.MapControllerRoute(
                    name: "HomePagination",
                    pattern: "Home/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
                //Search
                endpoints.MapControllerRoute(
                    name: "HomeSearchByKeyword",
                    pattern: "Home/Search/{keyword?}/{Id?}",
                    defaults: new { controller = "Home", action = "Privacy" });
                //Review
                endpoints.MapControllerRoute(
                    name: "Review",
                    pattern: "Review/{Pro_Id?}/{Id?}",
                    defaults: new { controller = "Review", action = "Review_Interface" });
                //propose
                endpoints.MapControllerRoute(
                    name: "AdvanceProposal",
                    pattern: "Propose",
                    defaults: new { controller = "Proposals", action = "Advance" });
                //vote
                endpoints.MapControllerRoute(
                    name: "ProposalToVote",
                    pattern: "Review/Vote/{propId?}/{vote?}",
                    defaults: new { controller = "Vote", action = "ProposeVote" });
                //response
                endpoints.MapControllerRoute(
                    name: "ProposalToResponse",
                    pattern: "Response/{propId?}",
                    defaults: new { controller = "Response", action = "GerentResponse" });
                //Login_user
                endpoints.MapControllerRoute(
                    name: "LoginOfUser",
                    pattern: "Login/User",
                    defaults: new { controller = "UserLogin", action = "User" });
                //Login_gerent
                endpoints.MapControllerRoute(
                    name: "LoginOfGerent",
                    pattern: "Login/Gerent",
                    defaults: new { controller = "GerentLogin", action = "Gerent" });
                //makevarious
                endpoints.MapControllerRoute(
                    name: "Variousrooms",
                    pattern: "Department",
                    defaults: new { controller = "Home", action = "Variousrooms" });
                //User_Supervision
                endpoints.MapControllerRoute(
                    name: "UseSupervision",
                    pattern: "Supervision/Pending/{id?}",
                    defaults: new { controller = "UserSupervision", action = "UserPending" });
                //User_Supervision
                endpoints.MapControllerRoute(
                    name: "UserSupervision",
                    pattern: "Supervision/Finished/{id?}",
                    defaults: new { controller = "UserSupervision", action = "UserFinished" });
                //User_Supervision
                endpoints.MapControllerRoute(
                    name: "UserSupervision",
                    pattern: "Supervision/Fail/{id?}",
                    defaults: new { controller = "UserSupervision", action = "UserFail" });
                //Gerent_See
                endpoints.MapControllerRoute(
                    name: "GerentSee",
                    pattern: "Gerent/Pending/{id?}",
                    defaults: new { controller = "Gerentcase", action = "GerentSee" });
                //Gerent_See
                endpoints.MapControllerRoute(
                    name: "GerentSee",
                    pattern: "Gerent/Finished/{id?}",
                    defaults: new { controller = "Gerentcase", action = "GerentSeeFinish" });
                //Failure_page
                endpoints.MapControllerRoute(
                    name: "Failurepage",
                    pattern: "Error",
                    defaults: new { controller = "Home", action = "Failurepage" });
                //Error_Home
                endpoints.MapControllerRoute(
                    name: "Errorhomepage",
                    pattern: "NullCase",
                    defaults: new { controller = "Home", action = "Errorhomepage" });
                //Error_User_Supervision
                endpoints.MapControllerRoute(
                    name: "UseSupervision",
                    pattern: "Supervision/NullPending/{id?}",
                    defaults: new { controller = "UserSupervision", action = "ErrorUserPending" });
                //Error_User_Supervision
                endpoints.MapControllerRoute(
                    name: "UserSupervision",
                    pattern: "Supervision/NullFinished/{id?}",
                    defaults: new { controller = "UserSupervision", action = "ErrorUserFinished" });
                //Error_User_Supervision
                endpoints.MapControllerRoute(
                    name: "UserSupervision",
                    pattern: "Supervision/NullFail/{id?}",
                    defaults: new { controller = "UserSupervision", action = "ErrorUserFail" });
                //Error_Gerent_See
                endpoints.MapControllerRoute(
                    name: "GerentSee",
                    pattern: "Gerent/NullPending/{id?}",
                    defaults: new { controller = "Gerentcase", action = "ErrorGerentSee" });
                //Error_Gerent_See
                endpoints.MapControllerRoute(
                    name: "GerentSee",
                    pattern: "Gerent/NullFinished/{id?}",
                    defaults: new { controller = "Gerentcase", action = "ErrorGerentSeeFinish" });

            });
        }
    }
}
