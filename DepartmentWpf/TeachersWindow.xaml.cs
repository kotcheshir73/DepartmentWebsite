using System;
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
    /// Логика взаимодействия для TeachersWindow.xaml
    /// </summary>
    public partial class TeachersWindow : Window
    {
        public TeachersWindow()
        {
            InitializeComponent();
        }

        private void addMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditTeacherWindow editTeacherWindow = new EditTeacherWindow();
            editTeacherWindow.ShowDialog();
        }

        private void editMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var info = this.dataGridTeachers.SelectedItem;
            EditTeacherWindow editTeacherWindow = new EditTeacherWindow(new Dictionary<string, string>());
            editTeacherWindow.ShowDialog();
        }
    }
}
