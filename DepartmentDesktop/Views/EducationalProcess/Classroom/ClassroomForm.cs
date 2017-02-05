using DepartmentDAL.Enums;
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

namespace DepartmentDesktop.Views.EducationalProcess.Classroom
{
    public partial class ClassroomForm : Form
    {
        private readonly IClassroomService _service;

        private string _id;

        public ClassroomForm(IClassroomService service)
        {
            InitializeComponent();
            _service = service;
        }

        public ClassroomForm(IClassroomService service, string id)
        {
            InitializeComponent();
            _service = service;
            _id = id;
        }

        private void ClassroomForm_Load(object sender, EventArgs e)
        {
            foreach (var elem in Enum.GetValues(typeof(ClassroomTypes)))
            {
                comboBoxTypeClassroom.Items.Add(elem);
            }
            comboBoxTypeClassroom.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(_id))
            {
                var entity = _service.GetClassroom(new ClassroomGetBindingModel { Id = _id });
                if (entity == null)
                {
                    MessageBox.Show("Запись не найдена", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                comboBoxTypeClassroom.SelectedValue = entity.ClassroomType;
                textBoxClassroom.Text = _id;
                textBoxCapacity.Text = entity.Capacity.ToString();
            }
        }

        private bool CheckFill()
        {
            if (string.IsNullOrEmpty(comboBoxTypeClassroom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxClassroom.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxCapacity.Text))
            {
                return false;
            }
            int capacity = 0;
            if (!int.TryParse(textBoxCapacity.Text, out capacity))
            {
                return false;
            }
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e)

        {
            if (CheckFill())
            {
                if (string.IsNullOrEmpty(_id))
                {
                    var res = _service.CreateClassroom(new ClassroomRecordBindingModel
                    {
                        Id = textBoxClassroom.Text,
                        ClassroomType = comboBoxTypeClassroom.Text,
                        Capacity = Convert.ToInt32(textBoxCapacity.Text)
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
                    var res = _service.UpdateClassroom(new ClassroomRecordBindingModel
                    {
                        Id = textBoxClassroom.Text,
                        ClassroomType = comboBoxTypeClassroom.Text,
                        Capacity = Convert.ToInt32(textBoxCapacity.Text)
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
