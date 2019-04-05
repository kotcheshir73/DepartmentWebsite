using DepartmentDesktop.Views.EducationalProcess.AcademicYear.StreamLesson.StreamLessonRecord;
using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace DepartmentDesktop.Views.EducationalProcess.AcademicYear.StreamLesson
{
    public partial class StreamLessonForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStreamLessonService _service;

        private Guid? _id = null;

        private Guid? _ayId = null;

        public StreamLessonForm(IStreamLessonService service, Guid? ayId = null, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _ayId = ayId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void StreamLessonForm_Load(object sender, EventArgs e)
        {
            var resultAY = _service.GetAcademicYears(new AcademicYearGetBindingModel { });
            if (!resultAY.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке учебных годов возникла ошибка: ", resultAY.Errors);
                return;
            }

            comboBoxAcademicYear.ValueMember = "Value";
            comboBoxAcademicYear.DisplayMember = "Display";
            comboBoxAcademicYear.DataSource = resultAY.Result.List
                .Select(ay => new { Value = ay.Id, Display = ay.Title }).ToList();
            comboBoxAcademicYear.SelectedValue = _ayId;

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            if (tabPageRecords.Controls.Count == 0)
            {
                var control = Container.Resolve<StreamLessonRecordControl>();
                control.Dock = DockStyle.Fill;
                tabPageRecords.Controls.Add(control);
            }
            (tabPageRecords.Controls[0] as StreamLessonRecordControl).LoadData(_id.Value, _ayId.Value);

            var result = _service.GetStreamLesson(new StreamLessonGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;
            
            comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
            textBoxStreamLessonName.Text = entity.StreamLessonName;
            textBoxStreamLessonHours.Text = entity.StreamLessonHours.ToString();
        }

        private bool CheckFill()
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

        private bool Save()
        {
            if (CheckFill())
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
                    Program.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("Сохранение прошло успешно", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
