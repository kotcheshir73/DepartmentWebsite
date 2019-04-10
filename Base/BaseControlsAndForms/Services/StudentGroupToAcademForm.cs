using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace BaseControlsAndForms.StudentGroup
{
    public partial class StudentGroupToAcademForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentGroupService _service;

		private readonly IStudentService _serviceS;

		private readonly IProcess _process;

        private List<Guid> _ids = null;

        public StudentGroupToAcademForm(IStudentGroupService service, IStudentService serviceS, IProcess process, List<Guid> ids)
		{
			InitializeComponent();
			_service = service;
			_serviceS = serviceS;
			_process = process;
            _ids = ids;
        }

		private void StudentGroupToAcademForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < _ids.Count; ++i)
            {
                var result = _serviceS.GetStudent(new StudentGetBindingModel { Id = _ids[i] });
                if (!result.Succeeded)
                {
                    ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                    return;
                }
                dataGridViewStudents.Rows.Add(new object[] {
                    result.Result.NumberOfBook,
                    result.Result.LastName,
                    result.Result.FirstName,
                    result.Result.Patronymic
                });
            }
        }

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxToAcademOrderNumber.Text))
			{
				MessageBox.Show("Введите номер приказа ухода в академ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var result = _process.ToAcademStudents(new StudentToAcademBindingModel
			{
				ToAcademDate = dateTimePickerToAcademDate.Value,
				ToAcademOrderNumber = textBoxToAcademOrderNumber.Text,
                StudnetIds = _ids
            });
			if (result.Succeeded)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
                ErrorMessanger.PrintErrorMessage("Ошибка при сохранении спсика: ", result.Errors);
			}
		}

		private void ButtonClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}