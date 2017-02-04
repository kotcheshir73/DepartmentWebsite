using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepartmentDesktop.Views.Users
{
    public partial class UserF : Form
    {
        private int _id;

        //private Models.InterfaceModel<User> _model;

        public UserF(int id)
        {
            InitializeComponent();
            _id = id;
          //  _model = new Models.UserModel();
        }

        private void UserF_Load(object sender, EventArgs e)
        {
            if(_id != 0)
            {
                //var elem = _model.GetElem(_id);
                //textBoxId.Text = String.Format("{0:0000}", elem.Id);
                //labelCreate.Text = elem.DateCreate.ToLongDateString();
                //labelDateLastVisit.Text = elem.DateLastVisit.ToLongDateString();
                //checkBoxBan.Checked = elem.Ban;

                //textBoxLogin.Text = elem.Login;
                //textBoxPassword.Text = elem.Password;

                //роль
                //
            }
        }
    }
}
