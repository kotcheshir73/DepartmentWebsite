using Enums;
using Interfaces.BindingModels;
using Models.Base;
using System;

namespace Implementations
{
    public static class ModelFacotryFromBindingModel
    {
        public static EducationDirection CreateEducationDirection(EducationDirectionSetBindingModel model, EducationDirection entity = null)
        {
            if (entity == null)
            {
                entity = new EducationDirection();
            }
            entity.Cipher = model.Cipher;
            entity.ShortName = model.ShortName;
            entity.Description = model.Description;
            entity.Title = model.Title;

            return entity;
        }

        public static DisciplineBlock CreateDisciplineBlock(DisciplineBlockSetBindingModel model, DisciplineBlock entity = null)
        {
            if (entity == null)
            {
                entity = new DisciplineBlock();
            }
            entity.Title = model.Title;
            entity.DisciplineBlockBlueAsteriskName = model.DisciplineBlockBlueAsteriskName;
            entity.DisciplineBlockUseForGrouping = model.DisciplineBlockUseForGrouping;
            entity.DisciplineBlockOrder = model.DisciplineBlockOrder;

            return entity;
        }

        public static LecturerPost CreateLecturerPost(LecturerPostSetBindingModel model, LecturerPost entity = null)
        {
            if (entity == null)
            {
                entity = new LecturerPost();
            }
            entity.PostTitle = model.PostTitle;
            entity.Hours = model.Hours;

            return entity;
        }

        public static Classroom CreateClassroom(ClassroomSetBindingModel model, Classroom entity = null)
        {
            if (entity == null)
            {
                entity = new Classroom();
            }
            entity.Number = model.Number;
            entity.Capacity = model.Capacity;
            entity.ClassroomType = (ClassroomTypes)Enum.Parse(typeof(ClassroomTypes), model.ClassroomType);
            entity.NotUseInSchedule = model.NotUseInSchedule;

            return entity;
        }

        public static Discipline CreateDiscipline(DisciplineSetBindingModel model, Discipline entity = null)
        {
            if (entity == null)
            {
                entity = new Discipline();
            }
            entity.DisciplineBlockId = model.DisciplineBlockId;
            entity.DisciplineName = model.DisciplineName;
            entity.DisciplineShortName = model.DisciplineShortName;
            entity.DisciplineBlueAsteriskName = model.DisciplineBlueAsteriskName;

            return entity;
        }

        public static Lecturer CreateLecturer(LecturerSetBindingModel model, Lecturer entity = null)
        {
            if (entity == null)
            {
                entity = new Lecturer();
            }
            entity.LecturerPostId = model.LecturerPostId;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Patronymic = model.Patronymic;
            entity.Abbreviation = model.Abbreviation;
            entity.DateBirth = model.DateBirth;
            entity.Post = (Post)Enum.Parse(typeof(Post), model.Post);
            entity.Rank = (Rank)Enum.Parse(typeof(Rank), model.Rank);
            entity.Rank2 = (Rank2)Enum.Parse(typeof(Rank2), model.Rank2);
            entity.Address = model.Address;
            entity.HomeNumber = model.HomeNumber;
            entity.MobileNumber = model.MobileNumber;
            entity.Email = model.Email;
            entity.Description = model.Description;
            entity.Photo = model.Photo;

            return entity;
        }

        public static StudentGroup CreateStudentGroup(StudentGroupSetBindingModel model, StudentGroup entity = null)
        {
            if (entity == null)
            {
                entity = new StudentGroup();
            }
            entity.EducationDirectionId = model.EducationDirectionId;
            entity.GroupName = model.GroupName;
            entity.Course = (AcademicCourse)Enum.ToObject(typeof(AcademicCourse), model.Course);
            entity.CuratorId = model.CuratorId;

            return entity;
        }

        public static Student CreateStudent(StudentSetBindingModel model, Student entity = null)
        {
            if (entity == null)
            {
                entity = new Student();
            }
            entity.StudentGroupId = model.StudentGroupId;
            entity.NumberOfBook = model.NumberOfBook;
            entity.LastName = model.LastName;
            entity.FirstName = model.FirstName;
            entity.Patronymic = model.Patronymic;
            entity.Email = model.Email;
            entity.Description = model.Description;
            entity.Photo = model.Photo;
            entity.IsSteward = model.IsSteward;

            return entity;
        }

        public static StudentHistory CreateStudentHistory(StudentHistorySetBindingModel model, StudentHistory entity = null)
        {
            if (entity == null)
            {
                entity = new StudentHistory
                {
                    StudentId = model.StudetnId
                };
            }
            entity.TextMessage = model.TextMessage;

            return entity;
        }
    }
}