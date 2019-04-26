using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using BaseInterfaces.BindingModels;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.LecturerWorkload
{
    public partial class FormLecturerWorkload : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ILecturerWorkloadService _service;

        private Guid _ayId;

        public FormLecturerWorkload(ILecturerWorkloadService service, Guid ayId, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _ayId = ayId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        protected override bool LoadComponents()
        {
            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { Id = _ayId });
            if (!resultAY.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return false;
            }

            var resultL = _service.GetLecturers(new LecturerGetBindingModel { });
            if (!resultL.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке преподавателей возникла ошибка: ", resultL.Errors);
                return false;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
            comboBoxAcademicYear.SelectedItem = _ayId;

            comboBoxLecturer.ValueMember = "Value";
            comboBoxLecturer.DisplayMember = "Display";
            comboBoxLecturer.DataSource = resultL.Result.List
                .Select(ed => new { Value = ed.Id, Display = ed.FullName }).ToList();
            comboBoxLecturer.SelectedItem = null;

            return true;
        }

        protected override void LoadData()
        {
            var result = _service.GetLecturerWorkload(new LecturerWorkloadGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
            comboBoxLecturer.SelectedValue = entity.LecturerId;
            textBoxWorkload.Text = entity.Workload.ToString();
        }

        protected override bool CheckFill()
        {
            if (comboBoxAcademicYear.SelectedValue == null)
            {
                return false;
            }
            if (comboBoxLecturer.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxWorkload.Text))
            {
                return false;
            }
            if (!double.TryParse(textBoxWorkload.Text, out double count))
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
                result = _service.CreateLecturerWorkload(new LecturerWorkloadSetBindingModel
                {
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    LecturerId = new Guid(comboBoxLecturer.SelectedValue.ToString()),
                    Workload = Convert.ToDouble(textBoxWorkload.Text)
                });
            }
            else
            {
                result = _service.UpdateLecturerWorkload(new LecturerWorkloadSetBindingModel
                {
                    Id = _id.Value,
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    LecturerId = new Guid(comboBoxLecturer.SelectedValue.ToString()),
                    Workload = Convert.ToDouble(textBoxWorkload.Text)
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