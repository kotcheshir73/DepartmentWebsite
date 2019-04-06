using ControlsAndForms.Forms;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ControlsAndForms.Messangers
{
    public static class ErrorMessanger
    {
        public static void PrintErrorMessage(string text, List<KeyValuePair<string, string>> result)
        {
            if(result.Count == 1)
            {
                MessageBox.Show(result[0].Value, result[0].Key, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FormError form = new FormError();
                form.LoadData(text, result);
            }
        }
    }
}