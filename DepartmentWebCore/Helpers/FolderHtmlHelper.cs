using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Text;

namespace DepartmentWebCore.Helpers
{
    public static class FolderHtmlHelper
    {
        public static HtmlString DisciplineFolder(this IHtmlHelper helper, DisciplineContextElementModel folderItem, Guid disciplineId, bool action)
        {
            StringBuilder sb = new StringBuilder();

            if (folderItem.Childs != null)
            {
                foreach (var elem in folderItem.Childs)
                {
                    sb.Append(SubFolders(elem, disciplineId, action));
                }
            }

            return new HtmlString($"<ul class=\"disciplne-content\">{sb.ToString()}</ul>");
        }

        private static string SubFolders(DisciplineContextElementModel folderItem, Guid disciplineId, bool action)
        {
            StringBuilder sb = new StringBuilder();
            if (!folderItem.IsFile && folderItem.Childs != null && folderItem.Childs.Count > 0)
            {
                sb.Append("<ul class=\"disciplne-folder-active\">");
                foreach (var elem in folderItem.Childs)
                {
                    sb.Append(SubFolders(elem, disciplineId, action));
                }
                sb.Append("</ul>");
            }

            string icon = $"fas fa-{(folderItem.IsFile ? "file-download" : $"folder{(folderItem.Childs == null || folderItem.Childs.Count == 0 ? "" : "-open")} disciplne-folder-action")}";

            string mainClass = folderItem.IsFile ? "disciplne-file" : "disciplne-folder";

            string name = folderItem.IsFile ? $"<a href=\"/Discipline/Download?id={disciplineId}&fullName={folderItem.FullPath}\"><label>{folderItem.Name} ({folderItem.DateUpdate.ToShortDateString()})</label></a>" : folderItem.Name;

            string actions = action ? folderItem.IsFile ?
                $"<i data-file=\"{folderItem.FullPath}\" data-id=\"{disciplineId}\" class=\"fas fa-trash-alt discipline-file-delete\"></i>" :
                $"<i data-folder=\"{folderItem.FullPath}\" data-id=\"{disciplineId}\" class=\"fas fa-plus-circle discipline-file-insert\"></i>" : 
                "";

            return $"<li class=\"disciplne-folder-element\"><i class=\"{icon}\"></i><span class=\"{mainClass}\">{name}</span>{actions}{sb.ToString()}</li>";
        }
    }
}