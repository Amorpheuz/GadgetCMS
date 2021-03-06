﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GadgetCMS.Areas.Dashboard.Pages.Shared
{
    public class DashboardPages
    {
        public static string Index => "Index";

        public static string ManageUsers => "ManageUsers";

        public static string Articles => "Articles";

        public static string Parameters => "Parameters";

        public static string Categories => "Categories";

        public static string Reviews => "Reviews";

        public static string DownloadNLogFiles => "DownloadNLogFiles";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string ManageUsersNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageUsers);

        public static string ArticlesUsersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Articles);

        public static string ParametersUsersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Parameters);

        public static string CategoriesUsersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Categories);

        public static string ReviewsUsersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Reviews);

        public static string DownloadNLogFilesNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadNLogFiles);


        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                             ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
