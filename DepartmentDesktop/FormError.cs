using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepartmentDesktop
{
	public partial class FormError : Form
	{
		public FormError()
		{
			InitializeComponent();
		}

		public void LoadData(string text, Dictionary<string, string> result)
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
