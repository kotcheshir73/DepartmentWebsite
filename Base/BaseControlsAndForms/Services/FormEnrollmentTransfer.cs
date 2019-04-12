using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Messangers;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Unity;

namespace BaseControlsAndForms.Services
{
    public partial class FormEnrollmentTransfer : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IProcess _process;

        private Guid? _id = null;

        public FormEnrollmentTransfer(IProcess process, Guid? id = null)
        {
            InitializeComponent();
            _process = process;
            if (id != Guid.Empty)
            {
                _id = id;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxEnrollmentTransferOrderNumber.Text))
            {
                MessageBox.Show("Введите номер приказа на зачисление", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridViewStudents.Rows.Count == 1)
            {
                MessageBox.Show("Укажите хотя бы одного студента", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var list = new List<StudentSetBindingModel>();
            for (int i = 0; i < dataGridViewStudents.Rows.Count - 1; ++i)
            {
                var model = new StudentSetBindingModel
                {
                    NumberOfBook = dataGridViewStudents.Rows[i].Cells[0].Value.ToString(),
                    LastName = dataGridViewStudents.Rows[i].Cells[1].Value.ToString(),
                    FirstName = dataGridViewStudents.Rows[i].Cells[2].Value.ToString(),
                    Patronymic = dataGridViewStudents.Rows[i].Cells[3].Value.ToString(),
                    Description = dataGridViewStudents.Rows[i].Cells[5].Value?.ToString() ?? string.Empty,
                    StudentGroupId = _id.Value,
                    Email = "неизвестно"
                };
                list.Add(model);
            }
            var result = _process.EnrollmentTransferStudents(new StudentEnrollmentTransferBindingModel
            {
                EnrollmentTransferOrderNumber = textBoxEnrollmentTransferOrderNumber.Text,
                EnrollmentTransferOrderDate = dateTimePickerEnrollmentTransferDate.Value,
                StudentList = list
            });
            if (result.Succeeded)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("Ошибка при сохранении спсика: ", result.Errors);
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DataGridViewStudents_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                var buffer = Clipboard.GetText();
                if (Regex.IsMatch(buffer, @"[\w]+ \w.( )?\w."))
                {
                    var strs = buffer.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string lastName = strs[0];
                    string firstname = strs[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    string patron = strs.Length == 2 ? strs[1].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[1] :
                        strs[2].Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    dataGridViewStudents.Rows.Add(new object[] { "н/а", lastName, firstname, patron, "очная" });
                }
                else if (Regex.IsMatch(buffer, @"[\w\/]+\t[\w]+\t[\w]+\t[\w]+"))
                {
                    var strs = buffer.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    dataGridViewStudents.Rows.Add(new object[] { strs[0], strs[1], strs[2], strs[3], "очная" });
                }
            }
        }
    }
}