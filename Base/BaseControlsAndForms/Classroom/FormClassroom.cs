using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using Enums;
using System;
using Tools;
using Unity;

namespace BaseControlsAndForms.Classroom
{
    public partial class FormClassroom : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IClassroomService _service;

        public FormClassroom(IClassroomService service, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
        }

        protected override bool LoadComponents()
        {
            foreach (var elem in Enum.GetValues(typeof(ClassroomTypes)))
            {
                comboBoxTypeClassroom.Items.Add(elem.ToString());
            }
            comboBoxTypeClassroom.SelectedIndex = 0;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetClassroom(new ClassroomGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxTypeClassroom.SelectedIndex = comboBoxTypeClassroom.Items.IndexOf(entity.ClassroomType);
            textBoxClassroom.Text = entity.Number;
            textBoxCapacity.Text = entity.Capacity.ToString();
            checkBoxNotUseInSchedule.Checked = entity.NotUseInSchedule;
        }

        protected override bool CheckFill()
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
            if (!int.TryParse(textBoxCapacity.Text, out int capacity))
            {
                return false;
            }
            return true;
        }

        protected override bool Save()
        {
            ResultService result;
            if (!_id.HasValue)
            {
                result = _service.CreateClassroom(new ClassroomSetBindingModel
                {
                    Number = textBoxClassroom.Text,
                    ClassroomType = comboBoxTypeClassroom.Text,
                    Capacity = Convert.ToInt32(textBoxCapacity.Text),
                    NotUseInSchedule = checkBoxNotUseInSchedule.Checked
                });
            }
            else
            {
                result = _service.UpdateClassroom(new ClassroomSetBindingModel
                {
                    Id = _id.Value,
                    Number = textBoxClassroom.Text,
                    ClassroomType = comboBoxTypeClassroom.Text,
                    Capacity = Convert.ToInt32(textBoxCapacity.Text),
                    NotUseInSchedule = checkBoxNotUseInSchedule.Checked
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
    }
}