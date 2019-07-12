using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity.Attributes;
using Unity;
using DepartmentTablet.CustomControls;
using System.Collections;

namespace DepartmentTablet.Student
{
    public partial class ControlStudentConfig : UserControl
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private List<CustomControl> _controls { get; set; }

        private Dictionary<string, int> _controlIndexes = new Dictionary<string, int>
        {
            { "ControlAcademicYear", 0 },
            { "ControlEducationDirection", 1 },
            { "ControlStudentGroup", 2 },
            { "ControlStudent", 3 },
            { "ControlLectureDisciplines", 4 },
            { "ControlLectureDisciplineSemesters", 5 },
            { "ControlLectureDisciplineDetails", 6 },
        };

        public ControlStudentConfig()
        {
            InitializeComponent();
            _controls = new List<CustomControl>();
        }

        public void LoadData()
        {
            CustomControl control = Container.Resolve<ControlAcademicYear>();
            control.SelectValue += ControlAY_SelectValue;
            control.Title = "Учебные года";
            control.LoadData();
            _controls.Add(control);
            ShowControl(control);
        }

        private void Control_MoveBack(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                int index = Convert.ToInt32(control.Tag) - 1;
                _controls[index + 1].Visible = false;
                _controls[index].Visible = true;
                ShowControl(_controls[index]);
            }
        }

        private void ControlAY_SelectValue(object sender, EventArgs e)
        {
            int currentPos = _controlIndexes["ControlEducationDirection"];
            CustomControl control;
            if (_controls.Count <= currentPos)
            {
                control = Container.Resolve<ControlEducationDirection>();
                control.SelectValue += ControlED_SelectValue;
                control.MoveBack += Control_MoveBack;
                control.Tag = currentPos;
                control.Title = string.Format("{0} -> Направления", _controls[_controlIndexes["ControlAcademicYear"]].SelectedText);
                _controls.Add(control);
            }
            else
            {
                control = _controls[currentPos];
                control.Visible = true;
            }
            control.LoadData();
            _controls[currentPos - 1].Visible = false;
            ShowControl(_controls[currentPos]);
        }

        private void ControlED_SelectValue(object sender, EventArgs e)
        {
            int currentPos = _controlIndexes["ControlStudentGroup"];
            CustomControl control;
            if (_controls.Count <= currentPos)
            {
                control = Container.Resolve<ControlStudentGroup>();
                control.SelectValue += ControlSG_SelectValue;
                control.MoveBack += Control_MoveBack;
                control.Tag = currentPos;
                control.Title = string.Format("{0} -> {1} -> Группы", _controls[_controlIndexes["ControlAcademicYear"]].SelectedText,
                    _controls[_controlIndexes["ControlEducationDirection"]].SelectedText);
                _controls.Add(control);
            }
            else
            {
                control = _controls[currentPos];
                control.Visible = true;
            }
            control.LoadData(new ArrayList()
            {
                _controls[_controlIndexes["ControlEducationDirection"]].SelectedValue
            });
            _controls[currentPos - 1].Visible = false;
            ShowControl(_controls[currentPos]);
        }

        private void ControlSG_SelectValue(object sender, EventArgs e)
        {
            int currentPos = _controlIndexes["ControlStudent"];
            CustomControl control;
            if (_controls.Count <= currentPos)
            {
                control = Container.Resolve<ControlStudent>();
                control.SelectValue += ControlS_SelectValue;
                control.MoveBack += Control_MoveBack;
                control.Tag = currentPos;
                control.Title = string.Format("{0} -> {1} ->  {2} -> Студенты", _controls[_controlIndexes["ControlAcademicYear"]].SelectedText,
                    _controls[_controlIndexes["ControlEducationDirection"]].SelectedText, _controls[_controlIndexes["ControlStudentGroup"]].SelectedText);
                _controls.Add(control);
            }
            else
            {
                control = _controls[currentPos];
                control.Visible = true;
            }
            control.LoadData(new ArrayList()
            {
                _controls[_controlIndexes["ControlStudentGroup"]].SelectedValue
            });
            _controls[currentPos - 1].Visible = false;
            ShowControl(_controls[currentPos]);
        }

        private void ControlS_SelectValue(object sender, EventArgs e)
        {
            int currentPos = _controlIndexes["ControlLectureDisciplines"];
            CustomControl control;
            if (_controls.Count <= currentPos)
            {
                control = Container.Resolve<ControlLectureDisciplines>();
                control.SelectValue += ControlLD_SelectValue;
                control.MoveBack += Control_MoveBack;
                control.Tag = currentPos;
                control.Title = string.Format("{0} -> {1} ->  {2} -> {3} -> Дисциплины", _controls[_controlIndexes["ControlAcademicYear"]].SelectedText,
                    _controls[_controlIndexes["ControlEducationDirection"]].SelectedText, _controls[_controlIndexes["ControlStudentGroup"]].SelectedText, 
                    _controls[_controlIndexes["ControlStudent"]].SelectedText);
                _controls.Add(control);
            }
            else
            {
                control = _controls[currentPos];
                control.Visible = true;
            }
            control.LoadData(new ArrayList()
            {
                _controls[_controlIndexes["ControlAcademicYear"]].SelectedValue,
                _controls[_controlIndexes["ControlEducationDirection"]].SelectedValue
            });
            _controls[currentPos - 1].Visible = false;
            ShowControl(_controls[currentPos]);
        }

        private void ControlLD_SelectValue(object sender, EventArgs e)
        {
            int currentPos = _controlIndexes["ControlLectureDisciplineSemesters"];
            CustomControl control;
            if (_controls.Count <= currentPos)
            {
                control = Container.Resolve<ControlLectureDisciplineSemesters>();
                control.SelectValue += ControlLDS_SelectValue;
                control.MoveBack += Control_MoveBack;
                control.Tag = currentPos;
                control.Title = string.Format("{0} -> {1} -> {2} -> Семестры", _controls[_controlIndexes["ControlAcademicYear"]].SelectedText,
                    _controls[_controlIndexes["ControlEducationDirection"]].SelectedText, _controls[_controlIndexes["ControlLectureDisciplines"]].SelectedText);
                _controls.Add(control);
            }
            else
            {
                control = _controls[currentPos];
                control.Visible = true;
            }
            control.LoadData(new ArrayList()
            {
                _controls[_controlIndexes["ControlAcademicYear"]].SelectedValue,
                _controls[_controlIndexes["ControlEducationDirection"]].SelectedValue,
                _controls[_controlIndexes["ControlLectureDisciplines"]].SelectedValue
            });
            _controls[currentPos - 1].Visible = false;
            ShowControl(_controls[currentPos]);
        }

        private void ControlLDS_SelectValue(object sender, EventArgs e)
        {
            //int currentPos = _controlIndexes["ControlLectureDisciplineDetails"];
            //CustomControl control;
            //if (_controls.Count <= currentPos)
            //{
            //    control = Container.Resolve<ControlLectureDisciplineDetails>();
            //    control.SelectValue += ControlLDD_SelectValue;
            //    control.MoveBack += Control_MoveBack;
            //    control.Tag = currentPos;
            //    control.Title = string.Format("{0} -> {1} -> {2} -> {3} -> Нагрузка", _controls[_controlIndexes["ControlAcademicYear"]].SelectedText,
            //        _controls[_controlIndexes["ControlEducationDirection"]].SelectedText, _controls[_controlIndexes["ControlLectureDisciplines"]].SelectedText,
            //        _controls[_controlIndexes["ControlLectureDisciplineSemesters"]].SelectedText);
            //    _controls.Add(control);
            //}
            //else
            //{
            //    control = _controls[currentPos];
            //    control.Visible = true;
            //}
            //control.LoadData(new ArrayList()
            //{
            //    _controls[_controlIndexes["ControlAcademicYear"]].SelectedValue,
            //    _controls[_controlIndexes["ControlEducationDirection"]].SelectedValue,
            //    _controls[_controlIndexes["ControlLectureDisciplines"]].SelectedValue
            //});
            //_controls[currentPos - 1].Visible = false;
            //ShowControl(_controls[currentPos]);
        }

        private void ShowControl(UserControl control)
        {
            while (Controls.Count > 1)
            {
                Controls.RemoveAt(Controls.Count - 1);
            }
            Controls.Add(control);
            control.Left = 0;
            control.Top = 0;
            control.Height = Height;
            control.Width = Width;
            control.Anchor = (((AnchorStyles.Top
                        | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right);
        }
    }
}
