﻿using ControlsAndForms.Messangers;
using DatabaseContext;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DepartmentDesktop
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void ButtonEnter_Click(object sender, EventArgs e)
        {
            try
            {
                DepartmentUserManager.Login(textBoxLogin.Text, textBoxPassword.Text);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch(Exception ex)
            {
                ErrorMessanger.PrintErrorMessage("При аутентфикации возникла ошибка: ", new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Аутентфикация", ex.Message) });
            }
        }
    }
}