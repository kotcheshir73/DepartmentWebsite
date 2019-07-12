using DepartmentTablet.CustomControls;
using Enums;
using LearningProgressInterfaces.BindingModels;
using LearningProgressInterfaces.Interfaces;
using System;
using System.Collections;
using Unity;
using Unity.Attributes;

namespace DepartmentTablet.Conducted
{
    public partial class ControlConductedStudentMark : CustomControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonConductedStudentService _service;

        private Guid _id;

        private Guid _disciplineLessonConductedId;

        private Guid _studentId;

        public ControlConductedStudentMark(IDisciplineLessonConductedStudentService service)
        {
            InitializeComponent();
            Font = Program.Font;
            _service = service;
            _elemensInRow = 0;
        }

        public override void LoadData(ArrayList list = null)
        {
            foreach (var elem in Enum.GetValues(typeof(DisciplineLessonStudentStatus)))
            {
                comboBoxStatus.Items.Add(elem.ToString());
            }
            comboBoxStatus.SelectedIndex = -1;
            _id = new Guid(list[0].ToString());
            var result = _service.GetDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentGetBindingModel { Id = _id });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
            var entity = result.Result;

            _disciplineLessonConductedId = entity.DisciplineLessonConductedId;
            _studentId = entity.StudentId;
            comboBoxStatus.SelectedIndex = comboBoxStatus.Items.IndexOf(entity.Status.ToString());
            checkBoxBall.Checked = entity.Ball.HasValue;
            textBoxBall.Text = entity.Ball?.ToString() ?? "";
            textBoxComment.Text = entity.Comment;
        }

        private void checkBoxBall_CheckedChanged(object sender, EventArgs e)
        {
            textBoxBall.Enabled = checkBoxBall.Checked;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var result = _service.UpdateDisciplineLessonConductedStudent(new DisciplineLessonConductedStudentSetBindingModel
            {
                Id = _id,
                DisciplineLessonConductedId = _disciplineLessonConductedId,
                StudentId = _studentId,
                Status = comboBoxStatus.Text,
                Ball = checkBoxBall.Checked ? Convert.ToInt32(textBoxBall.Text) : (int?)null,
                Comment = textBoxComment.Text
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
            }
        }
    }
}
