using Interfaces.ViewModels;
using Models.Base;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Implementations
{
    public static class ModelFactoryToViewModel
    {
        public static EducationDirectionViewModel CreateEducationDirectionViewModel(EducationDirection entity)
        {
            return new EducationDirectionViewModel
            {
                Id = entity.Id,
                Cipher = entity.Cipher,
                ShortName = entity.ShortName,
                Description = entity.Description,
                Title = entity.Title
            };
        }

        public static DisciplineBlockViewModel CreateDisciplineBlockViewModel(DisciplineBlock entity)
        {
            return new DisciplineBlockViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                DisciplineBlockBlueAsteriskName = entity.DisciplineBlockBlueAsteriskName,
                DisciplineBlockUseForGrouping = entity.DisciplineBlockUseForGrouping,
                DisciplineBlockOrder = entity.DisciplineBlockOrder
            };
        }

        public static LecturerPostViewModel CreateLecturerPostViewModel(LecturerPost entity)
        {
            return new LecturerPostViewModel
            {
                Id = entity.Id,
                PostTitle = entity.PostTitle,
                Hours = entity.Hours
            };
        }

        public static ClassroomViewModel CreateClassroomViewModel(Classroom entity)
        {
            return new ClassroomViewModel
            {
                Id = entity.Id,
                Number = entity.Number,
                ClassroomType = entity.ClassroomType.ToString(),
                Capacity = entity.Capacity,
                NotUseInSchedule = entity.NotUseInSchedule
            };
        }

        public static DisciplineViewModel CreateDisciplineViewModel(Discipline entity)
        {
            return new DisciplineViewModel
            {
                Id = entity.Id,
                DisciplineBlockId = entity.DisciplineBlockId,
                DisciplineName = entity.DisciplineName,
                DisciplineShortName = entity.DisciplineShortName,
                DisciplineBlockTitle = entity.DisciplineBlock.Title,
                DisciplineBlueAsteriskName = entity.DisciplineBlueAsteriskName
            };
        }

        public static LecturerViewModel CreateLecturerViewModel(Lecturer entity)
        {
            return new LecturerViewModel
            {
                Id = entity.Id,
                LecturerPostId = entity.LecturerPostId,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Patronymic = entity.Patronymic,
                Abbreviation = entity.Abbreviation,
                DateBirth = entity.DateBirth,
                Post = entity.Post.ToString(),
                LecturerPost = entity.LecturerPost.PostTitle,
                Rank = entity.Rank.ToString(),
                Rank2 = entity.Rank2.ToString(),
                HomeNumber = entity.HomeNumber,
                MobileNumber = entity.MobileNumber,
                Email = entity.Email,
                Address = entity.Address,
                Description = entity.Description,
                Photo = entity.Photo != null && entity.Photo.Length > 0 ? Image.FromStream(new MemoryStream(entity.Photo)) : null,
            };
        }

        public static StudentGroupViewModel CreateStudentGroupViewModel(StudentGroup entity)
        {
            return new StudentGroupViewModel
            {
                Id = entity.Id,
                EducationDirectionId = entity.EducationDirectionId,
                EducationDirectionCipher = entity.EducationDirection.Cipher,
                GroupName = entity.GroupName,
                Course = (int)entity.Course,
                CountStudents = (entity.Students != null) ? entity.Students.Where(s => !s.IsDeleted).Count() : 0,
                StewardName = (entity.Students != null) ? entity.Students.FirstOrDefault(s => !s.IsDeleted && s.IsSteward)?.ToString() ?? string.Empty : string.Empty,
                Curator = entity.CuratorId.HasValue ? entity.Curator.ToString() : "",
                CuratorId = entity.CuratorId
            };
        }

        public static StudentViewModel CreateStudentViewModel(Student entity)
        {
            return new StudentViewModel
            {
                Id = entity.Id,
                NumberOfBook = entity.NumberOfBook,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Patronymic = entity.Patronymic,
                Email = entity.Email,
                Description = entity.Description,
                Photo = entity.Photo != null ? Image.FromStream(new MemoryStream(entity.Photo)) : null,
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = (entity.StudentGroup != null) ? entity.StudentGroup.GroupName : string.Empty,
                IsSteward = entity.IsSteward
            };
        }

        public static StudentHistoryViewModel CreateStudentHistoryViewModel(StudentHistory entity)
        {
            return new StudentHistoryViewModel
            {
                Id = entity.Id,
                StudentId = entity.StudentId,
                DateCreate = entity.DateCreate.ToLongDateString(),
                TextMessage = entity.TextMessage
            };
        }
    }
}