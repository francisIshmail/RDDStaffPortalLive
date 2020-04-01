using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDDStaffPortal.Areas.Admin.Models;

namespace RDDStaffPortal.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// This is test change in HomeController to check push from VS into GitHub
        /// Successfully pushed changes from VS to GitHub & Now checking from GitHib to VS
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult GetMenus()
        {
            try
            {

                List<Menus> mns = new List<Menus> {
                    new Menus{ ModuleName="Admin" , ModuleCssClass="fas fa-user menu-icon-c" , MenuName= "Menu Master", MenuCssClass="sub-item", URL="Admin/Menus/Index"  },
                    new Menus{ ModuleName="Admin" , ModuleCssClass="fas fa-user menu-icon-c" , MenuName= "User", MenuCssClass="sub-item", URL="Admin/Menus/Index"  },
                    new Menus{ ModuleName="Admin" , ModuleCssClass="fas fa-user menu-icon-c" , MenuName= "Designation", MenuCssClass="sub-item", URL="Admin/Menus/Index"  },

                    new Menus{ ModuleName="HR" , ModuleCssClass="fas fa-user-tie menu-icon-c" , MenuName= "Employee Registration", MenuCssClass="sub-item", URL="Admin/Menus/Index"  },
                    new Menus{ ModuleName="Admin" , ModuleCssClass="fas fa-user-tie menu-icon-c" , MenuName= "Department", MenuCssClass="sub-item", URL="Admin/Menus/Index"  },
                    new Menus{ ModuleName="Admin" , ModuleCssClass="fas fa-user-tie menu-icon-c" , MenuName= "Apply For Leave", MenuCssClass="sub-item", URL="Admin/Menus/Index"  },
                };

                //var result = (from m in objEntity.Menu_Tree
                //              select new Dynamic_Menu.Models.Menu_List
                //              {
                //                  M_ID = m.M_ID,
                //                  M_P_ID = m.M_P_ID,
                //                  M_NAME = m.M_NAME,
                //                  CONTROLLER_NAME = CONTROLLER_NAME,
                //                  ACTION_NAME = ACTION_NAME,
                //              }).ToList();
                return View("Menu", mns);
            }
            catch (Exception ex)
            {
                var error = ex.Message.ToString();
                return Content("Error");
            }
        }

    }
}
