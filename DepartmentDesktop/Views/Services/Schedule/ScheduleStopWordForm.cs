using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Services.Schedule
{
    public partial class ScheduleStopWordForm : Form
    {
        private readonly IScheduleStopWordService _service;

        private long _id;

        public ScheduleStopWordForm(IScheduleStopWordService service)
        {
            InitializeComponent();
            _service = service;
        }

        public ScheduleStopWordForm(IScheduleStopWordService service, long id)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void ScheduleStopWordForm_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(ScheduleStopWordTypes)))
            {
                comboBoxStopWordType.Items.Add(elem);
            }
            comboBoxStopWordType.SelectedIndex = 0;
            if (_id != 0)
            {
                var entity = _service.GetScheduleStopWord(new ScheduleStopWordGetBindingModel { Id = _id });
                if (entity == null)
                {
                    MessageBox.Show("Запись не найдена", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                comboBoxStopWordType.SelectedValue = entity.StopWordType;
                textBoxStopWord.Text = entity.StopWord;
            }
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxStopWordType.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxStopWord.Text))
            {
                return false;
            }
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (CheckFill())
            {
                if (_id == 0)
                {
                    var res = _service.CreateScheduleStopWord(new ScheduleStopWordRecordBindingModel
                    {
                        StopWord = textBoxStopWord.Text,
                        StopWordType = comboBoxStopWordType.Text
                    });
                    if (res.Succeeded)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("При сохранении возникла ошибка: " + res.Errors["error"], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var res = _service.UpdateScheduleStopWord(new ScheduleStopWordRecordBindingModel
                    {
                        Id = _id,
                        StopWord = textBoxStopWord.Text,
                        StopWordType = comboBoxStopWordType.Text
                    });
                    if (res.Succeeded)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("При сохранении возникла ошибка: " + res.Errors["error"], "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
