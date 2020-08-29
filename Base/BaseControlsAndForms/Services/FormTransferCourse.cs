﻿using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace BaseControlsAndForms.Services
{
    public partial class FormTransferCourse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentGroupService _serviceSG;

		private readonly IStudentService _serviceS;

		private readonly IProcess _process;

		private Guid? _id = null;

		public FormTransferCourse(IStudentGroupService serviceSG, IStudentService serviceS, IProcess process, Guid? id = null)
		{
			InitializeComponent();
			_serviceSG = serviceSG;
			_serviceS = serviceS;
			_process = process;
			_id = id;
		}

		private void FormTransferCourse_Load(object sender, EventArgs e)
		{
			var resultSG = _serviceSG.GetStudentGroups(new StudentGroupGetBindingModel { });
			if (!resultSG.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке групп возникла ошибка: ", resultSG.Errors);
				return;
			}

			comboBoxNewStudentGroup.ValueMember = "Value";
			comboBoxNewStudentGroup.DisplayMember = "Display";
			comboBoxNewStudentGroup.DataSource = resultSG.Result.List
				.Select(ed => new { Value = ed.Id, Display = ed.GroupName }).ToList();
			comboBoxNewStudentGroup.SelectedItem = null;

			var result = _serviceS.GetStudents(new StudentGetBindingModel { StudentGroupId = _id, StudentStatus = StudentState.Учится });
			if (!result.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				return;
			}
			var list = result.Result.List;
			for (int i = 0; i < list.Count; ++i)
			{
				dataGridViewStudents.Rows.Add(new object[] 
                {
                    false,
                    false,
                    list[i].Id,
                    list[i].NumberOfBook,
                    list[i].LastName,
                    list[i].FirstName,
                    list[i].Patronymic
                });
			}
		}

		private void CheckBoxSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < dataGridViewStudents.Rows.Count; ++i)
			{
				dataGridViewStudents.Rows[i].Cells[0].Value = checkBoxSelectAll.Checked;
			}
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxTransferOrderNumber.Text))
			{
				MessageBox.Show("Введите основание перевода", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (comboBoxNewStudentGroup.SelectedValue == null)
			{
				MessageBox.Show("Выберите группу перевода", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
            Guid newId = new Guid(comboBoxNewStudentGroup.SelectedValue.ToString());
			var list = new List<Tuple<Guid, bool>>();
			for (int i = 0; i < dataGridViewStudents.Rows.Count; ++i)
			{
				if (Convert.ToBoolean(dataGridViewStudents.Rows[i].Cells[0].Value))
				{
					list.Add(new Tuple<Guid, bool> (new Guid(dataGridViewStudents.Rows[i].Cells[2].Value.ToString()), Convert.ToBoolean(dataGridViewStudents.Rows[i].Cells[1].Value)));
				}
			}
			if (list.Count == 0)
			{
				MessageBox.Show("Укажите хотя бы одного студента", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			var result = _process.TransferCourse(new StudentTransferBindingModel
			{
				TransferOrderDate = dateTimePickerTransferDate.Value,
				TransferOrderNumber = textBoxTransferOrderNumber.Text,
				NewStudentGroupId = newId,
				OldStudentGroupId = _id.Value,
				StudentList = list
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

		private void DataGridViewStudents_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if(e.ColumnIndex == 1)
			{
				var val = dataGridViewStudents.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
				if((bool)val)
				{
					dataGridViewStudents.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value =
						dataGridViewStudents.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
				}
			}
		}
	}
}