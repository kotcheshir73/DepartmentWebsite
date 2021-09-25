using System;
using System.Collections.Generic;
using System.Linq;
using WebInterfaces.Interfaces;
using System.ComponentModel;
using Tools;
using AcademicYearInterfaces.BindingModels;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using AcademicYearInterfaces.Interfaces;
using System.IO;
using System.IO.Compression;

namespace WebImplementations.Implementations
{
    public class StudyProcessService : IStudyProcessService
    {
        private readonly IAcademicYearProcess _process;

        public StudyProcessService(IAcademicYearProcess process)
        {
            _process = process;
        }

        public ResultService<List<List<object>>> GetAcademicYearLoading(AcademicYearGetBindingModel model)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    List<List<object>> list = new List<List<object>>();

                    list.Add(new List<object> {
                        null,
                        "Ставки преподавателей"
                    });
                    list.Add(new List<object> {
                        null,
                        "Часы по ставкам преподавателей"
                    });
                    list.Add(new List<object> {
                        null,
                        "Часы по дисциплинам"
                    });

                    // получаем блоки дисциплин, по которым нужно будет группировать записи
                    var disciplineBlocks = context.DisciplineBlocks.Where(x => !x.IsDeleted && x.DisciplineBlockUseForGrouping).OrderBy(x => x.DisciplineBlockOrder);

                    foreach (var discBlock in disciplineBlocks)
                    {
                        List<object> element = new List<object>() {
                            null,
                            null,
                            null,
                            discBlock.Title,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null,
                            null
                        };
                        list.Add(element);

                        var aprs = context.AcademicPlanRecords
                           .Include(x => x.AcademicPlan)
                           .Include(x => x.AcademicPlan.EducationDirection)
                           .Include(x => x.Discipline)
                           .Include(x => x.Contingent)
                           .Where(x => x.Discipline.DisciplineBlockId == discBlock.Id && x.AcademicPlan.AcademicYearId == model.Id && !x.IsDeleted && x.IsUseInWorkload && !x.IsParent)
                           .OrderByDescending(x => (int)x.Semester % 2)
                           .ThenBy(x => x.AcademicPlan.EducationDirection.Qualification)
                           .ThenBy(x => x.Semester)
                           .ThenBy(x => x.AcademicPlan.EducationDirectionId)
                           .ThenBy(x => x.Discipline.DisciplineName);

                        foreach (var apr in aprs)
                        {
                            List<object> elementApr = new List<object>() {
                                apr.Id,
                                apr.Semester.HasValue ? (int)apr.Semester % 2 == 0 ? "весна" : "осень" : null,
                                apr.AcademicPlan.EducationDirectionId.HasValue ? apr.AcademicPlan.EducationDirection.Cipher : null,
                                apr.Discipline.DisciplineName,
                                apr.IsChild ? "да" : "",
                                apr.ContingentId.HasValue ? Math.Log((double)apr.Contingent.Course, 2) + 1 : (double?)null,
                                apr.ContingentId.HasValue ? apr.Contingent.CountStudetns : (int?)null,
                                apr.ContingentId.HasValue ? 1 : (int?)null,
                                apr.ContingentId.HasValue ? apr.Contingent.CountGroups : (int?)null,
                                apr.ContingentId.HasValue ? apr.Contingent.CountSubgroups : (int?)null
                            };

                            decimal? total = null;

                            var apres = context.AcademicPlanRecordElements.Where(x => !x.IsDeleted && x.AcademicPlanRecordId == apr.Id);

                            if (apres != null)
                            {
                                foreach (var apre in apres)
                                {
                                    if (apre.FactHours != 0)
                                    {
                                        total = total != null ? total + apre.FactHours : apre.FactHours;
                                    }
                                }
                            }
                            elementApr.Add(total);

                            list.Add(elementApr);
                        }
                    }

