﻿using BaseInterfaces.ViewModels;
using Enums;
using Models.Base;
using System.Drawing;
using System.IO;
using System.Linq;

namespace BaseImplementations
{
    public static class ModelFactoryToViewModel
    {
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

        public static EducationDirectionViewModel CreateEducationDirectionViewModel(EducationDirection entity)
        {
            return new EducationDirectionViewModel
            {
                Id = entity.Id,
                Cipher = entity.Cipher,
                ShortName = entity.ShortName,
                Title = entity.Title,
                Qualification = entity.Qualification.ToString(),
                Description = entity.Description,
                Profile = entity.Profile
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

        public static LecturerStudyPostViewModel CreateLecturerStudyPostViewModel(LecturerStudyPost entity)
        {
            return new LecturerStudyPostViewModel
            {
                Id = entity.Id,
                StudyPostTitle = entity.StudyPostTitle,
                Hours = entity.Hours
            };
        }

        public static DisciplineViewModel CreateDisciplineViewModel(Discipline entity)
        {
            return new DisciplineViewModel
            {
                Id = entity.Id,
                DisciplineBlockId = entity.DisciplineBlockId,
                DisciplineName = entity.DisciplineName,
                DisciplineShortName = string.IsNullOrEmpty(entity.DisciplineShortName) ? entity.ToString() : entity.DisciplineShortName,
                DisciplineBlockTitle = entity.DisciplineBlock?.ToString() ?? string.Empty,
                DisciplineBlueAsteriskName = entity.DisciplineBlueAsteriskName,
                DisciplineDescription =entity.DisciplineDescription
            };
        }

        public static LecturerViewModel CreateLecturerViewModel(Lecturer entity)
        {
            return new LecturerViewModel
            {
                Id = entity.Id,
                LecturerStudyPostId = entity.LecturerStudyPostId,
                LastName = entity.LastName,
                FirstName = entity.FirstName,
                Patronymic = entity.Patronymic,
                Abbreviation = entity.Abbreviation,
                DateBirth = entity.DateBirth,
                Post = entity.Post.ToString(),
                LecturerPost = entity.LecturerPost?.ToString() ?? string.Empty,
                Rank = entity.Rank.ToString(),
                Rank2 = entity.Rank2.ToString(),
                HomeNumber = entity.HomeNumber,
                MobileNumber = entity.MobileNumber,
                Email = entity.Email,
                Address = entity.Address,
                Description = entity.Description,
                Photo = entity.Photo != null && entity.Photo.Length > 0 ? Image.FromStream(new MemoryStream(entity.Photo)) : null,
                PhotoByteArr = entity.Photo,
                OnlyForPrivate = entity.OnlyForPrivate
            };
        }

        public static StudentGroupViewModel CreateStudentGroupViewModel(StudentGroup entity)
        {
            return new StudentGroupViewModel
            {
                Id = entity.Id,
                EducationDirectionId = entity.EducationDirectionId,
                EducationDirectionCipher = entity.EducationDirection?.ToString() ?? string.Empty,
                GroupName = entity.GroupName,
                Course = (int)entity.Course,
                CountStudents = (entity.Students != null) ? entity.Students.Where(s => !s.IsDeleted && s.StudentState == StudentState.Учится).Count() : 0,
                CountAcademStudents = (entity.Students != null) ? entity.Students.Where(s => !s.IsDeleted && s.StudentState == StudentState.Академ).Count() : 0,
                StewardName = (entity.Students != null) ? entity.Students.FirstOrDefault(s => !s.IsDeleted && s.IsSteward)?.ToString() ?? string.Empty : string.Empty,
                Curator = entity.Curator?.ToString() ?? string.Empty,
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
                State = entity.StudentState.ToString(),
                Email = entity.Email,
                Description = entity.Description,
                Photo = entity.Photo != null ? Image.FromStream(new MemoryStream(entity.Photo)) : null,
                StudentGroupId = entity.StudentGroupId,
                StudentGroup = entity.StudentGroup?.GroupName ?? string.Empty,
                IsSteward = entity.IsSteward
            };
        }

        public static StudentOrderViewModel CreateStudentOrderViewModel(StudentOrder entity)
        {
            return new StudentOrderViewModel
            {
                Id = entity.Id,
                OrderNumber = entity.OrderNumber,
                OrderDate = entity.DateCreate,
                StudentOrderType = entity.StudentOrderType.ToString(),
                CountStudents = entity.StudentOrderBlocks?.Sum(x => x.StudentOrderBlockStudents?.Count ?? 0) ?? 0
            };
        }

        public static StudentOrderBlockViewModel CreateStudentOrderBlockViewModel(StudentOrderBlock entity)
        {
            return new StudentOrderBlockViewModel
            {
                Id = entity.Id,
                StudentOrderId = entity.StudentOrderId,
                EducationDirectionId = entity.EducationDirectionId,
                StudentOrder = entity.StudentOrder?.ToString() ?? string.Empty,
                EducationDirection = entity.EducationDirection?.Cipher ?? string.Empty,
                StudentOrderType = entity.StudentOrderType.ToString()
            };
        }

        public static StudentOrderBlockStudentViewModel CreateStudentOrderBlockStudentViewModel(StudentOrderBlockStudent entity)
        {
            return new StudentOrderBlockStudentViewModel
            {
                Id = entity.Id,
                StudentOrderBlockId = entity.StudentOrderBlockId,
                StudentOrderBlock = entity.StudentOrderBlock?.StudentOrderType.ToString() ?? string.Empty,
                StudentId = entity.StudentId,
                Student = entity.Student?.ToString() ?? string.Empty,
                StudentGroupFromId = entity.StudentGroupFromId,
                StudentGroupToId = entity.StudentGroupToId,
                StudentGromFrom = entity.StudentGroupFrom?.ToString() ?? string.Empty,
                StudentGroupTo = entity.StudentGroupTo?.ToString() ?? string.Empty,
                Info = entity.Info
            };
        }
    }
}