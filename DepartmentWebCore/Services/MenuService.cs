using AcademicYearInterfaces.BindingModels;
using BaseInterfaces.BindingModels;
using BaseInterfaces.ViewModels;
using DepartmentWebCore.Models;
using Enums;
using Microsoft.EntityFrameworkCore;
using Models.AcademicYearData;
using Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools;

namespace DepartmentWebCore.Services
{
    public class MenuService
    {
        public static List<MenuElementModel> getMenuElementList()
        {
            List<MenuElementModel> menuElements = new List<MenuElementModel>();

            MenuElementModel lecturer = new MenuElementModel() { Name = "Преподаватели", Child = new List<MenuElementModel>(), Controller = "Lecturer", Action = "Index" };
            var lecturerList = LecturerService.GetLecturers(new BaseInterfaces.BindingModels.LecturerGetBindingModel());
            foreach (var tmp in lecturerList.Result)
            {
                lecturer.Child.Add(new MenuElementModel() { Id = tmp.Abbreviation, Name = tmp.FullName, Controller = "Lecturer", Action = "Lecturer" });
            }
            menuElements.Add(lecturer);


            MenuElementModel direction = new MenuElementModel() { Name = "Направления", Child = new List<MenuElementModel>()};
            var directionList = GetEducationDirections();
            foreach (var directionItem in directionList.Result)
            {
                var courseOfDirection = GetContingents(new ContingentGetBindingModel { EducationDirectionId = directionItem.Id, AcademicYearId = DisciplineService.GetAcademicYear().Result.Id });
                var directionMenu = new MenuElementModel { Name = directionItem.ShortName, Child = new List<MenuElementModel>() };
                foreach (var tmp in courseOfDirection.Result)
                {
                    directionMenu.Child.Add(new MenuElementModel { Id = tmp.Id.ToString(), Name = "Курс " + tmp.Course.ToString().Split('_')[1], Controller = "Discipline", Action = "Index" });
                }
                direction.Child.Add(directionMenu);
            }
            menuElements.Add(direction);


            return menuElements;
        }

        private static ResultService<List<EducationDirection>> GetEducationDirections()
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.EducationDirections.Where(x => !x.IsDeleted).AsQueryable();
                    query = query.OrderBy(x => x.Cipher);
                    return ResultService<List<EducationDirection>>.Success(query.ToList());
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<EducationDirection>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        private static ResultService<List<Contingent>> GetContingents(ContingentGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    var query = context.Contingents.Where(x => !x.IsDeleted).AsQueryable();
                    if (model.AcademicYearId.HasValue)
                    {
                        query = query.Where(x => x.AcademicYearId == model.AcademicYearId);
                    }
                    if (model.EducationDirectionId.HasValue)
                    {
                        query = query.Where(x => x.EducationDirectionId == model.EducationDirectionId);
                    }
                    query = query.OrderBy(x => x.Course);
                    query = query.Include(x => x.AcademicYear).Include(x => x.EducationDirection);

                    return ResultService<List<Contingent>>.Success(query.ToList());
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<Contingent>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }
    }
}
