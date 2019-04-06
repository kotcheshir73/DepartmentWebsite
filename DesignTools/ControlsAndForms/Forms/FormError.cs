using System.Collections.Generic;
using System.Windows.Forms;

namespace ControlsAndForms.Forms
{
    public partial class FormError : Form
	{
		public FormError()
		{
			InitializeComponent();
		}

		public void LoadData(string text, List<KeyValuePair<string, string>> result)
		{
			Text = text;
			foreach (var err in result)
			{
				dataGridViewErrors.Rows.Add();
				int index = dataGridViewErrors.Rows.Count - 1;
				dataGridViewErrors.Rows[index].Cells[0].Value = err.Key;
				dataGridViewErrors.Rows[index].Cells[1].Value = err.Value;
			}
			ShowDialog();
		}
	}
}
