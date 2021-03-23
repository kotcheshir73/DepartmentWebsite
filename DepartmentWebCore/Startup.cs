using AcademicYearImplementations.Implementations;
using AcademicYearInterfaces.Interfaces;
using AuthenticationImplementations.Implementations;
using AuthenticationInterfaces.Interfaces;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using DepartmentWebCore.Models;
using DepartmentWebCore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleImplementations.Services;
using ScheduleInterfaces.Interfaces;
using Unity;
using Unity.Lifetime;
using WebImplementations.Implementations;
using WebInterfaces.Interfaces;

namespace DepartmentWebCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.IsEssential = true;
            });

            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new PathString("/Account/Login");
                });

            // добавление кэширования
            services.AddMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.Configure<CustonConfig>(Configuration.GetSection("CustonConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<IWebEducationDirectionService, WebEducationDirectionService>(new HierarchicalLifetimeManager());
            container.RegisterType<IWebDisciplineService, WebDisciplineService>(new HierarchicalLifetimeManager());
            container.RegisterType<IWebLecturerService, WebLecturerService>(new HierarchicalLifetimeManager());
            container.RegisterType<IWebStudentGroupService, WebStudentGroupService>(new HierarchicalLifetimeManager());
            container.RegisterType<INewsService, NewsService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICommentService, CommentService>(new HierarchicalLifetimeManager());
            container.RegisterType<FileService, FileService>(new HierarchicalLifetimeManager());

            container.RegisterType<IClassroomService, ClassroomService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDisciplineService, DisciplineService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDisciplineBlockService, DisciplineBlockService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEducationDirectionService, EducationDirectionService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILecturerService, LecturerService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILecturerPostSerivce, LecturerPostSerivce>(new HierarchicalLifetimeManager());
            container.RegisterType<IStudentGroupService, StudentGroupService>(new HierarchicalLifetimeManager());

            container.RegisterType<IAuthenticationProcess, AuthenticationProcess>(new HierarchicalLifetimeManager());

            container.RegisterType<IConsultationRecordService, ConsultationRecordService>(new HierarchicalLifetimeManager());
            container.RegisterType<IExaminationRecordService, ExaminationRecordService>(new HierarchicalLifetimeManager());
            container.RegisterType<IOffsetRecordService, OffsetRecordService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISemesterRecordService, SemesterRecordService>(new HierarchicalLifetimeManager());
            container.RegisterType<IScheduleProcess, ScheduleProcess>(new HierarchicalLifetimeManager());

            container.RegisterType<IWebStudyProcessService, WebStudyProcessService>(new HierarchicalLifetimeManager());

            container.RegisterType<BaseService>(new HierarchicalLifetimeManager());
        }
    }
}
