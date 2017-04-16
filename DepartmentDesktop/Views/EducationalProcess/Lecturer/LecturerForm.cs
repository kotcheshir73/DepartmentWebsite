﻿using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.Lecturer
{
	public partial class LecturerForm : Form
	{
		private readonly ILecturerService _service;

		private long _id;

		public LecturerForm(ILecturerService service)
		{
			InitializeComponent();
			_service = service;
		}

		public LecturerForm(ILecturerService service, long id)
		{
			InitializeComponent();
			_service = service;
			_id = id;
		}

		private void LecturerForm_Load(object sender, EventArgs e)
		{
			if (_id != 0)
			{
				LoadData();
			}
		}

		private void LoadData()
		{
			var result = _service.GetLecturer(new LecturerGetBindingModel { Id = _id });
			if (!result.Succeeded)
			{
				Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;
			
			textBoxLastName.Text = entity.LastName;
			textBoxFirstName.Text = entity.FirstName;
			textBoxPatronymic.Text = entity.Patronymic;
			textBoxAbbreviation.Text = entity.Abbreviation;
			dateTimePickerDateBirth.Value = entity.DateBirth;
			textBoxAddress.Text = entity.Address;
			textBoxEmail.Text = entity.Email;
			textBoxMobileNumber.Text = entity.MobileNumber;
			textBoxHomeNumber.Text = entity.HomeNumber;
			textBoxPost.Text = entity.Post;
			textBoxRank.Text = entity.Rank;
			textBoxDescription.Text = entity.Description;
			if (entity.Photo != null)
			{
				pictureBoxPhoto.Image = entity.Photo;
			}
		}

		private bool CheckFill()
		{
			if (string.IsNullOrEmpty(textBoxLastName.Text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxFirstName.Text))
			{
				return false;
			}
			if(dateTimePickerDateBirth.Value.Date == DateTime.Now.Date)
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxAddress.Text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxEmail.Text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxMobileNumber.Text))
			{
				return false;
			}
			if (string.IsNullOrEmpty(textBoxPost.Text))
			{
				return false;
			}
			return true;
		}

		private bool Save()
		{
			if (CheckFill())
			{
				ImageConverter converter = new ImageConverter();
				ResultService result;
				if (_id == 0)
				{
					result = _service.CreateLecturer(new LecturerRecordBindingModel
					{
						LastName = textBoxLastName.Text,
						FirstName = textBoxFirstName.Text,
						Patronymic = textBoxPatronymic.Text,
						Abbreviation = textBoxAbbreviation.Text,
						DateBirth = dateTimePickerDateBirth.Value,
						Address = textBoxAddress.Text,
						Email = textBoxEmail.Text,
						MobileNumber = textBoxMobileNumber.Text,
						HomeNumber = textBoxHomeNumber.Text,
						Post = textBoxPost.Text,
						Rank = textBoxRank.Text,
						Description = textBoxDescription.Text,
						Photo = (byte[])converter.ConvertTo(pictureBoxPhoto.Image, typeof(byte[]))
					});
				}
				else
				{
					result = _service.UpdateLecturer(new LecturerRecordBindingModel
					{
						Id = _id,
						LastName = textBoxLastName.Text,
						FirstName = textBoxFirstName.Text,
						Patronymic = textBoxPatronymic.Text,
						Abbreviation = textBoxAbbreviation.Text,
						DateBirth = dateTimePickerDateBirth.Value,
						Address = textBoxAddress.Text,
						Email = textBoxEmail.Text,
						MobileNumber = textBoxMobileNumber.Text,
						HomeNumber = textBoxHomeNumber.Text,
						Post = textBoxPost.Text,
						Rank = textBoxRank.Text,
						Description = textBoxDescription.Text,
						Photo = (byte[])converter.ConvertTo(pictureBoxPhoto.Image, typeof(byte[]))
					});
				}
				if (result.Succeeded)
				{
					if (result.Result != null)
					{
						if (result.Result is long)
						{
							_id = (long)result.Result;
						}
					}
					return true;
				}
				return false;
			}
			else
			{
				MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		private void buttonUpload_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					pictureBoxPhoto.ClientSize = new Size(150, 150);
					pictureBoxPhoto.Image = new Bitmap(dialog.FileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка при загрузке файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
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