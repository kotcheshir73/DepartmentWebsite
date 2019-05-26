﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcademicYearImplementations.Implementations;
using AcademicYearInterfaces.Interfaces;
using AuthenticationImplementations.Implementations;
using AuthenticationInterfaces.Interfaces;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

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
            // Could be used to register more types
            container.RegisterType<IClassroomService, ClassroomService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDisciplineBlockService, DisciplineBlockService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDisciplineService, DisciplineService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEducationDirectionService, EducationDirectionService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILecturerPostSerivce, LecturerPostSerivce>(new HierarchicalLifetimeManager());
            container.RegisterType<ILecturerService, LecturerService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStudentGroupService, StudentGroupService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStudentService, StudentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStudentOrderService, StudentOrderService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStudentOrderBlockService, StudentOrderBlockService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStudentOrderBlockStudentService, StudentOrderBlockStudentService>(new HierarchicalLifetimeManager());

            container.RegisterType<IAcademicYearService, AcademicYearService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAcademicPlanService, AcademicPlanService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAcademicPlanRecordService, AcademicPlanRecordService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAcademicPlanRecordElementService, AcademicPlanRecordElementService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAcademicPlanRecordMissionService, AcademicPlanRecordMissionService>(new HierarchicalLifetimeManager());
            container.RegisterType<IContingentService, ContingentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDisciplineTimeDistributionService, DisciplineTimeDistributionService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDisciplineTimeDistributionRecordService, DisciplineTimeDistributionRecordService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDisciplineTimeDistributionClassroomService, DisciplineTimeDistributionClassroomService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILecturerWorkloadService, LecturerWorkloadService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISeasonDatesService, SeasonDatesService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStreamLessonService, StreamLessonService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStreamLessonRecordService, StreamLessonRecordService>(new HierarchicalLifetimeManager());
            container.RegisterType<ITimeNormService, TimeNormService>(new HierarchicalLifetimeManager());

            container.RegisterType<IConsultationRecordService, ConsultationRecordService>(new HierarchicalLifetimeManager());
            container.RegisterType<IExaminationRecordService, ExaminationRecordService>(new HierarchicalLifetimeManager());
            container.RegisterType<IOffsetRecordService, OffsetRecordService>(new HierarchicalLifetimeManager());
            container.RegisterType<ISemesterRecordService, SemesterRecordService>(new HierarchicalLifetimeManager());
            container.RegisterType<IScheduleLessonTimeService, ScheduleLessonTimeService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStreamingLessonService, StreamingLessonService>(new HierarchicalLifetimeManager());

            //container.RegisterType<IStatementService, StatementService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IStatementRecordService, StatementRecordService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IStatementRecordExtendedService, StatementRecordExtendedService>(new HierarchicalLifetimeManager());

            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoleService, RoleService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccessService, AccessService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuthenticationProcess, AuthenticationProcess>(new HierarchicalLifetimeManager());

            //container.RegisterType<IIndividualPlanTitleService, IndividualPlanTitleService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IIndividualPlanRecordService, IndividualPlanRecordService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IIndividualPlanKindOfWorkService, IndividualPlanKindOfWorkService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IIndividualPlanNIRScientificArticleService, IndividualPlanNIRScientificArticleService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IIndividualPlanNIRContractualWorkService, IndividualPlanNIRContractualWorkService>(new HierarchicalLifetimeManager());

            container.RegisterType<IProcess, Process>(new HierarchicalLifetimeManager());
            container.RegisterType<IScheduleProcess, ScheduleProcess>(new HierarchicalLifetimeManager());
            container.RegisterType<IAcademicYearProcess, AcademicYearProcess>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuthenticationProcess, AuthenticationProcess>(new HierarchicalLifetimeManager());

            container.RegisterType<ICommentService, CommentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEventService, EventService>(new HierarchicalLifetimeManager());
            container.RegisterType<IWebProcessService, WebProcessService>(new HierarchicalLifetimeManager());
        }
    }
}