﻿using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicYear.StreamLesson.StreamLessonRecord
{
    public partial class StreamLessonRecordForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStreamLessonRecordService _service;

        private Guid? _id = null;

        private Guid _sId;

        public StreamLessonRecordForm(IStreamLessonRecordService service, Guid sId, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _sId = sId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void StreamLessonRecordForm_Load(object sender, EventArgs e)
        {
            if (_sId == null)
            {
                MessageBox.Show("Неуказан поток", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var resultSL = _service.GetStreamLessons(new StreamLessonGetBindingModel { });
            if (!resultSL.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке потоков возникла ошибка: ", resultSL.Errors);
                return;
            }

            var resultAP = _service.GetAcademicPlans(new AcademicPlanGetBindingModel { });
            if (!resultAP.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке учебных планов возникла ошибка: ", resultAP.Errors);
                return;
            }

            comboBoxStreamLesson.ValueMember = "Value";
            comboBoxStreamLesson.DisplayMember = "Display";
            comboBoxStreamLesson.DataSource = resultSL.Result.List
                .Select(x => new { Value = x.Id, Display = x.StreamLessonName }).ToList();
            comboBoxStreamLesson.SelectedItem = _sId;

            comboBoxAcademicPlan.ValueMember = "Value";
            comboBoxAcademicPlan.DisplayMember = "Display";
            comboBoxAcademicPlan.DataSource = resultAP.Result.List
                .Select(x => new { Value = x.Id, Display = string.Format("{0}. {1} курсы", x.EducationDirection, x.AcademicCoursesStrings) }).ToList();
            comboBoxAcademicPlan.SelectedItem = null;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var result = _service.GetStreamLessonRecord(new StreamLessonRecordGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxStreamLesson.SelectedValue = entity.StreamLessonId;
            comboBoxAcademicPlanRecordElement.SelectedValue = entity.AcademicPlanRecordElementId;
            textBoxHours.Text = entity.Hours.ToString();
            checkBoxIsMain.Checked = entity.IsMain;
        }

        private bool CheckFill()
        {
            if (comboBoxStreamLesson.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxAcademicPlanRecordElement.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxHours.Text))
            {
                return false;
            }
            int hours = 0;
            if (!int.TryParse(textBoxHours.Text, out hours))
            {
                return false;
            }
            return true;
        }

        private bool Save()
        {
            if (CheckFill())
            {
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateStreamLessonRecord(new StreamLessonRecordRecordBindingModel
                    {
                        StreamLessonId = new Guid(comboBoxStreamLesson.SelectedValue.ToString()),
                        AcademicPlanRecordElementId = new Guid(comboBoxAcademicPlanRecordElement.SelectedValue.ToString()),
                        Hours = Convert.ToInt32(textBoxHours.Text),
                        IsMain = checkBoxIsMain.Checked
                    });
                }
                else
                {
                    result = _service.UpdateStreamLessonRecord(new StreamLessonRecordRecordBindingModel
                    {
                        Id = _id.Value,
                        StreamLessonId = new Guid(comboBoxStreamLesson.SelectedValue.ToString()),
                        AcademicPlanRecordElementId = new Guid(comboBoxAcademicPlanRecordElement.SelectedValue.ToString()),
                        Hours = Convert.ToInt32(textBoxHours.Text),
                        IsMain = checkBoxIsMain.Checked
                    });
                }
                if (result.Succeeded)
                {
                    if (result.Result != null)
                    {
                        if (result.Result is Guid)
                        {
                            _id = (Guid)result.Result;
                        }
                    }
                    return true;
                }
                else
                {
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBoxAcademicPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAcademicPlan.SelectedValue != null)
            {
                Guid apId = new Guid(comboBoxAcademicPlan.SelectedValue.ToString());
                var resultAPR = _service.GetAcademicPlanRecords(new AcademicPlanRecordGetBindingModel { AcademicPlanId = apId });
                if (!resultAPR.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке записей учебных планов возникла ошибка: ", resultAPR.Errors);
                    return;
                }

                comboBoxAcademicPlanRecord.ValueMember = "Value";
                comboBoxAcademicPlanRecord.DisplayMember = "Display";
                comboBoxAcademicPlanRecord.DataSource = resultAPR.Result.List
                    .Select(x => new { Value = x.Id, Display = x.Disciplne }).ToList();
                comboBoxAcademicPlanRecord.SelectedItem = null;
            }
        }

        private void comboBoxAcademicPlanRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAcademicPlanRecord.SelectedValue != null)
            {
                Guid aprId = new Guid(comboBoxAcademicPlanRecord.SelectedValue.ToString());
                var resultAPRE = _service.GetAcademicPlanRecordElements(new AcademicPlanRecordElementGetBindingModel { AcademicPlanRecordId = aprId });
                if (!resultAPRE.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке нагрузок записей учебных планов возникла ошибка: ", resultAPRE.Errors);
                    return;
                }
                comboBoxAcademicPlanRecordElement.ValueMember = "Value";
                comboBoxAcademicPlanRecordElement.DisplayMember = "Display";
                comboBoxAcademicPlanRecordElement.DataSource = resultAPRE.Result.List
                    .Select(x => new { Value = x.Id, Display = x.KindOfLoadName }).ToList();
                comboBoxAcademicPlanRecordElement.SelectedItem = null;
            }
        }

        private void comboBoxAcademicPlanRecordElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAcademicPlanRecordElement.SelectedValue != null)
            {
                Guid apreId = new Guid(comboBoxAcademicPlanRecordElement.SelectedValue.ToString());
                var record = _service.GetAcademicPlanRecordElement(new AcademicPlanRecordElementGetBindingModel { Id = apreId });
                if (!record.Succeeded)
                {
                    Program.PrintErrorMessage("При загрузке нагрузки записи учебного плана возникла ошибка: ", record.Errors);
                    return;
                }
                textBoxHours.Text = record.Result.Hours.ToString();
            }
        }
    }
}