using DepartmentDAL;
using DepartmentDAL.Enums;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Text;
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
                ResultService result;
                if (_id == 0)
                {
                    result = _service.CreateScheduleStopWord(new ScheduleStopWordRecordBindingModel
                    {
                        StopWord = textBoxStopWord.Text,
                        StopWordType = comboBoxStopWordType.Text
                    });
                }
                else
                {
                    result = _service.UpdateScheduleStopWord(new ScheduleStopWordRecordBindingModel
                    {
                        Id = _id,
                        StopWord = textBoxStopWord.Text,
                        StopWordType = comboBoxStopWordType.Text
                    });
                }
                if (result.Succeeded)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    StringBuilder strRes = new StringBuilder();
                    foreach (var err in result.Errors)
                    {
                        strRes.Append(string.Format("{0} : {1}\r\n", err.Key, err.Value));
                    }
                    MessageBox.Show("При сохранении возникла ошибка: " + strRes.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
