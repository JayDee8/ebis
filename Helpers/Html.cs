using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Text;

namespace ebis.Helpers
{
    public static class Html
    {
        /// <summary>
        /// Displays the sort direction arrow depending on a grids current column state.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="grid"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static MvcHtmlString SortDirection(this HtmlHelper helper, ref WebGrid grid, String columnName)
        {
            String html = "";

            if (grid.SortColumn == columnName && grid.SortDirection == System.Web.Helpers.SortDirection.Ascending)
            {
                html = "˅";
            }
            else if (grid.SortColumn == columnName && grid.SortDirection == System.Web.Helpers.SortDirection.Descending)
            {
                html = "˄";
            }
            else
            {
                html = "";
            }

            return MvcHtmlString.Create(html);
        }
        
        public static MvcHtmlString Nl2Br(this HtmlHelper htmlHelper, string text)
        {
            if (string.IsNullOrEmpty(text))
                return MvcHtmlString.Create(text);
            else
            {
                StringBuilder builder = new StringBuilder();
                string[] lines = text.Split('\n');
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i > 0)
                        builder.Append("<br/>\n");
                    builder.Append(HttpUtility.HtmlEncode(lines[i]));
                }
                return MvcHtmlString.Create(builder.ToString());
            }
        }

        public static MvcHtmlString SumValues(this HtmlHelper htmlHelper, int value1, int value2, int value3)
        {
            int res = value1 + value2 + value3;
            return MvcHtmlString.Create(res.ToString());
        }

        public static MvcHtmlString StavString(this HtmlHelper htmlHelper, int value)
        {
            switch (value)
            {
                case 1:
                    return MvcHtmlString.Create("potvrzeno");
                case 2:
                    return MvcHtmlString.Create("odmítnuto");
                case 3:
                    return MvcHtmlString.Create("časový problém");
                default:
                    return MvcHtmlString.Create("bez odpovědi");
            }
        }

        public static MvcHtmlString GetNastroje(this HtmlHelper htmlHelper, ebis.Models.osoby values)
        {
            var nastroje = "";
            foreach (var nastroj in values.nastroje.ToList())
            {
                nastroje += nastroj.zkratka + " ";
            }
            return MvcHtmlString.Create(nastroje);
        }
    }
}