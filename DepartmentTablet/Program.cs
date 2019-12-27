using AcademicYearImplementations.Implementations;
using AcademicYearInterfaces.Interfaces;
using AuthenticationImplementations.Implementations;
using AuthenticationInterfaces.Interfaces;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using DatabaseContext;
using DepartmentService.Services;
using LaboratoryHeadInterfaces.Interfaces;
using LearningProgressImplementations.Implementations;
using LearningProgressInterfaces.Interfaces;
using ScheduleImplementations.Services;
using ScheduleInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tools;
using Unity;
using Unity.Lifetime;

namespace DepartmentTablet
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            //IAdministrationProcess administrationProcess = container.Resolve<IAdministrationProcess>();
            //var result = administrationProcess.CheckExsistData();
            //if (!result.Succeeded)
            //{
            //    PrintErrorMessage("Не удалось восстановить данные. {0}", result.Errors);
            //}

            if (File.Exists("Config.conf"))
            {
                // TODO
                using (StreamReader reader = new StreamReader("Config.conf"))
                {
                    string str = null;
                    while ((str = reader.ReadLine()) != null)
                    {
                        
                    }
                }
                Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            }
            else
            {
                Font = new Font("Times New Roman", 15.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //AuthorizationService.Login("admin", "qwerty");
            DepartmentUserManager.Login("admin", "qwerty");
            Application.Run(container.Resolve<FormMain>());
        }

        public static Font Font;

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IClassroomService, ClassroomService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IEducationDirectionService, EducationDirectionService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineBlockService, DisciplineBlockService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineService, DisciplineService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerPostSerivce, LecturerPostSerivce>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILecturerService, LecturerService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentGroupService, StudentGroupService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentService, StudentService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentOrderService, StudentOrderService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentOrderBlockService, StudentOrderBlockService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStudentOrderBlockStudentService, StudentOrderBlockStudentService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<ISemesterRecordService, SemesterRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOffsetRecordService, OffsetRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IExaminationRecordService, ExaminationRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IConsultationRecordService, ConsultationRecordService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IContingentService, ContingentService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISeasonDatesService, SeasonDatesService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITimeNormService, TimeNormService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAcademicYearService, AcademicYearService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAcademicPlanService, AcademicPlanService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAcademicPlanRecordService, AcademicPlanRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAcademicPlanRecordElementService, AcademicPlanRecordElementService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStreamLessonService, StreamLessonService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStreamLessonRecordService, StreamLessonRecordService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRoleService, RoleService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAccessService, AccessService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IMaterialTechnicalValueService, MaterialTechnicalValueService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialTechnicalValueGroupService, MaterialTechnicalValueGroupService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialTechnicalValueRecordService, MaterialTechnicalValueRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISoftwareService, SoftwareService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISoftwareRecordService, SoftwareRecordService>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IDisciplineLessonService, DisciplineLessonService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineLessonTaskService, DisciplineLessonTaskService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineLessonTaskVariantService, DisciplineLessonTaskVariantService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineStudentRecordService, DisciplineStudentRecordService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineLessonConductedStudentService, DisciplineLessonConductedStudentService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineLessonConductedService, DisciplineLessonConductedService>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDisciplineLessonTaskStudentAcceptService, DisciplineLessonTaskStudentAcceptService>(new HierarchicalLifetimeManager());            

            currentContainer.RegisterType<IProcess, Process>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAuthenticationProcess, AuthenticationProcess>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IAcademicYearProcess, AcademicYearProcess>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IScheduleProcess, ScheduleProcess>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILaboratoryProcess, LaboratoryProcess>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILearningProgressProcess, LearningProgressProcess>(new HierarchicalLifetimeManager());

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
