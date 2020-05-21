using RDDStaffPortal.Areas.Admin.Models;
using RDDStaffPortal.DAL.DataModels;
using RDDStaffPortal.DAL.InitialSetup;
using System;
using System.Collections.Generic;
using System.Linq;
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

            JsonResult result = new JsonResult();
            string search = Request.Form.GetValues("search[value]")[0];
            string draw = Request.Form.GetValues("draw")[0];
            string order = Request.Form.GetValues("order[0][column]")[0];
            string orderDir = Request.Form.GetValues("order[0][dir]")[0];
            int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);


           

            if (pageSize == -1)
            {
                draw = "2";
            }

            List<RDD_Menus> data = menuDbOp.GetMenuList1();
            // Total record count.
            int totalRecords = 0;
            if (data != null)
            {
                totalRecords = data.Count;
            }
            else
                totalRecords = 0;


            if (!string.IsNullOrEmpty(search) &&
                        !string.IsNullOrWhiteSpace(search))
            {
                // Apply search
                data = data.Where(p => p.MenuId.ToString().ToLower().Contains(search.ToLower()) ||
                                        p.MenuName.ToString().ToLower().Contains(search.ToLower()) ||
                                        p.MenuCssClass.ToString().ToLower().Contains(search.ToLower()) ||
                                        p.ModuleName.ToString().ToLower().Contains(search.ToLower()) ||
                                        p.Levels.ToString().ToLower().Contains(search.ToLower()) ||
                                        p.URL.ToString().ToLower().Contains(search.ToLower()) ||
                                        p.DisplaySeq.ToString().ToLower().Contains(search.ToLower()) ||
                                       p.IsDefault.ToString().ToLower().Contains(search.ToLower())

                                      ).ToList();

            }

         //   if (!string.IsNullOrEmpty(SeqNo1) &&
         //  !string.IsNullOrWhiteSpace(SeqNo1))
         //   {
         //       data = data.Where(p => p.DisplaySeq.ToString().ToLower().Contains(SeqNo1.ToLower())
         //                             ).ToList();
         //   }
         //   if (!string.IsNullOrEmpty(MenuId1) &&
         //   !string.IsNullOrWhiteSpace(MenuId1))
         //   {
         //       data = data.Where(p => p.MenuId.ToString().ToLower().Contains(MenuId1.ToLower())
         //                             ).ToList();
         //   }
         //   if (!string.IsNullOrEmpty(MenuName1) &&
         // !string.IsNullOrWhiteSpace(MenuName1))
         //   {
         //       data = data.Where(p => p.MenuId.ToString().ToLower().Contains(MenuName1.ToLower())
         //                             ).ToList();
         //   }
         //   if (!string.IsNullOrEmpty(Module1) &&
         //!string.IsNullOrWhiteSpace(Module1))
         //   {
         //       data = data.Where(p => p.ModuleName.ToString().ToLower().Contains(Module1.ToLower())
         //                             ).ToList();
         //   }
         //   if (!string.IsNullOrEmpty(CssClass1) &&
         //!string.IsNullOrWhiteSpace(CssClass1))
         //   {
         //       data = data.Where(p => p.MenuCssClass.ToString().ToLower().Contains(CssClass1.ToLower())
         //                             ).ToList();
         //   }
         //   if (!string.IsNullOrEmpty(Default1) &&
         //  !string.IsNullOrWhiteSpace(Default1))
         //   {
         //       data = data.Where(p => p.IsDefault.ToString().ToLower().Contains(Default1.ToLower())
         //                             ).ToList();
         //   }

         //   if (!string.IsNullOrEmpty(Levels1) &&
         // !string.IsNullOrWhiteSpace(Levels1))
         //   {
         //       data = data.Where(p => p.Levels.ToString().ToLower().Contains(Levels1.ToLower())
         //                             ).ToList();
         //   }
            int recFilter = data.Count;


          //  data = this.MenuColumnWithOrder(order, orderDir, data);

            if (pageSize != -1)
            {
                data = data.Skip(startRec).Take(pageSize).ToList();
            }
            else
            {
                data = data.ToList();
            }

            result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);

            return result;

           // return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<RDD_Menus> MenuColumnWithOrder(string order, string orderDir, List<RDD_Menus> data)
        {
            // Initialization.
            List<RDD_Menus> lst = new List<RDD_Menus>();
            try
            {
                // Sorting
                switch (order)
                {
                    case "0":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.MenuId).ToList()
                                                                                                 : data.OrderBy(p => p.MenuId).ToList();
                        break;

                    case "1":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.MenuName).ToList()
                                                                                                 : data.OrderBy(p => p.MenuName).ToList();
                        break;
                    case "2":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Levels).ToList()
                                                                                                 : data.OrderBy(p => p.Levels).ToList();
                        break;
                    case "3":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.URL).ToList()
                                                                                                 : data.OrderBy(p => p.URL).ToList();
                        break;
                    case "4":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.MenuCssClass).ToList()
                                                                                                 : data.OrderBy(p => p.MenuCssClass).ToList();
                        break;
                    case "5":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.DisplaySeq).ToList()
                                                                                                 : data.OrderBy(p => p.DisplaySeq).ToList();
                        break;
                    case "6":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ModuleName).ToList()
                                                                                                 : data.OrderBy(p => p.ModuleName).ToList();
                        break;
                    case "7":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ModuleId).ToList()
                                                                                                 : data.OrderBy(p => p.ModuleId).ToList();
                        break;

                    default:
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.IsDefault).ToList()
                                                                                                  : data.OrderBy(p => p.IsDefault).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;

        }


        public ActionResult BindModules(int Levels)
        {
            List<RDD_Modules> modules = new List<RDD_Modules>();
            modules = moduleDbOp.GetModulesList3(Levels);
            return Json(modules, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteMenu(int MenuId)
        {
            return Json(new { DeleteFlag = menuDbOp.DeleteMenu(MenuId) }, JsonRequestBehavior.AllowGet);
        }






    }
}