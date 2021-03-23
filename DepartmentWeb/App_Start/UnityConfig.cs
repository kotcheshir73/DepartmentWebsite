using AcademicYearImplementations.Implementations;
using AcademicYearInterfaces.Interfaces;
using AuthenticationImplementations.Implementations;
using AuthenticationInterfaces.Interfaces;
using BaseImplementations.Implementations;
using BaseInterfaces.Interfaces;
using ScheduleImplementations.Services;
using ScheduleInterfaces.Interfaces;
using System;
using Unity;
using Unity.Lifetime;
using WebImplementations.Implementations;
using WebInterfaces.Interfaces;

namespace DepartmentWeb
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<IClassroomService, ClassroomService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDisciplineBlockService, DisciplineBlockService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDisciplineService, DisciplineService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEducationDirectionService, EducationDirectionService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILecturerStudyPostSerivce, LecturerPostSerivce>(new HierarchicalLifetimeManager());
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

            //container.RegisterType<IStatementService, StatementService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IStatementRecordService, StatementRecordService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IStatementRecordExtendedService, StatementRecordExtendedService>(new HierarchicalLifetimeManager());

            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoleService, RoleService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccessService, AccessService>(new HierarchicalLifetimeManager());            

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
            container.RegisterType<INewsService, NewsService>(new HierarchicalLifetimeManager());
        }
    }
}