﻿using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentTablet.CustomControls
{
    public partial class ControlStudent : CustomControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStudentService _service;

        public ControlStudent(IStudentService service)
        {
            InitializeComponent();
            Font = Program.Font;
            _service = service;
            _elemensInRow = 5;
        }

        public override void LoadData(ArrayList list = null)
        {
            var result = _service.GetStudents(new StudentGetBindingModel
            {
                StudentGroupId = new Guid(list[0].ToString())
            });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                return;
            }
            panelMain.Controls.Clear();
            int i = 0;
            int j = 0;
            foreach (var res in result.Result.List)
            {
                Button button = new Button
                {
                    Location = new Point(10 + j * 350, 10 + i * 200),
                    Name = string.Format("button{0}{1}", i, j),
                    Size = new Size(300, 150),
                    TabIndex = i + j,
                    Tag = res.Id,
                    Text = res.FullName,
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
