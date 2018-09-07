using DepartmentService.BindingModels;
using DepartmentService.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace DepartmentDesktop.Views.LearningProgress.DisciplineLesson.DisciplineLessonTask.DisciplineLessonTaskVariant
{
    public partial class DisciplineLessonTaskVariantForm : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineLessonTaskVariantService _service;

        private Guid? _id = null;

        private Guid? _dltId = null;

        public DisciplineLessonTaskVariantForm(IDisciplineLessonTaskVariantService service, Guid? dltId = null, Guid? id = null)
        {
            InitializeComponent();
            _service = service;
            _dltId = dltId;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void DisciplineLessonTaskVariantForm_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            var result = _service.GetDisciplineLessonTaskVariant(new DisciplineLessonTaskVariantGetBindingModel { Id = _id.Value });
            if (!result.Succeeded)
            {
                Program.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
                Close();
            }
            var entity = result.Result;

            textBoxVariantNumber.Text = entity.VariantNumber;
            textBoxVariantTask.Text = entity.VariantTask;
        //    textBoxOrder.Text = entity.o
        }
    }
}