                    return ResultService<List<List<object>>>.Success(list);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<List<object>>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<List<object>> GetLecturerMissions(Guid LecturerId, Guid AcademicYearId)
        {
            try
            {
                using (var context = DepartmentUserManager.GetContext)
                {
                    List<object> list = new List<object>();

                    var lecturer = context.Lecturers.Where(x => !x.IsDeleted && x.Id == LecturerId).FirstOrDefault();

                    if (lecturer == null)
                    {
                        throw new Exception("Преподаватель не найден");
                    }

                    var workload = context.LecturerWorkload.Where(x => !x.IsDeleted && x.LecturerId == LecturerId && x.AcademicYearId == AcademicYearId).FirstOrDefault();

                    if (workload == null)
                    {
                        throw new Exception("Нагрузка преподавателя не найдена");
                    }

                    var lecturerStudyPost = context.LecturerStudyPosts.Where(x => !x.IsDeleted && x.Id == lecturer.LecturerStudyPostId).FirstOrDefault();

                    if (lecturerStudyPost == null)
                    {
                        throw new Exception("Пост преподавателя не найден");
                    }

                    list.Add(workload.Workload);
                    list.Add(workload.Workload * lecturerStudyPost.Hours);
                    list.Add(0);

                    decimal disHours = 0;

                    // получаем блоки дисциплин, по которым нужно будет группировать записи
                    var disciplineBlocks = context.DisciplineBlocks.Where(x => !x.IsDeleted && x.DisciplineBlockUseForGrouping).OrderBy(x => x.DisciplineBlockOrder);

                    foreach (var discBlock in disciplineBlocks)
                    {
                        list.Add(null);

                        var aprs = context.AcademicPlanRecords
                           .Include(x => x.AcademicPlan)
                           .Include(x => x.AcademicPlan.EducationDirection)
                           .Include(x => x.Discipline)
                           .Where(x => x.Discipline.DisciplineBlockId == discBlock.Id && x.AcademicPlan.AcademicYearId == AcademicYearId && !x.IsDeleted && x.IsUseInWorkload && !x.IsParent)
                           .OrderByDescending(x => (int)x.Semester % 2)
                           .ThenBy(x => x.AcademicPlan.EducationDirection.Qualification)
                           .ThenBy(x => x.Semester)
                           .ThenBy(x => x.AcademicPlan.EducationDirectionId)
                           .ThenBy(x => x.Discipline.DisciplineName);

                        foreach (var apr in aprs)
                        {
                            decimal? total = null;

                            var apres = context.AcademicPlanRecordElements
                                .Include(x => x.AcademicPlanRecordMissions)
                                .Where(x => !x.IsDeleted && x.AcademicPlanRecordId == apr.Id);

                            foreach (var apre in apres)
                            {
                                var missions = apre.AcademicPlanRecordMissions.Where(x => !x.IsDeleted && x.LecturerId == LecturerId);
                                foreach (var mission in missions)
                                {
                                    if (mission.Hours != 0)
                                    {
                                        total = total != null ? total + mission.Hours : mission.Hours;
                                    }
                                }
                            }

                            disHours = total != null ? disHours + (decimal)total : disHours;
                            list.Add(total);
                        }
                    }

                    list[2] = disHours;

                    return ResultService<List<object>>.Success(list);
                }
            }
            catch (Exception ex)
            {
                return ResultService<List<object>>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<MemoryStream> ImportLecturerWorkloads(ImportLecturerWorkloadBindingModel model)
        {
            try
            {
                var result = _process.ImportLecturerWorkloads(model);

                var files = Directory.GetFiles(model.Path, "*.xlsx");

                var zipStream = GetMemoryStreamZipArchive(files);

                return ResultService<MemoryStream>.Success(zipStream);
            }
            catch (Exception ex)
            {
                return ResultService<MemoryStream>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        public ResultService<MemoryStream> ImportDisciplineTimeDistributions(ImportDisciplineTimeDistributionsBindingModel model)
        {
            try
            {
                var result = _process.ImportDisciplineTimeDistributionsLecturers(model);

                var files = Directory.GetFiles(model.Path, "*.docx");

                var zipStream = GetMemoryStreamZipArchive(files);

                return ResultService<MemoryStream>.Success(zipStream);
            }
            catch (Exception ex)
            {
                return ResultService<MemoryStream>.Error(ex, ResultServiceStatusCode.Error);
            }
        }

        private MemoryStream GetMemoryStreamZipArchive(string[] files)
        {
            var zipStream = new MemoryStream();
            using (var zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var filePath in files)
                {
                    zipArchive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                    File.Delete(filePath);
                }
            }
            zipStream.Position = 0;

            return zipStream;
        }

        public (List<string> displayNames, List<string> propertiesNames) GetPropertiesNames(Type type)
        {
            List<string> displayNames = new List<string>();
            List<string> propertiesNames = new List<string>();

            type.GetProperties().ToList().ForEach(x =>
            {
                object[] dn = x.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                if (dn.Length > 0)
                {
                    displayNames.Add((dn.FirstOrDefault() as DisplayNameAttribute).DisplayName);
                    propertiesNames.Add(x.Name);
                }
            });

            (List<string> displayNames, List<string> propertiesNames) info = (displayNames, propertiesNames);

            return info;
        }

        public List<List<object>> GetPropertiesValues<T>(List<T> list, List<string> propertiesNames)
        {
            List<List<object>> result = new List<List<object>>();

            foreach (T element in list)
            {
                result.Add(new List<object> { element.GetType().GetProperty("Id").GetValue(element, null) });
                foreach (string propertyName in propertiesNames)
                {
                    object value = element.GetType().GetProperty(propertyName).GetValue(element, null);
                    if (value is bool)
                    {
                        if ((bool)value == true)
                        {
                            result.LastOrDefault().Add("да");
                        }
                        else
                        {
                            result.LastOrDefault().Add("нет");
                        }
                    }
                    else
                    {
                        result.LastOrDefault().Add(value);
                    }
                }
            }

            return result;
        }
    }
}
