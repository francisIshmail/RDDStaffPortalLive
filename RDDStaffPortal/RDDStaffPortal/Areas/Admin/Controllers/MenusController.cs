using RDDStaffPortal.Areas.Admin.Models;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.InitialSetup;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

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
                result = moduleDbOp.save1(rdd_module);
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
                rdd_menu.Levels = menu.Levels;
                rdd_menu.MenuId = menu.MenuId;
                rdd_menu.MenuName = menu.MenuName;
                rdd_menu.MenuCssClass = menu.MenuCssClass;
                rdd_menu.ModuleId = menu.ModuleId;
                rdd_menu.ModuleName = menu.ModuleName;
                rdd_menu.URL = menu.URL;
                rdd_menu.DisplaySeq = menu.DisplaySeq;
                rdd_menu.IsDefault = menu.IsDefault;
                rdd_menu.CreatedBy = User.Identity.Name;
                result = menuDbOp.save1(rdd_menu);
            }
            catch (Exception ex)
            {
                result = "Error occured :" + ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMenuList()
        {
            return Json(menuDbOp.GetMenuList1(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult BindModules(int Levels)
        {
            List<RDD_Modules> modules = new List<RDD_Modules>();
            modules = moduleDbOp.GetModulesList3(Levels);
            return Json(modules, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteMenu(int MenuId)
        {
            return Json( new { DeleteFlag = menuDbOp.DeleteMenu(MenuId) }, JsonRequestBehavior.AllowGet);
        }






    }
}