using DepartmentDesktop.Views.EducationalProcess.AcademicYear.StreamLesson.StreamLessonRecord;
using DepartmentModel;
using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

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
            comboBoxAcademicYear.SelectedItem = _ayId;

            var control = Container.Resolve< StreamLessonRecordControl> ();

            control.Left = 0;
            control.Top = 0;
            control.Height = Height - 60;
            control.Width = Width - 15;
            control.Anchor = (((AnchorStyles.Top
                    | AnchorStyles.Bottom)
                    | AnchorStyles.Left)
                    | AnchorStyles.Right);

            tabPageRecords.Controls.Add(control);

            if (_id.HasValue)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            (tabPageRecords.Controls[0] as StreamLessonRecordControl).LoadData(_id.Value);
            var result = _service.GetStreamLesson(new StreamLessonGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;
            
            comboBoxAcademicYear.SelectedValue = entity.AcademicYearId;
            textBoxStreamLessonName.Text = entity.StreamLessonName;
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
            return true;
        }

        private bool Save()
        {
            if (CheckFill())
            {
                ResultService result;
                if (!_id.HasValue)
                {
                    result = _service.CreateStreamLesson(new StreamLessonRecordBindingModel
                    {
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                        StreamLessonName = textBoxStreamLessonName.Text
                    });
                }
                else
                {
                    result = _service.UpdateStreamLesson(new StreamLessonRecordBindingModel
                    {
                        Id = _id.Value,
                        AcademicYearId = new Guid(comboBoxAcademicYear.SelectedValue.ToString()),
                        StreamLessonName = textBoxStreamLessonName.Text
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
