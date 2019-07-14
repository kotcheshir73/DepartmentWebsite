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

            return $"<li class=\"sub-menu-item\" data-controller=\"{menuItem.Controller}\" data-action=\"{menuItem.Action}\" data-id=\"{menuItem.Id}\">{menuItem.Name}{sb.ToString()}</li>";
        }
    }
}