﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;

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
    }
}