using DepartmentService.Context;
using DepartmentService.IServices;
using DepartmentService.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace DepartmentDesktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            IAdministrationProcess administrationProcess = container.Resolve<IAdministrationProcess>();
            var result = administrationProcess.CheckExsistData();
            if (!result.Succeeded)
            {
                PrintErrorMessage("Не удалось восстановить данные. {0}", result.Errors);
            }

			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			AuthorizationService.Login("admin", "qwerty");

            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, DepartmentDbContext>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IEducationalProcessService, EducationalProcessService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IClassroomService, ClassroomService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IEducationDirectionService, EducationDirectionService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IDisciplineBlockService, DisciplineBlockService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IDisciplineService, DisciplineService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISeasonDatesService, SeasonDatesService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerPostSerivce, LecturerPostSerivce>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerService, LecturerService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentGroupService, StudentGroupService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentService, StudentService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IStudentMoveService, StudentMoveService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStreamingLessonService, StreamingLessonService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IScheduleService, ScheduleService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISemesterRecordService, SemesterRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOffsetRecordService, OffsetRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationRecordService, ExaminationRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IConsultationRecordService, ConsultationRecordService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IScheduleLessonTimeService, ScheduleLessonTimeService>(new HierarchicalLifetimeManager());

			currentContainer.RegisterType<IKindOfLoadService, KindOfLoadService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IContingentService, ContingentService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ITimeNormService, TimeNormService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IAcademicYearService, AcademicYearService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IAcademicPlanService, AcademicPlanService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IAcademicPlanRecordService, AcademicPlanRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAcademicPlanRecordElementService, AcademicPlanRecordElementService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILoadDistributionService, LoadDistributionService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ILoadDistributionRecordService, LoadDistributionRecordService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ILoadDistributionMissionService, LoadDistributionMissionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStreamLessonService, StreamLessonService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStreamLessonRecordService, StreamLessonRecordService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IRoleService, RoleService>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IAccessService, AccessService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IMaterialTechnicalValueService, MaterialTechnicalValueService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialTechnicalValueGroupService, MaterialTechnicalValueGroupService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialTechnicalValueRecordService, MaterialTechnicalValueRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISoftwareRecordService, SoftwareRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILaboratoryProcess, LaboratoryProcess>(new HierarchicalLifetimeManager());


            currentContainer.RegisterType<IAdministrationProcess, AdministrationProcess>(new HierarchicalLifetimeManager());

            currentContainer
        .RegisterType<FormMain>()
        .RegisterInstance<IUnityContainer>(currentContainer);
            return currentContainer;
        }

		public static void PrintErrorMessage(string text, List<KeyValuePair<string, string>> result)
		{
			FormError form = new FormError();
			form.LoadData(text, result);
		}
    }
}
