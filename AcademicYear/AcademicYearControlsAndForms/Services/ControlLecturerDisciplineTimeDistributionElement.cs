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

namespace AcademicYearControlsAndForms.Services
{
    public partial class ControlLecturerDisciplineTimeDistributionElement : UserControl
    {
        private Guid _disciplineTimeDistributiond;

        public ControlLecturerDisciplineTimeDistributionElement(LecturerDisciplineTimeDistribution model)
        {
            InitializeComponent();
            _disciplineTimeDistributiond = model.DisciplineTimeDistributionId;
            labelEducationDirection.Text = $"Направление: {model.EducationDirection}";
            labelGroupName.Text = $"Группа: {model.StudentGroup}";
            labelDisipline.Text = $"Дисциплина: {model.Discipline}";
            labelSemester.Text = $"{model.Semestr} семестр {model.AcademicYear} гг.";
            textBoxComment.Text = model.Comment;
            textBoxCommentWishesOfTeacher.Text = model.CommentWishesOfTeacher;

            foreach(var elem in model.LecturerDisciplineTimeDistributionElements)
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
        }

        private void DataGridViewElements_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int total = Convert.ToInt32(dataGridViewElements.Rows[e.RowIndex].Cells[24].Value);
            if (e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 15 || e.ColumnIndex == 16)
            {
                int value = Convert.ToInt32(dataGridViewElements.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                for(int i = 2; i < 8; i += 2)
                {
                    dataGridViewElements.Rows[e.RowIndex].Cells[e.ColumnIndex + i].Value = value;
                }
                if(e.ColumnIndex < 10)
                {
                    if(total / (value * 4) == 2)
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
            for(int i = 7; i < 23; ++i)
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
    }
}
