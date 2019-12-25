using Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScheduleControlsAndForms.BaseControls
{
    public class ButtonShare : Button
    {
        private readonly Color ConsultColor = Color.LightGreen;

        private readonly Color ExamColor = Color.DarkCyan;

        private readonly Color OffsetColor = Color.SandyBrown;

        private ScheduleRecordType _scheduleRecordType;

        public Guid? Id { get; set; }

        public List<Guid> Ids { get; set; }

        public ScheduleRecordType ScheduleRecordType
        {
            get { return _scheduleRecordType; }
            set
            {
                _scheduleRecordType = value;
                switch (_scheduleRecordType)
                {
                    case ScheduleRecordType.Consultation:
                        BackColor = ConsultColor;
                        break;
                    case ScheduleRecordType.Examination:
                        BackColor = ExamColor;
                        break;
                    case ScheduleRecordType.Offset:
                        BackColor = OffsetColor;
                        break;
                    case ScheduleRecordType.Semester:
                        break;
                }
            }
        }

        public ButtonShare()
        {
            Location = new Point(0, 0);
            Dock = DockStyle.Fill;
            Margin = new Padding(0);
        }
    }
}