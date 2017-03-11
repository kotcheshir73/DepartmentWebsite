using DepartmentDAL;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.EducationalProcess.StudentGroup
{
    public partial class StudentGroupForm : Form
    {
        private readonly IStudentGroupService _service;
    
        private readonly IStudentService _serviceS;

        private long _id = 0;

        public StudentGroupForm(IStudentGroupService service, IStudentService serviceS)
        {
            InitializeComponent();
            _service = service;
            _serviceS = serviceS;
        }

        public StudentGroupForm(IStudentGroupService service, IStudentService serviceS, long id)
        {
            InitializeComponent();
            _service = service;
            _serviceS = serviceS;
            _id = id;
        }

        private void StudentGroupForm_Load(object sender, EventArgs e)
        {
            comboBoxEducationDirection.ValueMember = "Value";
            comboBoxEducationDirection.DisplayMember = "Display";
            comboBoxEducationDirection.DataSource = _service.GetEducationDirections()
                .Select(ed => new { Value = ed.Id, Display = ed.Cipher + " " + ed.Title }).ToList();

            var control = new StudentGroupStudentsControl(_serviceS);
            control.Left = 0;
            control.Top = 0;
            control.Height = Height - 60;
            control.Width = Width - 15;
            control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                        | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            tabPageStudents.Controls.Add(control);

            if (_id != 0)
            {
                control.LoadData(_id);
                var entity = _service.GetStudentGroup(new StudentGroupGetBindingModel { Id = _id });
                if (entity == null)
                {
                    MessageBox.Show("Запись не найдена", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                comboBoxEducationDirection.SelectedValue = entity.EducationDirectionId;
                textBoxGroupName.Text = entity.GroupName;
                textBoxKurs.Text = entity.Kurs.ToString();
                textBoxCapacity.Text = entity.Capacity.ToString();
                textBoxSubgroupsCount.Text = entity.SubgroupsCount.ToString();
            }
        }

        private bool CheckFill()
        {
            if (comboBoxEducationDirection.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxGroupName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxKurs.Text))
            {
                return false;
            }
            int kurs = 0;
            if (!int.TryParse(textBoxKurs.Text, out kurs))
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
                    result = _service.CreateStudentGroup(new StudentGroupRecordBindingModel
                    {
                        EducationDirectionId = Convert.ToInt64(comboBoxEducationDirection.SelectedValue),
                        GroupName = textBoxGroupName.Text,
                        Kurs = Convert.ToInt32(textBoxKurs.Text)
                    });
                }
                else
                {
                    result = _service.UpdateStudentGroup(new StudentGroupRecordBindingModel
                    {
                        Id = _id,
                        EducationDirectionId = Convert.ToInt64(comboBoxEducationDirection.SelectedValue),
                        GroupName = textBoxGroupName.Text,
                        Kurs = Convert.ToInt32(textBoxKurs.Text)
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
