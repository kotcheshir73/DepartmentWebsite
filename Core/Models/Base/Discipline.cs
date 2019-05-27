using Models.AcademicYearData;
using Models.Attributes;
using Models.Examination;
using Models.LearningProgress;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Models.Base
{
    /// <summary>
    /// Класс, описывающий дисциплину
    /// </summary>
    [DataContract]
    [ClassUse("DisciplineBlock", "DisciplineBlockId", "Все дисциплины привязываются к блокам в учебных планах")]
    public class Discipline : BaseEntity
	{
        [Required]
        [DataMember]
        public Guid DisciplineBlockId { get; set; }

        [DataMember]
        public Guid? DisciplineParentId { get; set; }

        [DataMember]
        public bool IsParent { get; set; }

        [MaxLength(200)]
		[Required]
        [DataMember]
        public string DisciplineName { get; set; }
        
        [MaxLength(20)]
        [DataMember]
        public string DisciplineShortName { get; set; }

        [DataMember]
        public string DisciplineDescription { get; set; }

        [MaxLength(200)]
        [DataMember]
        public string DisciplineBlueAsteriskName { get; set; }

        [NotMapped]
        public string DisciplineBlueAsteriskCode { get; set; }

        [NotMapped]
        public string DisciplineBlueAsteriskPracticCode { get; set; }

        [NotMapped]
        public bool NotSelected { get; set; }

        //-------------------------------------------------------------------------

        public virtual DisciplineBlock DisciplineBlock { get; set; }

        //-------------------------------------------------------------------------

        [ForeignKey("DisciplineId")]
		public virtual List<AcademicPlanRecord> AcademicPlanRecord { get; set; }

		[ForeignKey("DisciplineId")]
		public virtual List<DisciplineLesson> DisciplineLessons { get; set; }

		[ForeignKey("DisciplineId")]
		public virtual List<DisciplineStudentRecord> DisciplineStudentRecords { get; set; }

        [ForeignKey("DisciplineId")]
        public virtual List<Statement> Statements { get; set; }

        [ForeignKey("DisciplineId")]
        public virtual List<ExaminationList> ExaminationLists { get; set; }

        [ForeignKey("DisciplineId")]
        public virtual List<ExaminationTemplate> ExaminationTemplates { get; set; }

        //-------------------------------------------------------------------------

        public override string ToString()
        {
            if(!string.IsNullOrEmpty(DisciplineShortName))
            {
                return DisciplineShortName;
            }
            if(string.IsNullOrEmpty(DisciplineName))
            {
                return string.Empty;
            }
            // TODO избавиться от '-ия' и т.п.
            StringBuilder sb = new StringBuilder();
            var glas = new List<char> { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
            var predlogs = new List<string> { "в", "без", "до", "и", "из", "к", "на", "по", "о", "от", "перед", "при", "через", "с", "у", "за", "над", "об", "под", "для" };
            var strsSpice = DisciplineName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            // если одно слово, и оно не содержит деифсов и точек, то просто берем первые 8 символов
            if (strsSpice.Length == 1 && !strsSpice[0].Contains("-") && !strsSpice[0].Contains("."))
            {
                int length = strsSpice[0].Length > 8 ? 8 : strsSpice[0].Length;
                sb.Append(string.Format("{0}.", strsSpice[0].Substring(0, length)));
            }
            else
            {
                foreach (var strSpice in strsSpice)
                {
                    // если слово содержит дефис и оно одно, то каждое слово сокращаем до 3-4 знаков
                    if (strSpice.Contains("-") && strsSpice.Length == 1)
                    {
                        var substrs = strSpice.Split(new char[] { '-', '.' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var subS in substrs)
                        {
                            for (int t = 0; t < subS.Length; ++t)
                            {
                                if (t < 3)
                                {
                                    sb.Append(subS[t]);
                                }
                                else if (!glas.Contains(subS[t]))
                                {
                                    sb.Append(subS[t]);
                                }
                                else
                                {
                                    sb.Append('.');
                                    break;
                                }
                            }
                            sb.Append('-');
                        }
                        sb = sb.Remove(sb.Length - 1, 1);
                    }
                    // иначе
                    else
                    {
                        // разбиваем строку на подстроки по точке и дефису
                        var substrs = strSpice.Split(new char[] { '.', '-' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var subS in substrs)
                        {
                            // если строка - предлог, то берем тока первый симвов а нижнем регистре
                            if (predlogs.Contains(subS.ToLower()))
                            {
                                sb.Append(subS.ToLower()[0]);
                            }
                            // слово может быть целиком уже аббревиатурой
                            else if (subS.ToUpper() == subS)
                            {
                                sb.Append(subS);
                            }
                            // либо берем первую букву в верхнем регистре
                            else
                            {
                                sb.Append(subS[0].ToString().ToUpper());
                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }
    }
}