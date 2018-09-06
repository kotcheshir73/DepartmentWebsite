using DepartmentDesktop.Models;
using DepartmentDesktop.Views.Lecturer.DisciplineLessonTaskSettings;
using DepartmentModel;
using DepartmentService.BindingModels.StandartBindingModels.EducationDirection;
using DepartmentService.IServices.StandartInterfaces.EducationDirection;
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
using Unity.Resolution;

namespace DepartmentDesktop.Views.Lecturer
{
    public partial class DisciplineLessonTaskSettingsForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskService _serviceDLT;

        private readonly IDisciplineLessonTaskVariantService _serviceDLTV;

        private Guid? _id = null;

        private Guid _lessonId;

        public DisciplineLessonTaskSettingsForm(IDisciplineLessonTaskService serviceDLT, IDisciplineLessonTaskVariantService serviceDLTV, Guid lessonId, Guid? id = null)
        {
            InitializeComponent();

            _serviceDLT = serviceDLT;
            _serviceDLTV = serviceDLTV;

            if (id != Guid.Empty)
            {
                _id = id;
            }

            _lessonId = lessonId;

        }

        private void DisciplineLessonTaskSettingsForm_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            var result = _serviceDLT.GetDisciplineLessonTask(new DisciplineLessonTaskGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxTask.Text = entity.Task;
            checkBoxIsNecessarily.Checked = entity.IsNecessarily ? true : false;
            textBoxOrder.Text = entity.Order.ToString();
            textBoxMaxBall.Text = entity.MaxBall.ToString();

            if (tabPage.Controls.Count == 0)
            {
                var controlDLTS = Container.Resolve<DisciplineLessonTaskSettingsControl>();
                controlDLTS.Dock = DockStyle.Fill;
                tabPage.Controls.Add(controlDLTS);
            }
            (tabPage.Controls[0] as DisciplineLessonTaskSettingsControl).LoadData(_id.Value);
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(textBoxTask.Text))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxMaxBall.Text))
            {
                decimal maxBall = 0;
                if (!decimal.TryParse(textBoxMaxBall.Text, out maxBall))
                {
                    return false;
                }
            }
            int order = 0;
            if (!int.TryParse(textBoxOrder.Text, out order))
            {
                return false;
            }
            return true;
        }

        public bool Save()
        {
            if (CheckFill())
            {
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _serviceDLT.CreateDisciplineLessonTask(new DisciplineLessonTaskRecordBindingModel
                    {
                        DisciplineLessonId = _lessonId,
                        Task = textBoxTask.Text,
                        IsNecessarily = checkBoxIsNecessarily.Checked,
                        MaxBall = !string.IsNullOrEmpty(textBoxMaxBall.Text) ? Convert.ToInt32(textBoxMaxBall.Text): (int?)null,
                        Order = Convert.ToInt32(textBoxOrder.Text)
                    });
                }
                else
                {
                    result = _serviceDLT.UpdateDisciplineLessonTask(new DisciplineLessonTaskRecordBindingModel
                    {
                        Id = _id.Value,
                        DisciplineLessonId = _lessonId,
                        Task = textBoxTask.Text,
                        IsNecessarily = checkBoxIsNecessarily.Checked,
                        MaxBall = !string.IsNullOrEmpty(textBoxMaxBall.Text) ? Convert.ToInt32(textBoxMaxBall.Text) : (int?)null,
                        Order = Convert.ToInt32(textBoxOrder.Text)
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
    }
}
