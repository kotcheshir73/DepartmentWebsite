using DepartmentWebCore.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace DepartmentWebCore.Helpers
{
    public static class MenuHtmlHelper
    {
        public static HtmlString MenuItem(this IHtmlHelper helper, MenuElementModel menuItem)
        {
            string action = $"<a href=\"/{menuItem.Controller}/{menuItem.Action}\">{menuItem.Name}</a>";

            StringBuilder sb = new StringBuilder();

            if(menuItem.Child != null)
            {
                sb.Append("<ul class=\"sub-menu\">");
                foreach (var elem in menuItem.Child)
                {
                    sb.Append(SubMenuItem(elem));
                }
                sb.Append("</ul>");
            }

            return new HtmlString($"<li class=\"menuMarginLeft\">{action}{sb.ToString()}</li>");
        }

        private static string SubMenuItem(MenuElementModel menuItem)
        {
            StringBuilder sb = new StringBuilder();

            if (menuItem.Child != null)
            {
                sb.Append("<ul class=\"sub-menu\">");
                foreach (var elem in menuItem.Child)
                {
                    sb.Append(SubMenuItem(elem));
                }
                sb.Append("</ul>");
            }

            StringBuilder addParameters = new StringBuilder("?");
            if(menuItem.Id.HasValue)
            {
                addParameters.Append($"Id={menuItem.Id}");
            }
            if(menuItem.AdditionalParameters != null)
            {
                foreach(var elem in menuItem.AdditionalParameters)
                {
                    addParameters.Append($"&{elem.Key}={elem.Value}");
                }
            }

            if(addParameters.Length == 1)
            {
                addParameters.Clear();
            }

            string action = $"<a href=\"/{menuItem.Controller}/{menuItem.Action}{addParameters}\">{menuItem.Name}</a>";

            return $"<li class=\"sub-menu-item\">{action}{sb.ToString()}</li>";
        }
    }
}