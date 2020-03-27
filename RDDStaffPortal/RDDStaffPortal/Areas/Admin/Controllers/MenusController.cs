using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.Admin.Models;
using RDDStaffPortal.DAL.InitialSetup;
using RDDStaffPortal.DAL.DataModels;

namespace RDDStaffPortal.Areas.Admin.Controllers
{
    [Authorize]
    public class MenusController : Controller
    {
        ModulesDbOperation moduleDbOp = new ModulesDbOperation();
        MenusDbOperation menuDbOp = new MenusDbOperation();
        // GET: Admin/Menus
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AddModule(Modules module)
        {
            string result = string.Empty;
            try
            {
                RDD_Modules rdd_module = new RDD_Modules();
                rdd_module.ModuleId = module.ModuleId;
                rdd_module.ModuleName = module.ModuleName;
                rdd_module.cssClass = module.ModuleCssClass;
                rdd_module.IsActive = true;
                rdd_module.CreatedBy = User.Identity.Name;
                result = moduleDbOp.Save(rdd_module);
            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AddMenu(Menus menu)
        {
            string result = string.Empty;
            try
            {
                RDD_Menus rdd_menu = new RDD_Menus();
                rdd_menu.MenuId = menu.MenuId;
                rdd_menu.MenuName = menu.MenuName;
                rdd_menu.MenuCssClass = menu.MenuCssClass;
                rdd_menu.ModuleId = menu.ModuleId;
                rdd_menu.ModuleName = menu.ModuleName;
                rdd_menu.URL = menu.URL;
                rdd_menu.DisplaySeq = menu.DisplaySeq;
                rdd_menu.IsDefault = menu.IsDefault;
                rdd_menu.CreatedBy = User.Identity.Name;
                result = menuDbOp.Save(rdd_menu);
            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMenuList()
        {
            return Json(menuDbOp.GetMenuList(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult BindModules()
        {
            List<RDD_Modules> modules = new List<RDD_Modules>();
            modules = moduleDbOp.GetModuleList();
            return Json(modules, JsonRequestBehavior.AllowGet);

        }



    }
}