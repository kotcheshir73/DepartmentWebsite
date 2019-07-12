using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcademicYearInterfaces.ViewModels;
using ControlsAndForms.Messangers;
using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;

namespace AcademicYearControlsAndForms.Services
{
    public partial class ControlLecturerDisciplineTimeDistributionElement : UserControl
    {
        private readonly IAcademicYearProcess _process;

        private LecturerDisciplineTimeDistribution _model;

        public ControlLecturerDisciplineTimeDistributionElement(IAcademicYearProcess process, LecturerDisciplineTimeDistribution model)
        {
            InitializeComponent();
            _process = process;
            _model = model;
            LoadData();
        }

        private void LoadData()
        {
            labelEducationDirection.Text = $"Направление: {_model.EducationDirection}";
            labelGroupName.Text = $"Группа: {_model.StudentGroup}";
            labelDisipline.Text = $"Дисциплина: {_model.Discipline}";
            labelSemester.Text = $"{_model.Semestr} семестр {_model.AcademicYear} гг.";
            textBoxComment.Text = _model.Comment;
            textBoxCommentWishesOfTeacher.Text = _model.CommentWishesOfTeacher;

            dataGridViewElements.Rows.Clear();
            foreach (var elem in _model.LecturerDisciplineTimeDistributionElements)
            {
                dataGridViewElements.Rows.Add(new object[]
                    {
                        elem.DisciplineTimeDistributionClassroomId,
                        elem.DisciplineTimeDistributionRecordFirstWeekFirstHalfId,
                        elem.DisciplineTimeDistributionRecordSecondWeekFirstHalfId,
                        elem.DisciplineTimeDistributionRecordFirstWeekSecondHalfId,
                        elem.DisciplineTimeDistributionRecordSecondWeekSecondHalfId,
                        elem.TimeNorm,
                        elem.DisciplineTimeDistributionClassroom,
                        elem.DisciplineTimeDistributionRecordFirstWeekFirstHalf,
                        elem.DisciplineTimeDistributionRecordSecondWeekFirstHalf,
                        elem.DisciplineTimeDistributionRecordFirstWeekFirstHalf,
                        elem.DisciplineTimeDistributionRecordSecondWeekFirstHalf,
                        elem.DisciplineTimeDistributionRecordFirstWeekFirstHalf,
                        elem.DisciplineTimeDistributionRecordSecondWeekFirstHalf,
                        elem.DisciplineTimeDistributionRecordFirstWeekFirstHalf,
                        elem.DisciplineTimeDistributionRecordSecondWeekFirstHalf,
                        elem.DisciplineTimeDistributionRecordFirstWeekSecondHalf,
                        elem.DisciplineTimeDistributionRecordSecondWeekSecondHalf,
                        elem.DisciplineTimeDistributionRecordFirstWeekSecondHalf,
                        elem.DisciplineTimeDistributionRecordSecondWeekSecondHalf,
                        elem.DisciplineTimeDistributionRecordFirstWeekSecondHalf,
                        elem.DisciplineTimeDistributionRecordSecondWeekSecondHalf,
                        elem.DisciplineTimeDistributionRecordFirstWeekSecondHalf,
                        elem.DisciplineTimeDistributionRecordSecondWeekSecondHalf,
                        null,
                        elem.TotalSum
                    });
            }

            for (int i = 0; i < dataGridViewElements.Rows.Count; ++i)
            {
                int sum = 0;
                for (int j = 7; j < 23; ++j)
                {
                    sum += Convert.ToInt32(dataGridViewElements.Rows[i].Cells[j].Value);
                }
                dataGridViewElements.Rows[i].Cells[23].Value = sum;
            }
        }

