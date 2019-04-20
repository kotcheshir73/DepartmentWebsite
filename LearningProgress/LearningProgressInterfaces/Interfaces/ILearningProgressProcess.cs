using BaseInterfaces.ViewModels;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.ViewModels;
using System;
using System.Collections.Generic;
using Tools;

namespace LearningProgressInterfaces.Interfaces
{
    public interface ILearningProgressProcess
    {
        ResultService<Guid> GetCurrentAcademicYear();

        ResultService<List<LearningProcessDisciplineViewModel>> GetDisciplines(LearningProcessDisciplineBindingModel model);

        ResultService<List<LearningProcessDisciplineDetailViewModel>> GetDisciplineDetails(LearningProcessDisciplineDetailBindingModel model);

        ResultService FormDisciplineLessons(LearningProcessFormDisciplineLessonsBindingModel model);

        ResultService FormDisciplineLessonTasks(LearningProcessFormDisciplineLessonTasksBindingModel model);

        ResultService FormDisciplineLessonVariants(LearningProcessFormDisciplineLessonTaskVariantsBindingModel model);

        ResultService<List<DisciplineLessonTaskVariantViewModel>> GetDisciplineLessonTaskVariants(GetDisciplineLessonTaskVariantsBindingModel model);

        ResultService<List<DisciplineLessonTaskViewModel>> GetDisiplineLessonTasksForDuplicate(GetDisiplineLessonTasksForDuplicateBindingModel model);

        ResultService DuplicateDisiplineLessonTasks(DuplicateDisiplineLessonTasksBindingModel model);

        ResultService<List<DisciplineLessonViewModel>> GetDisiplineLessonsForDuplicate(GetDisiplineLessonsForDuplicateBindingModel model);

        ResultService DuplicateDisiplineLessons(DuplicateDisiplineLessonsBindingModel model);

        ResultService<List<Semesters>> GetSemesters(LearningProcessSemesterBindingModel model);

        ResultService<List<StudentGroupViewModel>> GetStudentGroups(LearningProcessStudentGroupBindingModel model);

        ResultService<List<DisciplineStudentRecordViewModel>> GetDisciplineStudentRecordsForFill(DisciplineStudentRecordsForFillBindingModel model);

        ResultService<List<string>> GetDisciplineLessonSubgroup(DisciplineLessonSubgroupBindingModel model);

        ResultService<List<DisciplineLessonConductedStudentViewModel>> GetDisciplineLessonConductedStudentsForFill(DisciplineLessonConductedStudentsForFillBindingModel model);

        ResultService<List<LessonConductedViewModel>> GetLessonConducteds(LessonConductedsBindingModel model);

        ResultService<List<DisciplineLessonViewModel>> GetDisciplineLessons(LearningProcessDisciplineLessonBindingModel model);

        ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>> GetDisciplineLessonTaskStudentAcceptForForm(DisciplineLessonTaskStudentAcceptForFormBindingModel model);

        ResultService SetDisciplineLessonTaskStudentAccept(List<DisciplineLessonTaskStudentAcceptUpdateBindingModel> model);

        ResultService<List<DisciplineLessonTaskStudentAcceptViewModel>> GetDisciplineLessonTaskStudentAcceptForFill(DisciplineLessonTaskStudentAcceptForFillBindingModel model);

        ResultService<List<DisciplineLessonConductedViewModel>> GetFullDisciplineLessonConducteds(FullDisciplineLessonConductedBindingModel model);
    }
}