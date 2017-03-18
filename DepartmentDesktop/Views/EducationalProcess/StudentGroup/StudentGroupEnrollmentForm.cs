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

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
	public partial class StudentGroupEnrollmentForm : Form
	{
		private readonly IStudentGroupService _service;

		private readonly IStudentService _serviceS;

		private long _id = 0;

		public StudentGroupEnrollmentForm(IStudentGroupService service, IStudentService serviceS, long id)
		{
			InitializeComponent();
			_service = service;
			_serviceS = serviceS;
			_id = id;
		}

		private void buttonLoadFromFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "doc|*.doc|docx|*.docx";
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				var result = _serviceS.LoadStudentsFromFile(new StudentLoadDocBindingModel { Id = _id, FileName = dialog.FileName });
				if (result.Succeeded)
				{
					
				}
				else
				{
					Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
				}
			}
		}
	}
}