        private void DataGridViewElements_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int total = Convert.ToInt32(dataGridViewElements.Rows[e.RowIndex].Cells[24].Value);
            if (e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 15 || e.ColumnIndex == 16)
            {
                int value = Convert.ToInt32(dataGridViewElements.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                for (int i = 2; i < 8; i += 2)
                {
                    dataGridViewElements.Rows[e.RowIndex].Cells[e.ColumnIndex + i].Value = value;
                }
                if (e.ColumnIndex < 10 && value > 0)
                {
                    if (total / (value * 4) == 2)
                    {
                        for (int i = 8; i < 16; i += 2)
                        {
                            dataGridViewElements.Rows[e.RowIndex].Cells[e.ColumnIndex + i].Value = value;
                        }
                    }
                    value += Convert.ToInt32(dataGridViewElements.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value ?? 0);
                    if (total <= value * 4)
                    {
                        for (int i = 0; i < 8; i += 1)
                        {
                            dataGridViewElements.Rows[e.RowIndex].Cells[15 + i].Value = null;
                        }
                    }
                }
            }

            int sum = 0;
            for (int i = 7; i < 23; ++i)
            {
                sum += Convert.ToInt32(dataGridViewElements.Rows[e.RowIndex].Cells[i].Value);
            }
            dataGridViewElements.Rows[e.RowIndex].Cells[23].Value = sum;
            if (Convert.ToInt32(dataGridViewElements.Rows[e.RowIndex].Cells[24].Value) != sum)
            {
                dataGridViewElements.Rows[e.RowIndex].Cells[24].Style.BackColor = Color.Red;
            }
            else
            {
                dataGridViewElements.Rows[e.RowIndex].Cells[24].Style.BackColor = Color.Silver;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewElements.Rows.Count; ++i)
            {
                if (Convert.ToInt32(dataGridViewElements.Rows[i].Cells[24].Value) != Convert.ToInt32(dataGridViewElements.Rows[i].Cells[23].Value))
                {
                    ErrorMessanger.PrintErrorMessage("", new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Ошибка", "Имеется несовпадение по часам") });
                    return;
                }
            }

            LecturerDisciplineTimeDistributionSet model = new LecturerDisciplineTimeDistributionSet
            {
                DisciplineTimeDistributionId = _model.DisciplineTimeDistributionId,
                Comment = textBoxComment.Text,
                CommentWishesOfTeacher = textBoxCommentWishesOfTeacher.Text,
                LecturerDisciplineTimeDistributionElements = new List<LecturerDisciplineTimeDistributionElementSet>()
            };

            for (int i = 0; i < dataGridViewElements.Rows.Count; ++i)
            {
                model.LecturerDisciplineTimeDistributionElements.Add(new LecturerDisciplineTimeDistributionElementSet
                {
                    DisciplineTimeDistributionClassroomId = new Guid(dataGridViewElements.Rows[i].Cells[0].Value.ToString()),
                    DisciplineTimeDistributionClassroom = dataGridViewElements.Rows[i].Cells[6].Value?.ToString(),
                    DisciplineTimeDistributionRecordFirstWeekFirstHalfId = new Guid(dataGridViewElements.Rows[i].Cells[1].Value.ToString()),
                    DisciplineTimeDistributionRecordFirstWeekFirstHalf = dataGridViewElements.Rows[i].Cells[7].Value != null ?
                                                                Convert.ToDouble(dataGridViewElements.Rows[i].Cells[7].Value) : (double?)null,
                    DisciplineTimeDistributionRecordSecondWeekFirstHalfId = new Guid(dataGridViewElements.Rows[i].Cells[2].Value.ToString()),
                    DisciplineTimeDistributionRecordSecondWeekFirstHalf = dataGridViewElements.Rows[i].Cells[8].Value != null ?
                                                                Convert.ToDouble(dataGridViewElements.Rows[i].Cells[8].Value) : (double?)null,
                    DisciplineTimeDistributionRecordFirstWeekSecondHalfId = new Guid(dataGridViewElements.Rows[i].Cells[3].Value.ToString()),
                    DisciplineTimeDistributionRecordFirstWeekSecondHalf = dataGridViewElements.Rows[i].Cells[15].Value != null ?
                                                                Convert.ToDouble(dataGridViewElements.Rows[i].Cells[15].Value) : (double?)null,
                    DisciplineTimeDistributionRecordSecondWeekSecondHalfId = new Guid(dataGridViewElements.Rows[i].Cells[4].Value.ToString()),
                    DisciplineTimeDistributionRecordSecondWeekSecondHalf = dataGridViewElements.Rows[i].Cells[16].Value != null ?
                                                                Convert.ToDouble(dataGridViewElements.Rows[i].Cells[16].Value) : (double?)null
                });
            }

            var result = _process.SetLecturerDisciplineTimeDistributions(model);
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
            }
            else
            {
                _model.Comment = model.Comment;
                _model.CommentWishesOfTeacher = model.CommentWishesOfTeacher;

                foreach (var elem in _model.LecturerDisciplineTimeDistributionElements)
                {
                    for (int i = 0; i < dataGridViewElements.Rows.Count; ++i)
                    {
                        if(dataGridViewElements.Rows[i].Cells[0].Value.ToString() == elem.DisciplineTimeDistributionClassroomId.ToString())
                        {
                            elem.DisciplineTimeDistributionClassroom = dataGridViewElements.Rows[i].Cells[6].Value?.ToString();
                            elem.DisciplineTimeDistributionRecordFirstWeekFirstHalf = dataGridViewElements.Rows[i].Cells[7].Value != null ?
                                                                Convert.ToDouble(dataGridViewElements.Rows[i].Cells[7].Value) : (double?)null;
                            elem.DisciplineTimeDistributionRecordSecondWeekFirstHalf = dataGridViewElements.Rows[i].Cells[8].Value != null ?
                                                                        Convert.ToDouble(dataGridViewElements.Rows[i].Cells[8].Value) : (double?)null;
                            elem.DisciplineTimeDistributionRecordFirstWeekSecondHalf = dataGridViewElements.Rows[i].Cells[15].Value != null ?
                                                                Convert.ToDouble(dataGridViewElements.Rows[i].Cells[15].Value) : (double?)null;
                            elem.DisciplineTimeDistributionRecordSecondWeekSecondHalf = dataGridViewElements.Rows[i].Cells[16].Value != null ?
                                                                        Convert.ToDouble(dataGridViewElements.Rows[i].Cells[16].Value) : (double?)null;
                            break;
                        }
                    }
                }

                LoadData();
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
