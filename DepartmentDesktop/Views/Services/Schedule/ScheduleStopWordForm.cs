using DepartmentDAL;
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
                var result = _service.GetScheduleStopWord(new ScheduleStopWordGetBindingModel { Id = _id });
				if (!result.Succeeded)
				{
					Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
					Close();
				}
				var entity = result.Result;

				comboBoxStopWordType.SelectedValue = entity.StopWordType;
                textBoxStopWord.Text = entity.StopWord;
				textBoxReplace.Text = entity.StopWordReplace;
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
						StopWordReplace = textBoxReplace.Text,
                        StopWordType = comboBoxStopWordType.Text
                    });
                }
                else
                {
                    result = _service.UpdateScheduleStopWord(new ScheduleStopWordRecordBindingModel
                    {
                        Id = _id,
                        StopWord = textBoxStopWord.Text,
						StopWordReplace = textBoxReplace.Text,
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
					Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
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
