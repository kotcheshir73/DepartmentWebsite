﻿using BaseInterfaces.BindingModels;
using BaseInterfaces.Interfaces;
using ControlsAndForms.Forms;
using ControlsAndForms.Messangers;
using System;
using System.Windows.Forms;
using Tools;
using Unity;

namespace BaseControlsAndForms.DisciplineBlock
{
    public partial class FormDisciplineBlock : StandartForm
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IDisciplineBlockService _service;

		public FormDisciplineBlock(IDisciplineBlockService service, Guid? id = null) : base(id)
        {
			InitializeComponent();
			_service = service;
        }

		private void FormDisciplineBlock_Load(object sender, EventArgs e)
		{
            StandartForm_Load(sender, e);
		}

        protected override void LoadData()
        {
            var result = _service.GetDisciplineBlock(new DisciplineBlockGetBindingModel { Id = _id.Value });
			if (!result.Succeeded)
			{
                ErrorMessanger.PrintErrorMessage("При загрузке возникла ошибка: ", result.Errors);
				Close();
			}
			var entity = result.Result;

			textBoxTitle.Text = entity.Title;
            textBoxDisciplineBlockBlueAsteriskName.Text = entity.DisciplineBlockBlueAsteriskName;
            checkBoxDisciplineBlockUseForGrouping.Checked = entity.DisciplineBlockUseForGrouping;
            textBoxDisciplineBlockOrder.Text = entity.DisciplineBlockOrder.ToString();
		}

		private bool CheckFill()
		{
			if (string.IsNullOrEmpty(textBoxTitle.Text))
			{
				return false;
            }
            if (string.IsNullOrEmpty(textBoxDisciplineBlockOrder.Text))
            {
                return false;
            }
            int order = 0;
            if (!int.TryParse(textBoxDisciplineBlockOrder.Text, out order))
            {
                return false;
            }
            return true;
		}

        protected override bool Save()
		{
			if (CheckFill())
			{
				ResultService result;
				if (!_id.HasValue)
				{
					result = _service.CreateDisciplineBlock(new DisciplineBlockSetBindingModel
					{
						Title = textBoxTitle.Text,
                        DisciplineBlockBlueAsteriskName = textBoxDisciplineBlockBlueAsteriskName.Text,
                        DisciplineBlockUseForGrouping = checkBoxDisciplineBlockUseForGrouping.Checked,
                        DisciplineBlockOrder = Convert.ToInt32(textBoxDisciplineBlockOrder.Text)
                    });
				}
				else
				{
					result = _service.UpdateDisciplineBlock(new DisciplineBlockSetBindingModel
					{
						Id = _id.Value,
						Title = textBoxTitle.Text,
                        DisciplineBlockBlueAsteriskName = textBoxDisciplineBlockBlueAsteriskName.Text,
                        DisciplineBlockUseForGrouping = checkBoxDisciplineBlockUseForGrouping.Checked,
                        DisciplineBlockOrder = Convert.ToInt32(textBoxDisciplineBlockOrder.Text)
					});
				}
				if (result.Succeeded)
				{
					if (result.Result != null)
					{
						if (result.Result is Guid)
						{
							_id = (Guid)result.Result;
						}
					}
					return true;
				}
				else
				{
                    ErrorMessanger.PrintErrorMessage("При сохранении возникла ошибка: ", result.Errors);
					return false;
				}
			}
			else
			{
				MessageBox.Show("Заполните все обязательные поля", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}
	}
}