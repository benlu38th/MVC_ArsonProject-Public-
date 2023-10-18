using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_ArsonProject.Models;

namespace MVC_ArsonProject.Areas.BackEnd.Filter
{
    public class PermissionFilter : ActionFilterAttribute
    {
        private Context db = new Context();

        StringBuilder _menu = new StringBuilder();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //清空menu字串
            _menu.Clear();

            //取得使用者識別
            int _ticketUserID = Convert.ToInt32(((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.Name);

            //判斷使用者是否有該權限
            //取得目前URL的controllerName及actionName
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();

            //使用者有的權限
            var userPermissions = db.Users.Where(u => u.Id == _ticketUserID).Select(u => u.Permission).FirstOrDefault()?.Split(',');

            bool userHasPermission = false;
            if (userPermissions != null)
            {
                //取出使用者擁有的權限的控制器名稱
                var userPermissionsControllerNames = db.Permissions
                    .Where(p => userPermissions.Contains(p.Code))
                    .Select(p => p.ControllerName)
                    .ToList();

                foreach (var item in userPermissionsControllerNames)
                {
                    if (item == controllerName)
                    {
                        userHasPermission = true;
                        break;
                    }
                }
            }

            //判斷是否有這一頁的權限
            if(userHasPermission == false)
            {
                //跳回UserList頁
                filterContext.Result = new RedirectResult("~/Backend/UsersList");
                return;
            }

            //全部權限
            List<Permission> allPermissions = db.Permissions.ToList();

            //第一層
            List<Permission> roots = db.Permissions.Where(p => p.ParentId == null).ToList();

            _menu.Append("<nav id =\"app-nav-main\" class=\"app-nav app-nav- ain flex-grow-1\">");
            _menu.Append("<ul class=\"app-menu list-unstyled accordion\" id=\"menu-accordion\">");


            //組出Menu字串
            foreach (var root in roots)
            {
                _menu.Append("<li class=\"nav-item\">");

                if (userPermissions != null && userPermissions.Any(u => u.StartsWith(root.Code)))
                {
                    if (root.Permissions.Count > 0)
                    {
                        //_menu.Append($"<a class=\"nav-link @((controllerName == \"{root.ControllerName}\") ? \"active\" : \"\")\" href=\"{root.Url}\">");
                        _menu.Append($"<a class=\"nav-link {(controllerName == root.ControllerName ? "active" : "")}\" href=\"{root.Url}\">");

                        _menu.Append("<span class=\"nav-icon\">");
                        _menu.Append("<svg width=\"1em\" height=\"1em\" viewBox=\"0 0 16 16\" class=\"bi bi-files\" fill=\"currentColor\" xmlns=\"http://www.w3.org/2000/svg\">");
                        _menu.Append("<path fill-rule=\"evenodd\" d=\"M4 2h7a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2zm0 1a1 1 0 0 0-1 1v10a1 1 0 0 0 1 1h7a1 1 0 0 0 1-1V4a1 1 0 0 0-1-1H4z\" />");
                        _menu.Append("<path d=\"M6 0h7a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2v-1a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H6a1 1 0 0 0-1 1H4a2 2 0 0 1 2-2z\" />");
                        _menu.Append("</svg>");
                        _menu.Append("</span>");
                        _menu.Append($"<span class=\"nav-link-text\">{root.Subject}</span>");

                        _menu.Append($"<span class=\"submenu-arrow\">");
                        _menu.Append($"<svg width = \"1em\" height=\"1em\" viewBox=\"0 0 16 16\" class=\"bi bi-chevron-down\" fill=\"currentColor\" xmlns=\"http://www.w3.org/2000/svg\">");
                        _menu.Append($"<path fill-rule=\"evenodd\" d=\"M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708z\" />");
                        _menu.Append($"</svg>");
                        _menu.Append($"</span>");

                        _menu.Append("</a>");
                        _menu.Append("</a>");

                        _menu.Append("<div>");
                        _menu.Append("<ul class=\"list-unstyled\">");
                        BuildChildMenuString(root, controllerName);
                        _menu.Append("</ul>");
                        _menu.Append("</div>");
                    }
                    else//最後一層
                    {
                        _menu.Append($"<a class=\"nav-link {(controllerName == root.ControllerName ? "active" : "")}\" href=\"{root.Url}\">");
                        _menu.Append("<span class=\"nav-icon\">");
                        _menu.Append("<svg width=\"1em\" height=\"1em\" viewBox=\"0 0 16 16\" class=\"bi bi-files\" fill=\"currentColor\" xmlns=\"http://www.w3.org/2000/svg\">");
                        _menu.Append("<path fill-rule=\"evenodd\" d=\"M4 2h7a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2zm0 1a1 1 0 0 0-1 1v10a1 1 0 0 0 1 1h7a1 1 0 0 0 1-1V4a1 1 0 0 0-1-1H4z\" />");
                        _menu.Append("<path d=\"M6 0h7a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2v-1a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H6a1 1 0 0 0-1 1H4a2 2 0 0 1 2-2z\" />");
                        _menu.Append("</svg>");
                        _menu.Append("</span>");
                        _menu.Append($"<span class=\"nav-link-text\">{root.Subject}</span>");
                        _menu.Append("</a>");
                    }
                }
            }
            _menu.Append("</nav");
            _menu.Append("</ul>");

            filterContext.Controller.ViewBag.Menu = _menu.ToString();
        }
        private void BuildChildMenuString(Permission nodes , string controllerName)
        {
            //取得使用者識別
            int _ticketUserID = Convert.ToInt32(((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.Name);

            //使用者有的權限
            var userPermissions = db.Users.Where(u => u.Id == _ticketUserID).Select(u => u.Permission).FirstOrDefault()?.Split(',');

            foreach (var node in nodes.Permissions)
            {
                if (userPermissions != null && userPermissions.Any(u => u.StartsWith(node.Code)))
                {
                    if (node.Permissions.Count > 0)
                    {
                        _menu.Append("<li>");
                        _menu.Append($"<a class=\"nav-link {(controllerName == node.ControllerName ? "active" : "")}\" href=\"{node.Url}\">");
                        //_menu.Append($"<a class=\"nav-link\" href=\"\">");
                        _menu.Append($"<span class=\"nav-link-text\">{node.Subject}</span>");

                        _menu.Append($"<span class=\"submenu-arrow\">");
                        _menu.Append($"<svg width = \"1em\" height=\"1em\" viewBox=\"0 0 16 16\" class=\"bi bi-chevron-down\" fill=\"currentColor\" xmlns=\"http://www.w3.org/2000/svg\">");
                        _menu.Append($"<path fill-rule=\"evenodd\" d=\"M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708z\" />");
                        _menu.Append($"</svg>");
                        _menu.Append($"</span>");

                        _menu.Append("</a>");

                        _menu.Append("<div>");
                        _menu.Append("<ul class=\"submenu-list list-unstyled\">");
                        BuildChildMenuString(node, controllerName);
                        _menu.Append("</ul>");
                        _menu.Append("</div>");
                        _menu.Append("</li>");
                    }
                    else
                    {
                        _menu.Append("<li>");
                        _menu.Append($"<a class=\"nav-link {(controllerName == node.ControllerName ? "active" : "")}\" href=\"{node.Url}\">");
                        //_menu.Append($"<a class=\"nav-link\" href=\"{node.Url}\">");
                        _menu.Append($"<span class=\"nav-link-text\">{node.Subject}</span>");
                        _menu.Append("</a>");
                        _menu.Append("</li>");
                    }
                }
            }
        }
    }
}