using System;

namespace Models.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ClassUseAttribute : Attribute
    {
        public string ClassName { get; set; }

        public string ColumnName { get; set; }

        public string Description { get; set; }

        public ClassUseAttribute(string ClassName, string ColumnName, string Description)
        {
            this.ClassName = ClassName;
            this.ColumnName = ColumnName;
            this.Description = Description;
        }
    }
}