﻿using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace BaseControlsAndForms.Lecturer
{
    public partial class LecturerForm : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILecturerService _service;

        public LecturerForm(ILecturerService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        protected override bool LoadComponents()
        {
            var resultLSP = _service.GetLecturerStudyPosts(new LecturerStudyPostGetBindingModel { });
            if (!resultLSP.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке должностей преподавателей возникла ошибка: ", resultLSP.Errors);
                return false;
            }

            var resultLDP = _service.GetLecturerDepartmentPosts(new LecturerDepartmentPostGetBindingModel { });
            if (!resultLDP.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке должностей преподавателей возникла ошибка: ", resultLDP.Errors);
                return false;
            }

            foreach (var elem in Enum.GetValues(typeof(Rank)))
            {
                comboBoxRank.Items.Add(elem.ToString());
            }
            comboBoxRank.SelectedIndex = 0;

            foreach (var elem in Enum.GetValues(typeof(Rank2)))
            {
                comboBoxRank2.Items.Add(elem.ToString());
            }
            comboBoxRank2.SelectedIndex = 0;

            comboBoxLecturerStudyPost.ValueMember = "Value";
            comboBoxLecturerStudyPost.DisplayMember = "Display";
            comboBoxLecturerStudyPost.DataSource = resultLSP.Result.List
                .Select(lp => new { Value = lp.Id, Display = lp.StudyPostTitle }).ToList();
            comboBoxLecturerStudyPost.SelectedItem = null;

            comboBoxLecturerDepartmentPost.ValueMember = "Value";
            comboBoxLecturerDepartmentPost.DisplayMember = "Display";
            comboBoxLecturerDepartmentPost.DataSource = resultLDP.Result.List
                .Select(lp => new { Value = lp.Id, Display = lp.DepartmentPostTitle }).ToList();
            comboBoxLecturerDepartmentPost.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetLecturer(new LecturerGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
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
            if(entity.LecturerDepartmentPostId.HasValue)
            {
                comboBoxLecturerDepartmentPost.SelectedValue = entity.LecturerDepartmentPostId;
            }
            comboBoxRank.SelectedIndex = comboBoxRank.Items.IndexOf(entity.Rank);
            comboBoxRank2.SelectedIndex = comboBoxRank2.Items.IndexOf(entity.Rank2);
            comboBoxLecturerStudyPost.SelectedValue = entity.LecturerStudyPostId;
            textBoxDescription.Text = entity.Description;
            if (entity.Photo != null)
            {
                pictureBoxPhoto.Image = entity.Photo;
            }
            checkBoxOnlyForPrivate.Checked = entity.OnlyForPrivate;
        }

        protected override bool CheckFill()
        {
            if (comboBoxLecturerStudyPost.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxLastName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxFirstName.Text))
            {
                return false;
            }
            if (dateTimePickerDateBirth.Value.Date == DateTime.Now.Date)
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
            if (string.IsNullOrEmpty(comboBoxRank.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(comboBoxRank2.Text))
            {
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            ImageConverter converter = new ImageConverter();
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateLecturer(new LecturerSetBindingModel
                {
                    LecturerStudyPostId = new Guid(comboBoxLecturerStudyPost.SelectedValue.ToString()),
                    LecturerDepartmentPostId = comboBoxLecturerDepartmentPost.SelectedValue != null ?
                     new Guid(comboBoxLecturerDepartmentPost.SelectedValue.ToString()) : (Guid?)null,
                    LastName = textBoxLastName.Text,
                    FirstName = textBoxFirstName.Text,
                    Patronymic = textBoxPatronymic.Text,
                    Abbreviation = textBoxAbbreviation.Text,
                    DateBirth = dateTimePickerDateBirth.Value,
                    Address = textBoxAddress.Text,
                    Email = textBoxEmail.Text,
                    MobileNumber = textBoxMobileNumber.Text,
                    HomeNumber = textBoxHomeNumber.Text,
                    Rank = comboBoxRank.Text,
                    Rank2 = comboBoxRank2.Text,
                    Description = textBoxDescription.Text,
                    Photo = (byte[])converter.ConvertTo(pictureBoxPhoto.Image, typeof(byte[])),
                    OnlyForPrivate = checkBoxOnlyForPrivate.Checked
                });
            }
            else
            {
                result = _service.UpdateLecturer(new LecturerSetBindingModel
                {
                    Id = _id.Value,
                    LecturerStudyPostId = new Guid(comboBoxLecturerStudyPost.SelectedValue.ToString()),
                    LecturerDepartmentPostId = comboBoxLecturerDepartmentPost.SelectedValue != null ?
                     new Guid(comboBoxLecturerDepartmentPost.SelectedValue.ToString()) : (Guid?)null,
                    LastName = textBoxLastName.Text,
                    FirstName = textBoxFirstName.Text,
                    Patronymic = textBoxPatronymic.Text,
                    Abbreviation = textBoxAbbreviation.Text,
                    DateBirth = dateTimePickerDateBirth.Value,
                    Address = textBoxAddress.Text,
                    Email = textBoxEmail.Text,
                    MobileNumber = textBoxMobileNumber.Text,
                    HomeNumber = textBoxHomeNumber.Text,
                    Rank = comboBoxRank.Text,
                    Rank2 = comboBoxRank2.Text,
                    Description = textBoxDescription.Text,
                    Photo = (byte[])converter.ConvertTo(pictureBoxPhoto.Image, typeof(byte[])),
                    OnlyForPrivate = checkBoxOnlyForPrivate.Checked
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
                ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                return false;
            }
        }

        private void ButtonUpload_Click(object sender, EventArgs e)
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
    }
}