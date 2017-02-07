using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DepartmentDAL;

namespace DepartmentDesktop.Controllers
{
    public partial class ListUsersUC : UserControl
    {
       // private DepartmentDbContext _context;

        public ListUsersUC()
        {
            InitializeComponent();
         //   _context = new DepartmentDbContext();
        }

        public void LoadData()
        {
            //var list = _model.GetList();
            //if(list == null)
            //{
            //    return;
            //}
            //dataGridViewList.DataSource = list.Select(r => new
            //{
            //    Id = r.Id,
            //    Логин = r.Login,
            //    Роль = r.Role.RoleName,
            //    Дата_создания = r.DateCreate.ToLongDateString(),
            //    Дата_посещения = r.DateLastVisit.ToLongDateString(),
            //    Активность = !r.IsBanned,
            //    Ссылка = (r.Lecturer != null) ? "Преподаватель" : (r.Student != null) ? "Студент" : "Нет"
            //});
            //if(dataGridViewList.Columns.Count > 0)
            //{
            //    dataGridViewList.Columns[0].Visible = false;
            //}
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonUpd_Click(object sender, EventArgs e)
        {

        }
    }
}
