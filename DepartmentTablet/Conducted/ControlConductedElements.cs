using DepartmentService.BindingModels;
using DepartmentService.IServices;
using DepartmentTablet.CustomControls;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentTablet.Conducted
{
    public partial class ControlConductedElements : CustomControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILearningProgressProcess _process;

        public ControlConductedElements(ILearningProgressProcess process)
        {
            InitializeComponent();
            Font = Program.Font;
            _process = process;
            _elemensInRow = 3;
        }

        public override void LoadData(ArrayList list = null)
        {
            var result = _process.GetFullDisciplineLessonConducteds(new FullDisciplineLessonConductedBindingModel
            {
                AcademicYearId = new Guid(list[0].ToString()),
                EducationDirectionId = new Guid(list[1].ToString()),
                DisciplineId = new Guid(list[2].ToString()),
                Semester = list[3].ToString(),
                TimeNormId = new Guid(list[4].ToString()),
                StudentGroupId = new Guid(list[5].ToString())
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
            panelMain.Controls.Clear();
            int i = 0;
            int j = 0;
            foreach (var res in result.Result)
            {
                Button button = new Button
                {
                    Location = new Point(10 + j * 350, 10 + i * 200),
                    Name = string.Format("button{0}{1}", i, j),
                    Size = new Size(300, 150),
                    TabIndex = i + j,
                    Tag = res.Id,
                    Text = string.Format("{0}{1}{2}", res.DisciplineLesson, Environment.NewLine, res.Subgroup),
                    UseVisualStyleBackColor = true,
                    Cursor = Cursors.Hand
                };
                button.Click += (sender, e) =>
                {
                    if (sender is Button but)
                    {
                        _selectedText = but.Text;
                        _selectedValue = but.Tag.ToString();
                        RunClick(this, e);
                    }
                };
                if (j < _elemensInRow)
                {
                    j++;
                }
                else if (j == _elemensInRow)
                {
                    j = 0;
                    i++;
                }
                panelMain.Controls.Add(button);
            }
        }
    }
}
