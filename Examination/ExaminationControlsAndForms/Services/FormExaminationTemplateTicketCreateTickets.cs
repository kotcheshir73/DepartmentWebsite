using ControlsAndForms.Messangers;
using Enums;
using ExaminationInterfaces.BindingModels;
using ExaminationInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace ExaminationControlsAndForms.Services
{
    public partial class FormExaminationTemplateTicketCreateTickets : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private ITicketProcess _process;

        public FormExaminationTemplateTicketCreateTickets(ITicketProcess process, IExaminationTemplateBlockService serviceB, Guid examinationTemplateId)
        {
            InitializeComponent();
            _process = process;
            examinationTemplateBlockElement.Service = serviceB;
            examinationTemplateBlockElement.ExaminationTemplateId = examinationTemplateId;
        }

        private void FormExaminationTemplateTicketCreateTickets_Load(object sender, EventArgs e)
        {
            var blocks = examinationTemplateBlockElement.Service.GetExaminationTemplateBlocks(new ExaminationTemplateBlockGetBindingModel
            {
                ExaminationTemplateId = examinationTemplateBlockElement.ExaminationTemplateId
            });

            if (blocks.Succeeded)
            {
                panelBlockConfig.Controls.Clear();
                int x = 0;
                int y = 0;
                foreach (var block in blocks.Result.List)
                {
                    if(block.CountQuestionInTicket <= 0)
                    {
                        continue;
                    }
                    Panel panel = new Panel
                    {
                        Name = $"panel{block.Id}",
                        Size = new Size(410, 90),
                        Location = new Point(x, y),
                        BorderStyle = BorderStyle.Fixed3D
                    };

                    Label labelTitle = new Label
                    {
                        Dock = DockStyle.Top,
                        Location = new Point(0, 0),
                        Name = $"labelTitle{block.Id}",
                        Size = new Size(406, 25),
                        Text = block.BlockName,
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    panel.Controls.Add(labelTitle);

                    Label labelHowUseExaminationBlock = new Label
                    {
                        AutoSize = true,
                        Location = new Point(5, 35),
                        Name = $"labelHowUseExaminationBlock{block.Id}",
                        Size = new Size(184, 13),
                        Text = "Когда сбрасывать вопросы блока:"
                    };
                    panel.Controls.Add(labelHowUseExaminationBlock);

                    Label labelHowGetQuestionFromExaminationBlock = new Label
                    {
                        AutoSize = true,
                        Location = new Point(5, 62),
                        Name = $"labelHowGetQuestionFromExaminationBlock{block.Id}",
                        Size = new Size(184, 13),
                        Text = "Как брать вопросы из блока:"
                    };
                    panel.Controls.Add(labelHowGetQuestionFromExaminationBlock);

                    ComboBox comboBoxHowUseExaminationBlock = new ComboBox
                    {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        FormattingEnabled = true,
                        Location = new Point(200, 32),
                        Name = $"comboBoxHowUseExaminationBlock{block.Id}",
                        Size = new Size(200, 21)
                    };
                    if (block.IsCombine)
                    {
                        comboBoxHowUseExaminationBlock.Items.Add(HowUseExaminationBlock.СбросПередБилетомПриНехваткиБлоков);
                        comboBoxHowUseExaminationBlock.Items.Add(HowUseExaminationBlock.СбросПередБилетомПриНехваткиВопросов);
                        comboBoxHowUseExaminationBlock.Items.Add(HowUseExaminationBlock.СбросПриОкончанииСписка);
                    }
                    else
                    {
                        comboBoxHowUseExaminationBlock.Items.Add(HowUseExaminationBlock.СбросПередБилетом);
                        comboBoxHowUseExaminationBlock.Items.Add(HowUseExaminationBlock.СбросПриОкончанииСписка);
                    }
                    comboBoxHowUseExaminationBlock.SelectedIndex = 0;
                    panel.Controls.Add(comboBoxHowUseExaminationBlock);

                    ComboBox comboBoxHowGetQuestionFromExaminationBlock = new ComboBox
                    {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        FormattingEnabled = true,
                        Location = new Point(200, 59),
                        Name = $"comboBoxHowGetQuestionFromExaminationBlock{block.Id}",
                        Size = new Size(200, 21)
                    };
                    comboBoxHowGetQuestionFromExaminationBlock.Items.Add(HowGetQuestionFromExaminationBlock.ПоСписку);
                    comboBoxHowGetQuestionFromExaminationBlock.Items.Add(HowGetQuestionFromExaminationBlock.РандомныйВопрос);
                    comboBoxHowGetQuestionFromExaminationBlock.SelectedIndex = 0;
                    panel.Controls.Add(comboBoxHowGetQuestionFromExaminationBlock);

                    if (x + panel.Width > panelBlockConfig.Width)
                    {
                        x = 0;
                        y += panel.Height;
                    }
                    else
                    {
                        x += panel.Width;
                    }

                    panelBlockConfig.Controls.Add(panel);
                }
            }
            else
            {
                ErrorMessanger.PrintErrorMessage("При удалении возникла ошибка: ", blocks.Errors);
            }
        }

        private void RadioButtonWhileCanCreate_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void RadioButtonByCountTickets_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownCountTickets.Enabled = radioButtonByCountTickets.Checked;
        }

        private void RadioButtonBySelectedBlock_CheckedChanged(object sender, EventArgs e)
        {
            examinationTemplateBlockElement.Enabled = radioButtonBySelectedBlock.Checked;
        }

        private void ButtonCreateTickets_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите создать билеты?", "Создание билетов", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Dictionary<string, HowUseExaminationBlock> howUseBlock = new Dictionary<string, HowUseExaminationBlock>();
                Dictionary<string, HowGetQuestionFromExaminationBlock> howGetQuestionFromBlock = new Dictionary<string, HowGetQuestionFromExaminationBlock>();
                foreach (Panel panel in panelBlockConfig.Controls.Cast<Panel>())
                {
                    string name = string.Empty;
                    HowUseExaminationBlock howUseExaminationBlock = HowUseExaminationBlock.СбросПриОкончанииСписка;
                    HowGetQuestionFromExaminationBlock howGetQuestionFromExaminationBlock = HowGetQuestionFromExaminationBlock.ПоСписку;
                    foreach (var elem in panel.Controls)
                    {
                        if (elem is Label label)
                        {
                            if (label.Name.StartsWith("labelTitle"))
                            {
                                name = label.Text;
                            }
                        }
                        else if(elem is ComboBox comboBox)
                        {
                            if (comboBox.Name.StartsWith("comboBoxHowUseExaminationBlock"))
                            {
                                howUseExaminationBlock = (HowUseExaminationBlock)comboBox.SelectedItem;
                            }
                            if (comboBox.Name.StartsWith("comboBoxHowGetQuestionFromExaminationBlock"))
                            {
                                howGetQuestionFromExaminationBlock = (HowGetQuestionFromExaminationBlock)comboBox.SelectedItem;
                            }
                        }
                    }
                    howUseBlock.Add(name, howUseExaminationBlock);
                    howGetQuestionFromBlock.Add(name, howGetQuestionFromExaminationBlock);
                }

                HowCreateTickets howCreateTickets = radioButtonWhileCanCreate.Checked ? HowCreateTickets.ПокаВозможноСоздавать :
                    radioButtonByCountTickets.Checked ? HowCreateTickets.ПоКоличествуБилетов : HowCreateTickets.ПоВыбранномуБлоку;

                var result = _process.MakeTickets(new TicketProcessMakeTicketsBindingModel
                {
                    ExaminationTemplateId = examinationTemplateBlockElement.ExaminationTemplateId.Value,
                    HowCreateTickets = howCreateTickets,
                    HowUseBlock = howUseBlock,
                    HowGetQuestionFromBlock = howGetQuestionFromBlock,
                    CountTickets = Convert.ToInt32(numericUpDownCountTickets.Value),
                    SelectedBlock = examinationTemplateBlockElement.Id
                });
                if(result.Succeeded)
                {
                    MessageBox.Show("Билеты созданы", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ErrorMessanger.PrintErrorMessage("При создании возникла ошибка: ", result.Errors);
                }
            }
        }
    }
}