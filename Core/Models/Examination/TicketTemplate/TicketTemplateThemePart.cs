using System;
using System.Runtime.Serialization;

namespace Models.Examination
{
    /// <summary>
    /// Класс отвечает за темы в документе
    /// TODO пока нет времени его расписывать, будем хранить просто текст
    /// </summary>
    [DataContract]
    public class TicketTemplateThemePart : IdEntity
    {
        [DataMember]
        public Guid TicketTemplateId { get; set; }

        [DataMember]
        public string InnerXml { get; set; }

        [DataMember]
        public int Order { get; set; }

        //-------------------------------------------------------------------------

        public virtual TicketTemplate TicketTemplate { get; set; }

        //-------------------------------------------------------------------------
    }
}