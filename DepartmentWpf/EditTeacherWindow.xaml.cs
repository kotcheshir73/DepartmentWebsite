using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DepartmentWpf
{
    /// <summary>
    /// Логика взаимодействия для EditTeacherWindow.xaml
    /// </summary>
    public partial class EditTeacherWindow : Window
    {
        public EditTeacherWindow()
        {
            InitializeComponent();
        }

        public EditTeacherWindow(Dictionary<string, string> info)
        {
            InitializeComponent();

            textBoxFirstName.Text = "Евгений";
            textBoxLastName.Text = "Эгов";
            textBoxPatronymic.Text = "НИколаевич";
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
