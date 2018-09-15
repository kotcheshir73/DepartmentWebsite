using DepartmentModel;
using DepartmentModel.Enums;
using DepartmentService.BindingModels;
using DepartmentService.ViewModels;
using System;
using System.Collections.Generic;

namespace DepartmentService.IServices
{
    public interface ILearningProgressProcess
    {
        ResultService<Guid> GetCurrentAcademicYear();

        ResultService<List<LearningProcessDisciplineViewModel>> GetDisciplines(LearningProcessDisciplineBindingModel model);

        ResultService<List<LearningProcessDisciplineDetailViewModel>> GetDisciplineDetails(LearningProcessDisciplineDetailBindingModel model);

        ResultService FormDisciplineLessons(LearningProcessFormDisciplineLessonsBindingModel model);

        ResultService FormDisciplineLessonTasks(LearningProcessFormDisciplineLessonTasksBindingModel model);

        ResultService FormDisciplineLessonVariants(LearningProcessFormDisciplineLessonTaskVariantsBindingModel model);

        ResultService<List<DisciplineLessonTaskVariantViewModel>> GetDisciplineLessonTaskVariants(GetDisciplineLessonTaskVariants model);

        ResultService<List<DisciplineLessonTaskViewModel>> GetDisiplineLessonTasksForDuplicate(GetDisiplineLessonTasksForDuplicate model);

        ResultService DuplicateDisiplineLessonTasks(DuplicateDisiplineLessonTasks model);

        ResultService<List<DisciplineLessonViewModel>> GetDisiplineLessonsForDuplicate(GetDisiplineLessonsForDuplicate model);

        ResultService DuplicateDisiplineLessons(DuplicateDisiplineLessons model);

        ResultService<List<Semesters>> GetSemesters(LearningProcessSemesterBindingModel model);

        ResultService<List<StudentGroupViewModel>> GetStudentGroups(LearningProcessStudentGroupBindingModel model);

        ResultService<List<DisciplineStudentRecordViewModel>> GetDisciplineStudentRecordsForFill(DisciplineStudentRecordsForFill model);

        ResultService<List<string>> GetDisciplineLessonSubgroup(DisciplineLessonSubgroup model);

        ResultService<List<DisciplineLessonConductedStudentViewModel>> GetDisciplineLessonConductedStudentsForFill(DisciplineLessonConductedStudentsForFill model);
    }
}
