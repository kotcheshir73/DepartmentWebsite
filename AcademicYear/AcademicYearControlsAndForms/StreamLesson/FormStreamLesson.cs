using AcademicYearControlsAndForms.StreamLessonRecord;
using AcademicYearInterfaces.BindingModels;
using AcademicYearInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Tools;
using Unity;

namespace AcademicYearControlsAndForms.StreamLesson
{
    public partial class FormStreamLesson : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStreamLessonService _service;

        private Guid? _ayId = null;

        public FormStreamLesson(IStreamLessonService service, Guid? ayId = null, Guid? id = null) : base(id)
        {
            InitializeComponent();
            _service = service;
            _ayId = ayId;
        }

        protected override bool LoadComponents()
        {
            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return false;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
            comboBoxAcademicYear.SelectedValue = _ayId;

            return true;
        }

        protected override void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<ControlStreamLessonRecord>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as ControlStreamLessonRecord).LoadData(_id.Value, _ayId.Value);

            var result = _service.GetStreamLesson(new StreamLessonGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
            textBoxStreamLessonName.Text = entity.StreamLessonName;
            textBoxStreamLessonHours.Text = entity.StreamLessonHours.ToString();
        }

        protected override bool CheckFill()
        {
            if (comboBoxAcademicYear.SelectedValue == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxStreamLessonName.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(textBoxStreamLessonHours.Text))
            {
                return false;
            }
            decimal hours = 0;
            if (!decimal.TryParse(textBoxStreamLessonHours.Text, out hours))
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
                result = _service.CreateStreamLesson(new StreamLessonSetBindingModel
                {
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    StreamLessonName = textBoxStreamLessonName.Text,
                    StreamLessonHours = Convert.ToDecimal(textBoxStreamLessonHours.Text)
                });
            }
            else
            {
                result = _service.UpdateStreamLesson(new StreamLessonSetBindingModel
                {
                    Id = _id.Value,
                    AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                    StreamLessonName = textBoxStreamLessonName.Text,
                    StreamLessonHours = Convert.ToDecimal(textBoxStreamLessonHours.Text)
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